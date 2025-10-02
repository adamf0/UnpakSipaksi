using Docker.DotNet.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.Roadmap.Application.CreateRoadmap;
using UnpakSipaksi.Modules.Roadmap.ApplicationTest;
using Xunit;

namespace Application.Integration.Tests
{
    public class RoadmapTest : BaseIntegrationTest
    {
        public RoadmapTest(IntegrationTestWebAppFactory factory) : base(factory)
        {
        }

        public static IEnumerable<object[]> InvalidData()
        {
            var validNidn = "1234567890";
            var validLink = "https://drive.google.com/file/abc123";
            var empty = "";
            var invalidLink = "https://example.com/file";

            // Nidn kosong
            yield return new object[] { empty, validLink, "'Nidn' tidak boleh kosong." };
            // Link kosong
            yield return new object[] { validNidn, empty, "'Link' tidak boleh kosong." };
            // Link tidak valid
            yield return new object[] { validNidn, invalidLink, "'Link' harus berupa URL drive.google.com yang valid dan diawali dengan http:// atau https://." };
        }

        [Theory]
        [MemberData(nameof(InvalidData))]
        public async Task Create_ShouldThrow_WhenInvalid(string nidn, string link, string message)
        {
            var command = new CreateRoadmapCommand(nidn, link);
            var result = await Sender.Send(command);

            Assert.True(result.IsFailure);
            var validationError = Assert.IsType<ValidationError>(result.Error);
            Assert.Contains(validationError.Errors, e => e.Description == message);
        }

        [Fact]
        public async Task Create_ShouldAdd_WhenValid_AndCallHandler()
        {
            var command = new CreateRoadmapCommand("1234567890", "https://drive.google.com/file/xxxx");
            var result = await Sender.Send(command);

            var data = DBContext.Roadmap.FirstOrDefault(p => p.Uuid == result!.Value);
            Assert.NotNull(data);
            Assert.Equal("1234567890", data.Nidn);
            Assert.Equal("https://drive.google.com/file/xxxx", data.Link);

            // Assert handler bisa diresolve dari DI
            using (var scope = Factory.Services.CreateScope())
            {
                var handler = scope.ServiceProvider
                    .GetService<IRequestHandler<CreateRoadmapCommand, Result<Guid>>>();

                Assert.NotNull(handler);
                Assert.IsType<CreateRoadmapCommandHandler>(handler);
            }
        }
    }
}
