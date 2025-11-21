using Dapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Moq.Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KategoriTkt.Application.CreateKategoriTkt;
using UnpakSipaksi.Modules.KategoriTkt.Application.DeleteKategoriTkt;
using UnpakSipaksi.Modules.KategoriTkt.Application.GetAllKategoriTkt;
using UnpakSipaksi.Modules.KategoriTkt.Application.GetBobotKategoriTkt;
using UnpakSipaksi.Modules.KategoriTkt.Application.GetKategoriTkt;
using UnpakSipaksi.Modules.KategoriTkt.Application.UpdateKategoriTkt;
using Xunit;

namespace UnpakSipaksi.Modules.KategoriTkt.ApplicationTest
{
    public class KategoriTktTest : BaseIntegrationTest
    {
        public KategoriTktTest(IntegrationTestWebAppFactory factory) : base(factory)
        {
        }

        public static IEnumerable<object[]> InvalidData()
        {
            var validUuid = Guid.NewGuid().ToString();
            var empty = "";

            // CREATE
            yield return new object[] { validUuid, "", "'Nama' tidak boleh kosong.", "created" };

            // UPDATE
            yield return new object[] { validUuid, "", "'Nama' tidak boleh kosong.", "updated" };
            yield return new object[] { empty, "tes", "'Uuid' tidak boleh kosong.", "updated" };
            yield return new object[] { "no-guid", "tes", "'Uuid' harus dalam format UUID v4 yang valid.", "updated" };

            // DELETE
            yield return new object[] { empty, "tes", "'Uuid' tidak boleh kosong.", "deleted" };
            yield return new object[] { "no-guid", "tes", "'Uuid' harus dalam format UUID v4 yang valid.", "deleted" };
        }

        public static IEnumerable<object?[]> ValidData()
        {
            var validUuid = Guid.NewGuid().ToString();
            yield return new object?[] { new object[] { "tes" }, null, "created" };
            yield return new object?[] { new object[] { "tes" }, new object[] { "tes updated" }, "updated" };
            yield return new object?[] { new object[] { "tes" }, null, "deleted" };
        }

        [Theory]
        [MemberData(nameof(InvalidData))]
        public async Task CreateUpdateDelete_ShouldThrow_WhenInvalidFluentValidation(
            string uuid,
            string nama,
            string message,
            string mode)
        {
            Result? result = null;

            if (mode == "created")
            {
                var command = new CreateKategoriTktCommand(nama);
                result = await Sender.Send(command);
            }
            else if (mode == "updated")
            {
                var command = new UpdateKategoriTktCommand(uuid, nama);
                result = await Sender.Send(command);
            }
            else
            {
                var command = new DeleteKategoriTktCommand(uuid);
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

            // --- CREATE ---
            var createCommand = new CreateKategoriTktCommand(namaBefore);
            var createResult = await Sender.Send(createCommand);

            Assert.True(createResult.IsSuccess);
            var dataCreate = DBContext.KategoriTkt.FirstOrDefault(p => p.Uuid == createResult!.Value);
            Assert.NotNull(dataCreate);
            Assert.Equal(namaBefore, dataCreate.Nama);

            using (var scope = Factory.Services.CreateScope())
            {
                var handler = scope.ServiceProvider
                    .GetService<IRequestHandler<CreateKategoriTktCommand, Result<Guid>>>();
                Assert.NotNull(handler);
                Assert.IsType<CreateKategoriTktCommandHandler>(handler);
            }

            var uuid = createResult.Value.ToString();

            // --- UPDATE ---
            if (mode == "updated")
            {
                var namaAfter = (string)afterData![0];
                var updateCommand = new UpdateKategoriTktCommand(uuid, namaAfter);
                var updateResult = await Sender.Send(updateCommand);

                Assert.True(updateResult.IsSuccess);
                var dataUpdate = DBContext.KategoriTkt.FirstOrDefault(p => p.Uuid.ToString() == uuid);
                Assert.NotNull(dataUpdate);
                Assert.Equal(namaAfter, dataUpdate.Nama);

                using (var scope = Factory.Services.CreateScope())
                {
                    var handler = scope.ServiceProvider
                        .GetService<IRequestHandler<UpdateKategoriTktCommand, Result>>();
                    Assert.NotNull(handler);
                    Assert.IsType<UpdateKategoriTktCommandHandler>(handler);
                }
            }

            // --- DELETE ---
            if (mode == "deleted")
            {
                var deleteCommand = new DeleteKategoriTktCommand(uuid);
                var deleteResult = await Sender.Send(deleteCommand);

                Assert.True(deleteResult.IsSuccess);
                var dataDeleted = DBContext.KategoriTkt.FirstOrDefault(p => p.Uuid.ToString() == uuid);
                Assert.Null(dataDeleted);

                using (var scope = Factory.Services.CreateScope())
                {
                    var handler = scope.ServiceProvider
                        .GetService<IRequestHandler<DeleteKategoriTktCommand, Result>>();
                    Assert.NotNull(handler);
                    Assert.IsType<DeleteKategoriTktCommandHandler>(handler);
                }
            }
        }

        [Fact]
        public async Task Update_ShouldThrow_WhenNotExist()
        {
            var guid = Guid.NewGuid().ToString();
            var namaBefore = "tes";

            var updateCommand = new UpdateKategoriTktCommand(guid, namaBefore);
            var updateResult = await Sender.Send(updateCommand);

            Assert.True(updateResult.IsFailure);
            Assert.Equal("KategoriTkt.NotFound", updateResult.Error.Code);
        }

        [Fact]
        public async Task Delete_ShouldThrow_WhenNotExist()
        {
            var guid = Guid.NewGuid().ToString();

            var deleteCommand = new DeleteKategoriTktCommand(guid);
            var deleteResult = await Sender.Send(deleteCommand);

            Assert.True(deleteResult.IsFailure);
            Assert.Equal("KategoriTkt.NotFound", deleteResult.Error.Code);
        }

        [Fact]
        public async Task GetAllKategoriTkt_Handle_ReturnsList_WhenDataExists()
        {
            // Arrange
            var mockConnectionFactory = new Mock<IDbConnectionFactory>();
            var mockConnection = new Mock<DbConnection>();

            var fakeData = new List<KategoriTktResponse>
            {
                new KategoriTktResponse { Uuid = Guid.NewGuid().ToString(), Nama = "TKT 1" }
            };

            mockConnection.SetupDapperAsync(c =>
                c.QueryAsync<KategoriTktResponse>(It.IsAny<string>(), null, null, null, null))
                .ReturnsAsync(fakeData);

            mockConnectionFactory.Setup(f => f.OpenConnectionAsync())
                .Returns(new ValueTask<DbConnection>(mockConnection.Object));

            var handler = new GetAllKategoriTktQueryHandler(mockConnectionFactory.Object);
            var query = new GetAllKategoriTktQuery();

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotEmpty(result.Value);
            Assert.Equal("TKT 1", result.Value.First().Nama);
        }

        [Fact]
        public async Task GetAllKategoriTkt_Handle_ReturnsFailure_WhenNoData()
        {
            // Arrange
            var mockConnectionFactory = new Mock<IDbConnectionFactory>();
            var mockConnection = new Mock<DbConnection>();

            mockConnection.SetupDapperAsync(c =>
                c.QueryAsync<KategoriTktResponse>(It.IsAny<string>(), null, null, null, null))
                .ReturnsAsync(new List<KategoriTktResponse>());

            mockConnectionFactory.Setup(f => f.OpenConnectionAsync())
                .Returns(new ValueTask<DbConnection>(mockConnection.Object));

            var handler = new GetAllKategoriTktQueryHandler(mockConnectionFactory.Object);
            var query = new GetAllKategoriTktQuery();

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
        }

        [Fact]
        public async Task GetKategoriTkt_Handle_ReturnsSuccess_WhenDataExists()
        {
            // Arrange
            var mockConnectionFactory = new Mock<IDbConnectionFactory>();
            var mockConnection = new Mock<DbConnection>();
            var uuid = Guid.NewGuid();

            var fakeData = new KategoriTktResponse { Uuid = uuid.ToString(), Nama = "TKT 1" };

            mockConnection.SetupDapperAsync(c =>
                c.QuerySingleOrDefaultAsync<KategoriTktResponse?>(It.IsAny<string>(), It.IsAny<object>(), null, null, null))
                .ReturnsAsync(fakeData);

            mockConnectionFactory.Setup(f => f.OpenConnectionAsync())
                .Returns(new ValueTask<DbConnection>(mockConnection.Object));

            var handler = new GetKategoriTktQueryHandler(mockConnectionFactory.Object);
            var query = new GetKategoriTktQuery(uuid);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal("TKT 1", result.Value.Nama);
        }

        [Fact]
        public async Task GetKategoriTkt_Handle_ReturnsFailure_WhenDataNotFound()
        {
            // Arrange
            var mockConnectionFactory = new Mock<IDbConnectionFactory>();
            var mockConnection = new Mock<DbConnection>();

            mockConnection.SetupDapperAsync(c =>
                c.QuerySingleOrDefaultAsync<KategoriTktResponse?>(It.IsAny<string>(), It.IsAny<object>(), null, null, null))
                .ReturnsAsync((KategoriTktResponse?)null);

            mockConnectionFactory.Setup(f => f.OpenConnectionAsync())
                .Returns(new ValueTask<DbConnection>(mockConnection.Object));

            var handler = new GetKategoriTktQueryHandler(mockConnectionFactory.Object);
            var query = new GetKategoriTktQuery(Guid.NewGuid());

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
        }

        [Fact]
        public async Task GetKategoriTktDefault_Handle_ReturnsSuccess_WhenDataExists()
        {
            // Arrange
            var mockConnectionFactory = new Mock<IDbConnectionFactory>();
            var mockConnection = new Mock<DbConnection>();
            var uuid = Guid.NewGuid();

            var fakeData = new KategoriTktDefaultResponse { Id = "1", Uuid = uuid.ToString(), Nama = "TKT Default" };

            mockConnection.SetupDapperAsync(c =>
                c.QuerySingleOrDefaultAsync<KategoriTktDefaultResponse?>(It.IsAny<string>(), It.IsAny<object>(), null, null, null))
                .ReturnsAsync(fakeData);

            mockConnectionFactory.Setup(f => f.OpenConnectionAsync())
                .Returns(new ValueTask<DbConnection>(mockConnection.Object));

            var handler = new GetKategoriTktDefaultQueryHandler(mockConnectionFactory.Object);
            var query = new GetKategoriTktDefaultQuery(uuid);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal("TKT Default", result.Value.Nama);
        }

        [Fact]
        public async Task GetKategoriTktDefault_Handle_ReturnsFailure_WhenDataNotFound()
        {
            // Arrange
            var mockConnectionFactory = new Mock<IDbConnectionFactory>();
            var mockConnection = new Mock<DbConnection>();

            mockConnection.SetupDapperAsync(c =>
                c.QuerySingleOrDefaultAsync<KategoriTktDefaultResponse?>(It.IsAny<string>(), It.IsAny<object>(), null, null, null))
                .ReturnsAsync((KategoriTktDefaultResponse?)null);

            mockConnectionFactory.Setup(f => f.OpenConnectionAsync())
                .Returns(new ValueTask<DbConnection>(mockConnection.Object));

            var handler = new GetKategoriTktDefaultQueryHandler(mockConnectionFactory.Object);
            var query = new GetKategoriTktDefaultQuery(Guid.NewGuid());

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
        }

        [Fact]
        public async Task GetBobotKategoriTkt_Handle_ReturnsSuccess_WhenSingleValueExists()
        {
            // Arrange
            var mockConnectionFactory = new Mock<IDbConnectionFactory>();
            var mockConnection = new Mock<DbConnection>();
            string kategoriSkema = "Penelitian Dasar";

            var fakeValue = new List<int> { 10 };

            mockConnection.SetupDapperAsync(c =>
                c.QueryAsync<int>(It.IsAny<string>(), null, null, null, null))
                .ReturnsAsync(fakeValue);

            mockConnectionFactory.Setup(f => f.OpenConnectionAsync())
                .Returns(new ValueTask<DbConnection>(mockConnection.Object));

            var handler = new GetBobotKategoriTktQueryHandler(mockConnectionFactory.Object);
            var query = new GetBobotKategoriTktQuery(kategoriSkema);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(10, result.Value);
        }

        [Fact]
        public async Task GetBobotKategoriTkt_Handle_ReturnsFailure_WhenMultipleValues()
        {
            // Arrange
            var mockConnectionFactory = new Mock<IDbConnectionFactory>();
            var mockConnection = new Mock<DbConnection>();

            var fakeValue = new List<int> { 10, 20 };

            mockConnection.SetupDapperAsync(c =>
                c.QueryAsync<int>(It.IsAny<string>(), null, null, null, null))
                .ReturnsAsync(fakeValue);

            mockConnectionFactory.Setup(f => f.OpenConnectionAsync())
                .Returns(new ValueTask<DbConnection>(mockConnection.Object));

            var handler = new GetBobotKategoriTktQueryHandler(mockConnectionFactory.Object);
            var query = new GetBobotKategoriTktQuery("Penelitian Dasar");

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
        }

        [Fact]
        public async Task GetBobotKategoriTkt_Handle_ReturnsFailure_WhenUnknownKategoriSkema()
        {
            // Arrange
            var mockConnectionFactory = new Mock<IDbConnectionFactory>();
            var handler = new GetBobotKategoriTktQueryHandler(mockConnectionFactory.Object);
            var query = new GetBobotKategoriTktQuery("Unknown");

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
        }
    }
}
