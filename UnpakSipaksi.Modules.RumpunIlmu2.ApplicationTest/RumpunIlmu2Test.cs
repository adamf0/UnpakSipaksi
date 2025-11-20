using Microsoft.Extensions.DependencyInjection;
using Moq;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.RumpunIlmu1.PublicApi;
using UnpakSipaksi.Modules.RumpunIlmu2.Application.Abstractions.Data;
using UnpakSipaksi.Modules.RumpunIlmu2.Application.CreateRumpunIlmu2;
using UnpakSipaksi.Modules.RumpunIlmu2.Application.DeleteRumpunIlmu2;
using UnpakSipaksi.Modules.RumpunIlmu2.Application.UpdateRumpunIlmu2;
using UnpakSipaksi.Modules.RumpunIlmu2.ApplicationTest;
using UnpakSipaksi.Modules.RumpunIlmu2.Domain.RumpunIlmu2;
using Xunit;
using static Org.BouncyCastle.Crypto.Engines.SM2Engine;

namespace Application.Integration.Tests
{
    public class RumpunIlmu2Test : BaseIntegrationTest
    {
        public RumpunIlmu2Test(IntegrationTestWebAppFactory factory) : base(factory)
        {
        }

        public static IEnumerable<object[]> InvalidData()
        {
            var valid = Guid.NewGuid().ToString();
            var rumpunIlmu1 = Guid.NewGuid().ToString();
            var empty = "";

            // CREATE
            yield return new object[] { empty, "", rumpunIlmu1, "'Nama' tidak boleh kosong.", "created" };
            yield return new object[] { valid, "tes", empty, "'RumpunIlmu1' tidak boleh kosong.", "created" };
            yield return new object[] { valid, "tes", "no-guid", "'RumpunIlmu1' harus dalam format UUID v4 yang valid.", "created" };

            // UPDATE
            yield return new object[] { empty, "tes", rumpunIlmu1, "'Uuid' tidak boleh kosong.", "updated" };
            yield return new object[] { "no-guid", "tes", rumpunIlmu1, "'Uuid' harus dalam format UUID v4 yang valid.", "updated" };
            yield return new object[] { valid, "", rumpunIlmu1, "'Nama' tidak boleh kosong.", "updated" };
            yield return new object[] { valid, "tes", empty, "'RumpunIlmu1' tidak boleh kosong.", "updated" };
            yield return new object[] { valid, "tes", "no-guid", "'RumpunIlmu1' harus dalam format UUID v4 yang valid.", "updated" };

            // DELETE
            yield return new object[] { empty, "tes", rumpunIlmu1, "'Uuid' tidak boleh kosong.", "deleted" };
            yield return new object[] { "no-guid", "tes", rumpunIlmu1, "'Uuid' harus dalam format UUID v4 yang valid.", "deleted" };
        }

        public static IEnumerable<object?[]> ValidData()
        {
            var rumpunIlmu1 = Guid.NewGuid().ToString();

            yield return new object?[] { new object[] { "tes", rumpunIlmu1 }, null, "created" };
            yield return new object?[] { new object[] { "tes", rumpunIlmu1 }, new object[] { "tes2", rumpunIlmu1 }, "updated" };
            yield return new object?[] { new object[] { "tes", rumpunIlmu1 }, null, "deleted" };
        }

        [Theory]
        [MemberData(nameof(InvalidData))]
        public async Task CreateUpdateDelete_ShouldThrow_WhenInvalidFluentValidation(
            string uuid,
            string nama,
            string rumpunIlmu1,
            string message,
            string mode)
        {
            Result? result = null;

            if (mode == "created")
            {
                var command = new CreateRumpunIlmu2Command(nama, rumpunIlmu1);
                result = await Sender.Send(command);
            }
            else if (mode == "updated")
            {
                var command = new UpdateRumpunIlmu2Command(uuid, nama, rumpunIlmu1);
                result = await Sender.Send(command);
            }
            else // deleted
            {
                var command = new DeleteRumpunIlmu2Command(uuid);
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
            var rumpunIlmu1Before = (string)beforeData[1];

            var rumpunIlmu1Mock = new Mock<IRumpunIlmu1Api>();
            rumpunIlmu1Mock.Setup(x => x.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new RumpunIlmu1Response("1", Guid.NewGuid().ToString(), rumpunIlmu1Before));

            // CREATE
            using (var scope = Factory.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                var handler = new CreateRumpunIlmu2CommandHandler(
                    services.GetRequiredService<IRumpunIlmu2Repository>(),
                    rumpunIlmu1Mock.Object,
                    services.GetRequiredService<IUnitOfWork>()
                );

                var command = new CreateRumpunIlmu2Command(namaBefore, rumpunIlmu1Before);

                // Act
                var createResult = await handler.Handle(command, CancellationToken.None);

                // Assert
                Assert.True(createResult.IsSuccess);
                var newUuid = createResult.Value.ToString();

                // UPDATE / DELETE
                if (mode == "updated")
                {
                    var namaAfter = (string)afterData![0];
                    var rumpunIlmu1After = (string)afterData[1];

                    var handlerUpdate = new UpdateRumpunIlmu2CommandHandler(
                        services.GetRequiredService<IRumpunIlmu2Repository>(),
                        rumpunIlmu1Mock.Object,
                        services.GetRequiredService<IUnitOfWork>()
                    );

                    var updateCommand = new UpdateRumpunIlmu2Command(newUuid, namaAfter, rumpunIlmu1After);

                    // Act
                    var updateResult = await handlerUpdate.Handle(updateCommand, CancellationToken.None);

                    Assert.True(updateResult.IsSuccess);

                    var dataUpdate = DBContext.RumpunIlmu2.FirstOrDefault(p => p.Uuid.ToString() == newUuid);
                    Assert.NotNull(dataUpdate);
                    Assert.Equal(namaAfter, dataUpdate.Nama);
                }
                else if (mode == "deleted")
                {
                    var deleteCommand = new DeleteRumpunIlmu2Command(newUuid);
                    var deleteResult = await Sender.Send(deleteCommand);

                    Assert.True(deleteResult.IsSuccess);
                    var dataDeleted = DBContext.RumpunIlmu2.FirstOrDefault(p => p.Uuid.ToString() == newUuid);
                    Assert.Null(dataDeleted);
                }
            }
        }
        [Fact]
        public async Task Create_ShouldThrow_WhenRumpunIlmu1NotExist()
        {
            var namaBefore = "tes";
            var rumpunIlmu1Before = Guid.NewGuid().ToString();

            var rumpunIlmu1Mock = new Mock<IRumpunIlmu1Api>();
            rumpunIlmu1Mock.Setup(x => x.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new RumpunIlmu1Response("1", Guid.NewGuid().ToString(), "rumpun1"));

            // CREATE
            using (var scope = Factory.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                var handler = new CreateRumpunIlmu2CommandHandler(
                    services.GetRequiredService<IRumpunIlmu2Repository>(),
                    rumpunIlmu1Mock.Object,
                    services.GetRequiredService<IUnitOfWork>()
                );

                var command = new CreateRumpunIlmu2Command(namaBefore, rumpunIlmu1Before);

                // Act
                var createResult = await handler.Handle(command, CancellationToken.None);

                // Assert
                Assert.True(createResult.IsFailure);
                Assert.Equal("RumpunIlmu2.RumpunIlmu1NotFound", createResult.Error.Code);
            }
        }
        [Fact]
        public async Task Create_ShouldThrow_WhenDomainRule()
        {
            var namaBefore = "tes";
            var rumpunIlmu1Before = Guid.NewGuid().ToString();

            var rumpunIlmu1Mock = new Mock<IRumpunIlmu1Api>();
            rumpunIlmu1Mock.Setup(x => x.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new RumpunIlmu1Response("0", rumpunIlmu1Before, "rumpun1"));

            // CREATE
            using (var scope = Factory.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                var handler = new CreateRumpunIlmu2CommandHandler(
                    services.GetRequiredService<IRumpunIlmu2Repository>(),
                    rumpunIlmu1Mock.Object,
                    services.GetRequiredService<IUnitOfWork>()
                );

                var command = new CreateRumpunIlmu2Command(namaBefore, rumpunIlmu1Before);

                // Act
                var createResult = await handler.Handle(command, CancellationToken.None);

                // Assert
                Assert.True(createResult.IsFailure);
                Assert.Equal("RumpunIlmu2.UnknownRumpunIlmu1", createResult.Error.Code);
            }
        }

        [Fact]
        public async Task Update_ShouldThrow_WhenNotExist()
        {
            var namaBefore = "tes";
            var rumpunIlmu1Before = Guid.NewGuid().ToString();

            var rumpunIlmu1Mock = new Mock<IRumpunIlmu1Api>();
            rumpunIlmu1Mock.Setup(x => x.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new RumpunIlmu1Response("1", rumpunIlmu1Before, "rumpun1"));

            // CREATE
            using (var scope = Factory.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                var handler = new CreateRumpunIlmu2CommandHandler(
                    services.GetRequiredService<IRumpunIlmu2Repository>(),
                    rumpunIlmu1Mock.Object,
                    services.GetRequiredService<IUnitOfWork>()
                );

                var command = new CreateRumpunIlmu2Command(namaBefore, rumpunIlmu1Before);

                // Act
                var createResult = await handler.Handle(command, CancellationToken.None);

                // Assert
                Assert.True(createResult.IsSuccess);
                //var newUuid = createResult.Value.ToString();

                // UPDATE / DELETE
                    var wrongUuid = Guid.NewGuid().ToString();

                    var handlerUpdate = new UpdateRumpunIlmu2CommandHandler(
                        services.GetRequiredService<IRumpunIlmu2Repository>(),
                        rumpunIlmu1Mock.Object,
                        services.GetRequiredService<IUnitOfWork>()
                    );

                    var updateCommand = new UpdateRumpunIlmu2Command(wrongUuid, namaBefore, rumpunIlmu1Before);

                    // Act
                    var updateResult = await handlerUpdate.Handle(updateCommand, CancellationToken.None);

                    Assert.True(updateResult.IsFailure);
                    Assert.Equal("RumpunIlmu2.NotFound", updateResult.Error.Code);
            }
        }
        [Fact]
        public async Task Update_ShouldThrow_WhenDomainRule()
        {
            var namaBefore = "tes";
            var rumpunIlmu1Before = Guid.NewGuid().ToString();

            var rumpunIlmu1Mock = new Mock<IRumpunIlmu1Api>();
            rumpunIlmu1Mock.Setup(x => x.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new RumpunIlmu1Response("1", rumpunIlmu1Before, "rumpun1"));

            var rumpunIlmu1WrongMock = new Mock<IRumpunIlmu1Api>();
            rumpunIlmu1WrongMock.Setup(x => x.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new RumpunIlmu1Response("0", rumpunIlmu1Before, "rumpun1"));

            // CREATE
            using (var scope = Factory.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                var handler = new CreateRumpunIlmu2CommandHandler(
                    services.GetRequiredService<IRumpunIlmu2Repository>(),
                    rumpunIlmu1Mock.Object,
                    services.GetRequiredService<IUnitOfWork>()
                );

                var command = new CreateRumpunIlmu2Command(namaBefore, rumpunIlmu1Before);

                // Act
                var createResult = await handler.Handle(command, CancellationToken.None);

                // Assert
                Assert.True(createResult.IsSuccess);
                var newUuid = createResult.Value.ToString();

                // UPDATE / DELETE
                var handlerUpdate = new UpdateRumpunIlmu2CommandHandler(
                    services.GetRequiredService<IRumpunIlmu2Repository>(),
                    rumpunIlmu1WrongMock.Object,
                    services.GetRequiredService<IUnitOfWork>()
                );

                var updateCommand = new UpdateRumpunIlmu2Command(newUuid, namaBefore, rumpunIlmu1Before);

                // Act
                var updateResult = await handlerUpdate.Handle(updateCommand, CancellationToken.None);

                Assert.True(updateResult.IsFailure);
                Assert.Equal("RumpunIlmu2.UnknownRumpunIlmu1", updateResult.Error.Code);
            }
        }

        [Fact]
        public async Task Update_ShouldThrow_WhenRumpunIlmu1NotExist()
        {
            var guid = Guid.NewGuid().ToString();
            var nama = "tes";
            var rumpunIlmu1 = Guid.NewGuid().ToString();

            var rumpunIlmu1Mock = new Mock<IRumpunIlmu1Api>();
            rumpunIlmu1Mock.Setup(x => x.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new RumpunIlmu1Response("1", Guid.NewGuid().ToString(), "rumpun1"));

            using (var scope = Factory.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                var handlerUpdate = new UpdateRumpunIlmu2CommandHandler(
                    services.GetRequiredService<IRumpunIlmu2Repository>(),
                    rumpunIlmu1Mock.Object,
                    services.GetRequiredService<IUnitOfWork>()
                );

                var updateCommand = new UpdateRumpunIlmu2Command(guid, nama, rumpunIlmu1);

                // Act
                var updateResult = await handlerUpdate.Handle(updateCommand, CancellationToken.None);

                Assert.True(updateResult.IsFailure);
                Assert.Equal("RumpunIlmu2.RumpunIlmu1NotFound", updateResult.Error.Code);
            }
        }

        [Fact]
        public async Task Delete_ShouldThrow_WhenNotExist()
        {
            var guid = Guid.NewGuid().ToString();

            var command = new DeleteRumpunIlmu2Command(guid);
            var result = await Sender.Send(command);

            Assert.True(result.IsFailure);
            Assert.Equal("RumpunIlmu2.NotFound", result.Error.Code);
        }
    }
}
