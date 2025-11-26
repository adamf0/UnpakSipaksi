using Dapper;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Moq.Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.RumpunIlmu1.Domain.RumpunIlmu1;
using UnpakSipaksi.Modules.RumpunIlmu1.PublicApi;
using UnpakSipaksi.Modules.RumpunIlmu2.Application.Abstractions.Data;
using UnpakSipaksi.Modules.RumpunIlmu2.Application.CreateRumpunIlmu2;
using UnpakSipaksi.Modules.RumpunIlmu2.Application.DeleteRumpunIlmu2;
using UnpakSipaksi.Modules.RumpunIlmu2.Application.GetAllRumpunIlmu2;
using UnpakSipaksi.Modules.RumpunIlmu2.Application.GetRumpunIlmu2;
using UnpakSipaksi.Modules.RumpunIlmu2.Application.UpdateRumpunIlmu2;
using UnpakSipaksi.Modules.RumpunIlmu2.Domain.RumpunIlmu2;
using Xunit;

namespace UnpakSipaksi.Modules.RumpunIlmu2.ApplicationTest
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
                .ReturnsAsync((RumpunIlmu1Response?)null);

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
                .ReturnsAsync((RumpunIlmu1Response?)null);

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
            // Arrange
            var uuid = Guid.NewGuid();

            // Mock repository
            var mockRepo = new Mock<IRumpunIlmu2Repository>();
            mockRepo.Setup(r => r.GetAsync(uuid, It.IsAny<CancellationToken>()))
                    .ReturnsAsync((Domain.RumpunIlmu2.RumpunIlmu2?)null);

            // Handler
            using (var scope = Factory.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                var handler = new DeleteRumpunIlmu2CommandHandler(
                    mockRepo.Object,
                    services.GetRequiredService<IUnitOfWork>()
                );

                var command = new DeleteRumpunIlmu2Command(uuid.ToString());

                // Act
                var result = await handler.Handle(command, CancellationToken.None);

                // Assert
                Assert.True(result.IsFailure);
                Assert.Equal("RumpunIlmu2.NotFound", result.Error.Code);
            }
        }

        [Fact]
        public async Task GetAllRumpunIlmu2_ReturnsSuccess_WhenDataExists()
        {
            // Arrange
            var mockConnectionFactory = new Mock<IDbConnectionFactory>();
            var mockConnection = new Mock<DbConnection>();

            var fakeData = new List<RumpunIlmu2Response>
        {
            new RumpunIlmu2Response { Uuid = Guid.NewGuid().ToString(), Nama = "Fokus 1", UuidRumpunIlmu1 = Guid.NewGuid().ToString() }
        };

            mockConnection.SetupDapperAsync(c => c.QueryAsync<RumpunIlmu2Response>(
                It.IsAny<string>(), null, null, null, null))
                .ReturnsAsync(fakeData);

            mockConnectionFactory.Setup(f => f.OpenConnectionAsync())
                .Returns(new ValueTask<DbConnection>(mockConnection.Object));

            var handler = new GetAllRumpunIlmu2QueryHandler(mockConnectionFactory.Object);
            var query = new GetAllRumpunIlmu2Query();

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotEmpty(result.Value);
            Assert.Equal(fakeData.First().Nama, result.Value.First().Nama);
        }

        [Fact]
        public async Task GetAllRumpunIlmu2_ReturnsFailure_WhenNoData()
        {
            // Arrange
            var mockConnectionFactory = new Mock<IDbConnectionFactory>();
            var mockConnection = new Mock<DbConnection>();

            mockConnection.SetupDapperAsync(c => c.QueryAsync<RumpunIlmu2Response>(
                It.IsAny<string>(), null, null, null, null))
                .ReturnsAsync(new List<RumpunIlmu2Response>());

            mockConnectionFactory.Setup(f => f.OpenConnectionAsync())
                .Returns(new ValueTask<DbConnection>(mockConnection.Object));

            var handler = new GetAllRumpunIlmu2QueryHandler(mockConnectionFactory.Object);
            var query = new GetAllRumpunIlmu2Query();

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
        }


        [Fact]
        public async Task GetRumpunIlmu2_ReturnsSuccess_WhenDataExists()
        {
            var mockConnectionFactory = new Mock<IDbConnectionFactory>();
            var mockConnection = new Mock<DbConnection>();
            var uuid = Guid.NewGuid();

            var fakeData = new RumpunIlmu2Response { Uuid = uuid.ToString(), Nama = "Fokus 1", UuidRumpunIlmu1 = Guid.NewGuid().ToString() };

            mockConnection.SetupDapperAsync(c => c.QuerySingleOrDefaultAsync<RumpunIlmu2Response>(
                It.IsAny<string>(), It.IsAny<object>(), null, null, null))
                .ReturnsAsync(fakeData);

            mockConnectionFactory.Setup(f => f.OpenConnectionAsync())
                .Returns(new ValueTask<DbConnection>(mockConnection.Object));

            var handler = new GetRumpunIlmu2QueryHandler(mockConnectionFactory.Object);
            var query = new GetRumpunIlmu2Query(uuid);

            var result = await handler.Handle(query, CancellationToken.None);

            Assert.True(result.IsSuccess);
            Assert.Equal(fakeData.Nama, result.Value.Nama);
        }

        [Fact]
        public async Task GetRumpunIlmu2_ReturnsFailure_WhenDataNotFound()
        {
            var mockConnectionFactory = new Mock<IDbConnectionFactory>();
            var mockConnection = new Mock<DbConnection>();

            mockConnection.SetupDapperAsync(c => c.QuerySingleOrDefaultAsync<RumpunIlmu2Response>(
                It.IsAny<string>(), It.IsAny<object>(), null, null, null))
                .ReturnsAsync((RumpunIlmu2Response?)null);

            mockConnectionFactory.Setup(f => f.OpenConnectionAsync())
                .Returns(new ValueTask<DbConnection>(mockConnection.Object));

            var handler = new GetRumpunIlmu2QueryHandler(mockConnectionFactory.Object);
            var query = new GetRumpunIlmu2Query(Guid.NewGuid());

            var result = await handler.Handle(query, CancellationToken.None);

            Assert.False(result.IsSuccess);
        }


        [Fact]
        public async Task GetRumpunIlmu2Default_ReturnsSuccess_WhenDataExists()
        {
            var mockConnectionFactory = new Mock<IDbConnectionFactory>();
            var mockConnection = new Mock<DbConnection>();

            var fakeData = new RumpunIlmu2DefaultResponse { Uuid = Guid.NewGuid().ToString(), Id = "1", Nama = "Fokus Default" };

            mockConnection.SetupDapperAsync(c => c.QuerySingleOrDefaultAsync<RumpunIlmu2DefaultResponse>(
                It.IsAny<string>(), It.IsAny<object>(), null, null, null))
                .ReturnsAsync(fakeData);

            mockConnectionFactory.Setup(f => f.OpenConnectionAsync())
                .Returns(new ValueTask<DbConnection>(mockConnection.Object));

            var handler = new GetRumpunIlmu2DefaultQueryHandler(mockConnectionFactory.Object);
            var query = new GetRumpunIlmu2DefaultQuery(Guid.Parse(fakeData.Uuid));

            var result = await handler.Handle(query, CancellationToken.None);

            Assert.True(result.IsSuccess);
            Assert.Equal(fakeData.Nama, result.Value.Nama);
        }

        [Fact]
        public async Task GetRumpunIlmu2Default_ReturnsFailure_WhenDataNotFound()
        {
            var mockConnectionFactory = new Mock<IDbConnectionFactory>();
            var mockConnection = new Mock<DbConnection>();

            mockConnection.SetupDapperAsync(c => c.QuerySingleOrDefaultAsync<RumpunIlmu2DefaultResponse>(
                It.IsAny<string>(), It.IsAny<object>(), null, null, null))
                .ReturnsAsync((RumpunIlmu2DefaultResponse?)null);

            mockConnectionFactory.Setup(f => f.OpenConnectionAsync())
                .Returns(new ValueTask<DbConnection>(mockConnection.Object));

            var handler = new GetRumpunIlmu2DefaultQueryHandler(mockConnectionFactory.Object);
            var query = new GetRumpunIlmu2DefaultQuery(Guid.NewGuid());

            var result = await handler.Handle(query, CancellationToken.None);

            Assert.False(result.IsSuccess);
        }
    }
}
