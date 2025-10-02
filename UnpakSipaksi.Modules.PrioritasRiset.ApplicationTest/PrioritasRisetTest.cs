using Docker.DotNet.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.PrioritasRiset.Application.CreatePrioritasRiset;
using UnpakSipaksi.Modules.PrioritasRiset.ApplicationTest;
using Xunit;

namespace Application.Integration.Tests
{
    public class PrioritasRisetTest : BaseIntegrationTest
    {
        public PrioritasRisetTest(IntegrationTestWebAppFactory factory) : base(factory)
        {
        }

        public static IEnumerable<object[]> InvalidData()
        {
            var empty = "";

            // Nama kosong
            yield return new object[] { empty, "'Nama' tidak boleh kosong." };
        }

        [Theory]
        [MemberData(nameof(InvalidData))]
        public async Task Create_ShouldThrow_WhenInvalid(string nama, string message)
        {
            var command = new CreatePrioritasRisetCommand(nama);
            var result = await Sender.Send(command);

            Assert.True(result.IsFailure);
            var validationError = Assert.IsType<ValidationError>(result.Error);
            Assert.Contains(validationError.Errors, e => e.Description == message);
        }

        [Fact]
        public async Task Create_ShouldAdd_WhenValid_AndCallHandler()
        {
            var command = new CreatePrioritasRisetCommand("tes");
            var result = await Sender.Send(command);

            var data = DBContext.PrioritasRiset.FirstOrDefault(p => p.Uuid == result!.Value);
            Assert.NotNull(data);
            Assert.Equal("tes", data.Nama);

            // Assert handler bisa diresolve dari DI
            using (var scope = Factory.Services.CreateScope())
            {
                var handler = scope.ServiceProvider
                    .GetService<IRequestHandler<CreatePrioritasRisetCommand, Result<Guid>>>();

                Assert.NotNull(handler);
                Assert.IsType<CreatePrioritasRisetCommandHandler>(handler);
            }
        }
    }
}
