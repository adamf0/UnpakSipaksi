using Docker.DotNet.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.RumpunIlmu1.Application.DeleteRumpunIlmu1;
using UnpakSipaksi.Modules.RumpunIlmu1.Application.UpdateRumpunIlmu1;
using UnpakSipaksi.Modules.RumpunIlmu1.Application.CreateRumpunIlmu1;
using UnpakSipaksi.Modules.RumpunIlmu1.ApplicationTest;
using Xunit;

namespace Application.Integration.Tests
{
    public class RumpunIlmu1Test : BaseIntegrationTest
    {
        public RumpunIlmu1Test(IntegrationTestWebAppFactory factory) : base(factory)
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
            var command = new CreateRumpunIlmu1Command(nama);
            var result = await Sender.Send(command);

            Assert.True(result.IsFailure);
            var validationError = Assert.IsType<ValidationError>(result.Error);
            Assert.Contains(validationError.Errors, e => e.Description == message);
        }

        [Fact]
        public async Task Create_ShouldAdd_WhenValid_AndCallHandler()
        {
            var command = new CreateRumpunIlmu1Command("tes");
            var result = await Sender.Send(command);

            var data = DBContext.RumpunIlmu1.FirstOrDefault(p => p.Uuid == result!.Value);
            Assert.NotNull(data);
            Assert.Equal("tes", data.Nama);

            // Assert handler bisa diresolve dari DI
            using (var scope = Factory.Services.CreateScope())
            {
                var handler = scope.ServiceProvider
                    .GetService<IRequestHandler<CreateRumpunIlmu1Command, Result<Guid>>>();

                Assert.NotNull(handler);
                Assert.IsType<CreateRumpunIlmu1CommandHandler>(handler);
            }
        }
        [Fact]
        public async Task Update_ShouldThrow_WhenNotExist()
        {
            var guid = Guid.NewGuid().ToString();
            var namaBefore = "tes";

            var updateCommand = new UpdateRumpunIlmu1Command(guid, namaBefore);
            var updateResult = await Sender.Send(updateCommand);

            Assert.True(updateResult.IsFailure);
            Assert.Equal("RumpunIlmu1.NotFound", updateResult.Error.Code);
        }

        [Fact]
        public async Task Delete_ShouldThrow_WhenNotExist()
        {
            var guid = Guid.NewGuid().ToString();

            var deleteCommand = new DeleteRumpunIlmu1Command(guid);
            var deleteResult = await Sender.Send(deleteCommand);

            Assert.True(deleteResult.IsFailure);
            Assert.Equal("RumpunIlmu1.NotFound", deleteResult.Error.Code);
        }
    }
}
