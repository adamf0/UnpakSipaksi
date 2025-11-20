using Docker.DotNet.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System.Reflection;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.RumpunIlmu1.PublicApi;
using UnpakSipaksi.Modules.RumpunIlmu2.Application.CreateRumpunIlmu2;
using UnpakSipaksi.Modules.RumpunIlmu2.Application.DeleteRumpunIlmu2;
using UnpakSipaksi.Modules.RumpunIlmu2.Application.UpdateRumpunIlmu2;
using UnpakSipaksi.Modules.RumpunIlmu2.Domain.RumpunIlmu2;
using UnpakSipaksi.Modules.RumpunIlmu2.PublicApi;
using UnpakSipaksi.Modules.RumpunIlmu3.Application.Abstractions.Data;
using UnpakSipaksi.Modules.RumpunIlmu3.Application.CreateRumpunIlmu3;
using UnpakSipaksi.Modules.RumpunIlmu3.Application.DeleteRumpunIlmu3;
using UnpakSipaksi.Modules.RumpunIlmu3.Application.UpdateRumpunIlmu3;
using UnpakSipaksi.Modules.RumpunIlmu3.ApplicationTest;
using UnpakSipaksi.Modules.RumpunIlmu3.Domain.RumpunIlmu3;
using Xunit;

namespace Application.Integration.Tests
{
    public class RumpunIlmu3Test : BaseIntegrationTest
    {
        public RumpunIlmu3Test(IntegrationTestWebAppFactory factory) : base(factory)
        {
        }

        public static IEnumerable<object[]> InvalidData()
        {
            var valid = Guid.NewGuid().ToString();
            var rumpunIlmu2 = Guid.NewGuid().ToString();
            var empty = "";

            // CREATE
            yield return new object[] { empty, "", rumpunIlmu2, "'Nama' tidak boleh kosong.", "created" };
            yield return new object[] { valid, "tes", empty, "'RumpunIlmu2' tidak boleh kosong.", "created" };
            yield return new object[] { valid, "tes", "no-guid", "'RumpunIlmu2' harus dalam format UUID v4 yang valid.", "created" };

            // UPDATE
            yield return new object[] { empty, "tes", rumpunIlmu2, "'Uuid' tidak boleh kosong.", "updated" };
            yield return new object[] { "no-guid", "tes", rumpunIlmu2, "'Uuid' harus dalam format UUID v4 yang valid.", "updated" };
            yield return new object[] { valid, "", rumpunIlmu2, "'Nama' tidak boleh kosong.", "updated" };
            yield return new object[] { valid, "tes", empty, "'RumpunIlmu2' tidak boleh kosong.", "updated" };
            yield return new object[] { valid, "tes", "no-guid", "'RumpunIlmu2' harus dalam format UUID v4 yang valid.", "updated" };

            // DELETE
            yield return new object[] { empty, "tes", rumpunIlmu2, "'Uuid' tidak boleh kosong.", "deleted" };
            yield return new object[] { "no-guid", "tes", rumpunIlmu2, "'Uuid' harus dalam format UUID v4 yang valid.", "deleted" };
        }

        public static IEnumerable<object?[]> ValidData()
        {
            var rumpunIlmu2 = Guid.NewGuid().ToString();

            yield return new object?[] { new object[] { "tes", rumpunIlmu2 }, null, "created" };
            yield return new object?[] { new object[] { "tes", rumpunIlmu2 }, new object[] { "tes2", rumpunIlmu2 }, "updated" };
            yield return new object?[] { new object[] { "tes", rumpunIlmu2 }, null, "deleted" };
        }

        [Theory]
        [MemberData(nameof(InvalidData))]
        public async Task CreateUpdateDelete_ShouldThrow_WhenInvalidFluentValidation(
            string uuid,
            string nama,
            string rumpunIlmu2,
            string message,
            string mode)
        {
            Result? result = null;

            if (mode == "created")
            {
                var command = new CreateRumpunIlmu3Command(nama, rumpunIlmu2);
                result = await Sender.Send(command);
            }
            else if (mode == "updated")
            {
                var command = new UpdateRumpunIlmu3Command(uuid, nama, rumpunIlmu2);
                result = await Sender.Send(command);
            }
            else // deleted
            {
                var command = new DeleteRumpunIlmu3Command(uuid);
                result = await Sender.Send(command);
            }

            Assert.True(result.IsFailure);
            if (result.Error is ValidationError validationError)
            {
                Assert.Contains(validationError.Errors, e => e.Description == message);
            }
            else
            {
                Assert.Equal(message, result.Error.Description);
            }
        }

        [Theory]
        [MemberData(nameof(ValidData))]
        public async Task CreateUpdateDelete_ShouldBeExecute_WhenValidData(
            object[] beforeData,
            object[]? afterData,
            string mode)
        {
            var namaBefore = (string)beforeData[0];
            var rumpunIlmu2Before = (string)beforeData[1];

            var rumpunIlmu2Mock = new Mock<IRumpunIlmu2Api>();
            rumpunIlmu2Mock.Setup(x => x.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new RumpunIlmu2Response("1", Guid.NewGuid().ToString(), rumpunIlmu2Before));

            // CREATE
            using (var scope = Factory.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                var handler = new CreateRumpunIlmu3CommandHandler(
                    services.GetRequiredService<IRumpunIlmu3Repository>(),
                    rumpunIlmu2Mock.Object,
                    services.GetRequiredService<IUnitOfWork>()
                );

                var command = new CreateRumpunIlmu3Command(namaBefore, rumpunIlmu2Before);

                // Act
                var createResult = await handler.Handle(command, CancellationToken.None);

                // Assert
                Assert.True(createResult.IsSuccess);
                var newUuid = createResult.Value.ToString();

                // UPDATE / DELETE
                if (mode == "updated")
                {
                    var namaAfter = (string)afterData![0];
                    var rumpunIlmu2After = (string)afterData[1];

                    var handlerUpdate = new UpdateRumpunIlmu3CommandHandler(
                        services.GetRequiredService<IRumpunIlmu3Repository>(),
                        rumpunIlmu2Mock.Object,
                        services.GetRequiredService<IUnitOfWork>()
                    );

                    var updateCommand = new UpdateRumpunIlmu3Command(newUuid, namaAfter, rumpunIlmu2After);

                    // Act
                    var updateResult = await handlerUpdate.Handle(updateCommand, CancellationToken.None);

                    Assert.True(updateResult.IsSuccess);

                    var dataUpdate = DBContext.RumpunIlmu3.FirstOrDefault(p => p.Uuid.ToString() == newUuid);
                    Assert.NotNull(dataUpdate);
                    Assert.Equal(namaAfter, dataUpdate.Nama);
                }
                else if (mode == "deleted")
                {
                    var deleteCommand = new DeleteRumpunIlmu3Command(newUuid);
                    var deleteResult = await Sender.Send(deleteCommand);

                    Assert.True(deleteResult.IsSuccess);
                    var dataDeleted = DBContext.RumpunIlmu3.FirstOrDefault(p => p.Uuid.ToString() == newUuid);
                    Assert.Null(dataDeleted);
                }
            }
        }
        [Fact]
        public async Task Create_ShouldThrow_WhenRumpunIlmu2NotExist()
        {
            var namaBefore = "tes";
            var rumpunIlmu2Before = Guid.NewGuid().ToString();

            var rumpunIlmu2Mock = new Mock<IRumpunIlmu2Api>();
            rumpunIlmu2Mock.Setup(x => x.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((RumpunIlmu2Response?)null);

            // CREATE
            using (var scope = Factory.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                var handler = new CreateRumpunIlmu3CommandHandler(
                    services.GetRequiredService<IRumpunIlmu3Repository>(),
                    rumpunIlmu2Mock.Object,
                    services.GetRequiredService<IUnitOfWork>()
                );

                var command = new CreateRumpunIlmu3Command(namaBefore, rumpunIlmu2Before);

                // Act
                var createResult = await handler.Handle(command, CancellationToken.None);

                // Assert
                Assert.True(createResult.IsFailure);
                Assert.Equal("RumpunIlmu3.RumpunIlmu2NotFound", createResult.Error.Code);
            }
        }
        [Fact]
        public async Task Create_ShouldThrow_WhenDomainRule()
        {
            var namaBefore = "tes";
            var rumpunIlmu2Before = Guid.NewGuid().ToString();

            var rumpunIlmu2Mock = new Mock<IRumpunIlmu2Api>();
            rumpunIlmu2Mock.Setup(x => x.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new RumpunIlmu2Response("0", rumpunIlmu2Before, "rumpun2"));

            // CREATE
            using (var scope = Factory.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                var handler = new CreateRumpunIlmu3CommandHandler(
                    services.GetRequiredService<IRumpunIlmu3Repository>(),
                    rumpunIlmu2Mock.Object,
                    services.GetRequiredService<IUnitOfWork>()
                );

                var command = new CreateRumpunIlmu3Command(namaBefore, rumpunIlmu2Before);

                // Act
                var createResult = await handler.Handle(command, CancellationToken.None);

                // Assert
                Assert.True(createResult.IsFailure);
                Assert.Equal("RumpunIlmu3.UnknownRumpunIlmu2", createResult.Error.Code);
            }
        }

        [Fact]
        public async Task Update_ShouldThrow_WhenNotExist()
        {
            var namaBefore = "tes";
            var rumpunIlmu2Before = Guid.NewGuid().ToString();

            var rumpunIlmu2Mock = new Mock<IRumpunIlmu2Api>();
            rumpunIlmu2Mock.Setup(x => x.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new RumpunIlmu2Response("1", rumpunIlmu2Before, "rumpun2"));

            // CREATE
            using (var scope = Factory.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                var handler = new CreateRumpunIlmu3CommandHandler(
                    services.GetRequiredService<IRumpunIlmu3Repository>(),
                    rumpunIlmu2Mock.Object,
                    services.GetRequiredService<IUnitOfWork>()
                );

                var command = new CreateRumpunIlmu3Command(namaBefore, rumpunIlmu2Before);

                // Act
                var createResult = await handler.Handle(command, CancellationToken.None);

                // Assert
                Assert.True(createResult.IsSuccess);
                //var newUuid = createResult.Value.ToString();

                // UPDATE / DELETE
                var wrongUuid = Guid.NewGuid().ToString();

                var handlerUpdate = new UpdateRumpunIlmu3CommandHandler(
                    services.GetRequiredService<IRumpunIlmu3Repository>(),
                    rumpunIlmu2Mock.Object,
                    services.GetRequiredService<IUnitOfWork>()
                );

                var updateCommand = new UpdateRumpunIlmu3Command(wrongUuid, namaBefore, rumpunIlmu2Before);

                // Act
                var updateResult = await handlerUpdate.Handle(updateCommand, CancellationToken.None);

                Assert.True(updateResult.IsFailure);
                Assert.Equal("RumpunIlmu3.NotFound", updateResult.Error.Code);
            }
        }
        [Fact]
        public async Task Update_ShouldThrow_WhenDomainRule()
        {
            var namaBefore = "tes";
            var rumpunIlmu2Before = Guid.NewGuid().ToString();

            var rumpunIlmu2Mock = new Mock<IRumpunIlmu2Api>();
            rumpunIlmu2Mock.Setup(x => x.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new RumpunIlmu2Response("1", rumpunIlmu2Before, "rumpun2"));

            var rumpunIlmu2WrongMock = new Mock<IRumpunIlmu2Api>();
            rumpunIlmu2WrongMock.Setup(x => x.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new RumpunIlmu2Response("0", rumpunIlmu2Before, "rumpun2"));

            // CREATE
            using (var scope = Factory.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                var handler = new CreateRumpunIlmu3CommandHandler(
                    services.GetRequiredService<IRumpunIlmu3Repository>(),
                    rumpunIlmu2Mock.Object,
                    services.GetRequiredService<IUnitOfWork>()
                );

                var command = new CreateRumpunIlmu3Command(namaBefore, rumpunIlmu2Before);

                // Act
                var createResult = await handler.Handle(command, CancellationToken.None);

                // Assert
                Assert.True(createResult.IsSuccess);
                var newUuid = createResult.Value.ToString();

                // UPDATE / DELETE
                var handlerUpdate = new UpdateRumpunIlmu3CommandHandler(
                    services.GetRequiredService<IRumpunIlmu3Repository>(),
                    rumpunIlmu2WrongMock.Object,
                    services.GetRequiredService<IUnitOfWork>()
                );

                var updateCommand = new UpdateRumpunIlmu3Command(newUuid, namaBefore, rumpunIlmu2Before);

                // Act
                var updateResult = await handlerUpdate.Handle(updateCommand, CancellationToken.None);

                Assert.True(updateResult.IsFailure);
                Assert.Equal("RumpunIlmu3.UnknownRumpunIlmu2", updateResult.Error.Code);
            }
        }

        [Fact]
        public async Task Update_ShouldThrow_WhenRumpunIlmu2NotExist()
        {
            var guid = Guid.NewGuid().ToString();
            var nama = "tes";
            var rumpunIlmu2 = Guid.NewGuid().ToString();

            var rumpunIlmu2Mock = new Mock<IRumpunIlmu2Api>();
            rumpunIlmu2Mock.Setup(x => x.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((RumpunIlmu2Response?)null);

            using (var scope = Factory.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                var handlerUpdate = new UpdateRumpunIlmu3CommandHandler(
                    services.GetRequiredService<IRumpunIlmu3Repository>(),
                    rumpunIlmu2Mock.Object,
                    services.GetRequiredService<IUnitOfWork>()
                );

                var updateCommand = new UpdateRumpunIlmu3Command(guid, nama, rumpunIlmu2);

                // Act
                var updateResult = await handlerUpdate.Handle(updateCommand, CancellationToken.None);

                Assert.True(updateResult.IsFailure);
                Assert.Equal("RumpunIlmu3.RumpunIlmu2NotFound", updateResult.Error.Code);
            }
        }

        [Fact]
        public async Task Delete_ShouldThrow_WhenNotExist()
        {
            var guid = Guid.NewGuid().ToString();

            var command = new DeleteRumpunIlmu2Command(guid);
            var result = await Sender.Send(command);

            Assert.True(result.IsFailure);
            Assert.Equal("RumpunIlmu3.NotFound", result.Error.Code);
        }
    }

}
