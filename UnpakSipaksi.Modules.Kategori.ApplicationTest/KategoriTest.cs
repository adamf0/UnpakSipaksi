using Dapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Moq.Dapper;
using System.Data;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.Kategori.Application.CreateKategori;
using UnpakSipaksi.Modules.Kategori.Application.DeleteKategori;
using UnpakSipaksi.Modules.Kategori.Application.GetAllKategori;
using UnpakSipaksi.Modules.Kategori.Application.GetKategori;
using UnpakSipaksi.Modules.Kategori.Application.UpdateKategori;
using Xunit;

namespace Application.Integration.Tests
{
    public class KategoriTest : BaseIntegrationTest
    {
        public KategoriTest(IntegrationTestWebAppFactory factory) : base(factory)
        {

        }

        public static IEnumerable<object[]> InvalidData()
        {
            var valid = Guid.NewGuid().ToString();
            var empty = "";

            // CREATE
            yield return new object[] { empty, "", "'Nama' tidak boleh kosong.", "created" };

            // UPDATE
            yield return new object[] { valid, "", "'Nama' tidak boleh kosong.", "updated" };
            yield return new object[] { empty, "tes", "'Uuid' tidak boleh kosong.", "updated" };
            yield return new object[] { "no-guid", "tes", "'Uuid' harus dalam format UUID v4 yang valid.", "updated" };

            // DELETE
            yield return new object[] { empty, "tes", "'Uuid' tidak boleh kosong.", "deleted" };
            yield return new object[] { "no-guid", "tes", "'Uuid' harus dalam format UUID v4 yang valid.", "deleted" };
        }

        public static IEnumerable<object?[]> ValidData()
        {
            yield return new object?[] { new object[] { "tes" }, null, "created" };
            yield return new object?[] { new object[] { "tes" }, new object[] { "tes2" }, "updated" };
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
                var command = new CreateKategoriCommand(nama);
                result = await Sender.Send(command);
            }
            else if (mode == "updated")
            {
                var command = new UpdateKategoriCommand(uuid, nama);
                result = await Sender.Send(command);
            }
            else
            {
                var command = new DeleteKategoriCommand(uuid);
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
            // --- CREATE ---
            var namaBefore = (string)beforeData[0];

            var createCommand = new CreateKategoriCommand(namaBefore);
            var createResult = await Sender.Send(createCommand);

            Assert.True(createResult.IsSuccess);
            var dataCreate = DBContext.Kategori.FirstOrDefault(p => p.Uuid == createResult!.Value);

            Assert.NotNull(dataCreate);
            Assert.Equal(namaBefore, dataCreate.Nama);

            using (var scope = Factory.Services.CreateScope())
            {
                var handler = scope.ServiceProvider
                    .GetService<IRequestHandler<CreateKategoriCommand, Result<Guid>>>();

                Assert.NotNull(handler);
                Assert.IsType<CreateKategoriCommandHandler>(handler);
            }

            var newUuid = createResult.Value.ToString();

            // --- UPDATE / DELETE ---
            if (mode == "updated")
            {
                var namaAfter = (string)afterData![0];

                var updateCommand = new UpdateKategoriCommand(newUuid, namaAfter);
                var updateResult = await Sender.Send(updateCommand);

                Assert.True(updateResult.IsSuccess);
                var dataUpdate = DBContext.Kategori.FirstOrDefault(p => p.Uuid == createResult!.Value);

                Assert.NotNull(dataUpdate);
                Assert.Equal(namaAfter, dataUpdate.Nama);

                using (var scope = Factory.Services.CreateScope())
                {
                    var handler = scope.ServiceProvider
                        .GetService<IRequestHandler<UpdateKategoriCommand, Result>>();

                    Assert.NotNull(handler);
                    Assert.IsType<UpdateKategoriCommandHandler>(handler);
                }
            }
            else if (mode == "deleted")
            {
                var deleteCommand = new DeleteKategoriCommand(newUuid);
                var deleteResult = await Sender.Send(deleteCommand);

                Assert.True(deleteResult.IsSuccess);
            }
        }

        [Fact]
        public async Task Update_ShouldThrow_WhenNotExist()
        {
            var guid = Guid.NewGuid().ToString();
            var namaBefore = "tes";

            var updateCommand = new UpdateKategoriCommand(guid, namaBefore);
            var updateResult = await Sender.Send(updateCommand);

            Assert.True(updateResult.IsFailure);
            Assert.Equal("Kategori.NotFound", updateResult.Error.Code);
        }

        [Fact]
        public async Task Delete_ShouldThrow_WhenNotExist()
        {
            var guid = Guid.NewGuid().ToString();

            var deleteCommand = new DeleteKategoriCommand(guid);
            var deleteResult = await Sender.Send(deleteCommand);

            Assert.True(deleteResult.IsFailure);
            Assert.Equal("Kategori.NotFound", deleteResult.Error.Code);
        }

        [Fact]
        public async Task Handle_All_ReturnsSuccess_WhenDataExists()
        {
            // Arrange
            var mockConnectionFactory = new Mock<IDbConnectionFactory>();
            var mockConnection = new Mock<DbConnection>();

            var fakeDataList = new List<KategoriResponse>
            {
                new() { Uuid = Guid.NewGuid().ToString(), Nama = "Kategori 1" },
                new() { Uuid = Guid.NewGuid().ToString(), Nama = "Kategori 2" }
            };

            mockConnection.SetupDapperAsync(c =>
                c.QueryAsync<KategoriResponse>(
                    It.IsAny<string>(),
                    It.IsAny<object>(),
                    It.IsAny<IDbTransaction>(),
                    It.IsAny<int?>(),
                    It.IsAny<System.Data.CommandType?>()
                )
            ).ReturnsAsync(fakeDataList);

            mockConnectionFactory.Setup(f => f.OpenConnectionAsync())
                .ReturnsAsync(mockConnection.Object);

            var handler = new GetAllKategoriQueryHandler(mockConnectionFactory.Object);
            var query = new GetAllKategoriQuery();

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotEmpty(result.Value);
            Assert.Equal(fakeDataList[0].Nama, result.Value[0].Nama);
        }

        [Fact]
        public async Task Handle_All_ReturnsFailure_WhenNoData()
        {
            // Arrange
            var mockConnectionFactory = new Mock<IDbConnectionFactory>();
            var mockConnection = new Mock<DbConnection>();

            mockConnection.SetupDapperAsync(c =>
                c.QueryAsync<KategoriResponse>(
                    It.IsAny<string>(),
                    It.IsAny<object>(),
                    It.IsAny<IDbTransaction>(),
                    It.IsAny<int?>(),
                    It.IsAny<System.Data.CommandType?>()
                )
            ).ReturnsAsync(new List<KategoriResponse>());

            mockConnectionFactory.Setup(f => f.OpenConnectionAsync())
                .ReturnsAsync(mockConnection.Object);

            var handler = new GetAllKategoriQueryHandler(mockConnectionFactory.Object);
            var query = new GetAllKategoriQuery();

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
        }

        [Fact]
        public async Task Handle_Default_ReturnsSuccess_WhenDataExists()
        {
            // Arrange
            var mockConnectionFactory = new Mock<IDbConnectionFactory>();
            var mockConnection = new Mock<DbConnection>();
            var uuid = Guid.NewGuid();

            var fakeData = new KategoriDefaultResponse
            {
                Id = "1",
                Uuid = uuid.ToString(),
                Nama = "Kategori Default"
            };

            mockConnection.SetupDapperAsync(c =>
                c.QuerySingleOrDefaultAsync<KategoriDefaultResponse>(
                    It.IsAny<string>(),
                    It.IsAny<object>(),
                    It.IsAny<IDbTransaction>(),
                    It.IsAny<int?>(),
                    It.IsAny<System.Data.CommandType?>()
                )
            ).ReturnsAsync(fakeData);

            mockConnectionFactory.Setup(f => f.OpenConnectionAsync())
                .ReturnsAsync(mockConnection.Object);

            var handler = new GetKategoriDefaultQueryHandler(mockConnectionFactory.Object);
            var query = new GetKategoriDefaultQuery(uuid);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(fakeData.Nama, result.Value.Nama);
        }

        [Fact]
        public async Task Handle_Default_ReturnsFailure_WhenDataNotFound()
        {
            // Arrange
            var mockConnectionFactory = new Mock<IDbConnectionFactory>();
            var mockConnection = new Mock<DbConnection>();

            mockConnection.SetupDapperAsync(c =>
                c.QuerySingleOrDefaultAsync<KategoriDefaultResponse>(
                    It.IsAny<string>(),
                    It.IsAny<object>(),
                    It.IsAny<IDbTransaction>(),
                    It.IsAny<int?>(),
                    It.IsAny<System.Data.CommandType?>()
                )
            ).ReturnsAsync((KategoriDefaultResponse?)null);

            mockConnectionFactory.Setup(f => f.OpenConnectionAsync())
                .ReturnsAsync(mockConnection.Object);

            var handler = new GetKategoriDefaultQueryHandler(mockConnectionFactory.Object);
            var query = new GetKategoriDefaultQuery(Guid.NewGuid());

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
        }

        [Fact]
        public async Task Handle_ReturnsSuccess_WhenDataExists()
        {
            // Arrange
            var mockConnectionFactory = new Mock<IDbConnectionFactory>();
            var mockConnection = new Mock<DbConnection>();
            var uuid = Guid.NewGuid();

            var fakeData = new KategoriResponse
            {
                Uuid = uuid.ToString(),
                Nama = "Kategori 1"
            };

            mockConnection.SetupDapperAsync(c =>
                c.QuerySingleOrDefaultAsync<KategoriResponse>(
                    It.IsAny<string>(),
                    It.IsAny<object>(),
                    It.IsAny<IDbTransaction>(),
                    It.IsAny<int?>(),
                    It.IsAny<System.Data.CommandType?>()
                )
            ).ReturnsAsync(fakeData);

            mockConnectionFactory.Setup(f => f.OpenConnectionAsync())
                .ReturnsAsync(mockConnection.Object);

            var handler = new GetKategoriQueryHandler(mockConnectionFactory.Object);
            var query = new GetKategoriQuery(uuid.ToString());

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(fakeData.Nama, result.Value.Nama);
        }

        [Fact]
        public async Task Handle_ReturnsFailure_WhenDataNotFound()
        {
            // Arrange
            var mockConnectionFactory = new Mock<IDbConnectionFactory>();
            var mockConnection = new Mock<DbConnection>();

            mockConnection.SetupDapperAsync(c =>
                c.QuerySingleOrDefaultAsync<KategoriResponse>(
                    It.IsAny<string>(),
                    It.IsAny<object>(),
                    It.IsAny<IDbTransaction>(),
                    It.IsAny<int?>(),
                    It.IsAny<System.Data.CommandType?>()
                )
            ).ReturnsAsync((KategoriResponse?)null);

            mockConnectionFactory.Setup(f => f.OpenConnectionAsync())
                .ReturnsAsync(mockConnection.Object);

            var handler = new GetKategoriQueryHandler(mockConnectionFactory.Object);
            var query = new GetKategoriQuery(Guid.NewGuid().ToString());

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
        }
    }
}
