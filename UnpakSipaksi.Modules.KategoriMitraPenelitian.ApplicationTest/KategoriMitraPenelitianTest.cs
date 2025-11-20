using Dapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Moq;
using Moq.Dapper;
using System.Data;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.Kategori.Application.DeleteKategori;
using UnpakSipaksi.Modules.Kategori.Application.UpdateKategori;
using UnpakSipaksi.Modules.KategoriMitraPenelitian.Application.CreateKategoriMitraPenelitian;
using UnpakSipaksi.Modules.KategoriMitraPenelitian.Application.DeleteKategoriMitraPenelitian;
using UnpakSipaksi.Modules.KategoriMitraPenelitian.Application.GetAllKategoriMitraPenelitian;
using UnpakSipaksi.Modules.KategoriMitraPenelitian.Application.GetKategoriMitraPenelitian;
using UnpakSipaksi.Modules.KategoriMitraPenelitian.Application.UpdateKategoriMitraPenelitian;
using Xunit;

namespace UnpakSipaksi.Modules.KategoriMitraPenelitian.ApplicationTest
{
    public class KategoriMitraPenelitianTest : BaseIntegrationTest
    {
        public KategoriMitraPenelitianTest(IntegrationTestWebAppFactory factory) : base(factory)
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
                var command = new CreateKategoriMitraPenelitianCommand(nama);
                result = await Sender.Send(command);
            }
            else if (mode == "updated")
            {
                var command = new UpdateKategoriMitraPenelitianCommand(uuid, nama);
                result = await Sender.Send(command);
            }
            else
            {
                var command = new DeleteKategoriMitraPenelitianCommand(uuid);
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
            var createCommand = new CreateKategoriMitraPenelitianCommand(namaBefore);
            var createResult = await Sender.Send(createCommand);

            Assert.True(createResult.IsSuccess);
            var dataCreate = DBContext.KategoriMitraPenelitian.FirstOrDefault(p => p.Uuid == createResult!.Value);
            Assert.NotNull(dataCreate);
            Assert.Equal(namaBefore, dataCreate.Nama);

            // Assert handler
            using (var scope = Factory.Services.CreateScope())
            {
                var handler = scope.ServiceProvider
                    .GetService<IRequestHandler<CreateKategoriMitraPenelitianCommand, Result<Guid>>>();
                Assert.NotNull(handler);
                Assert.IsType<CreateKategoriMitraPenelitianCommandHandler>(handler);
            }

            var uuid = createResult.Value.ToString();

            // --- UPDATE ---
            if (mode == "updated")
            {
                var namaAfter = (string)afterData![0];
                var updateCommand = new UpdateKategoriMitraPenelitianCommand(uuid, namaAfter);
                var updateResult = await Sender.Send(updateCommand);

                Assert.True(updateResult.IsSuccess);
                var dataUpdate = DBContext.KategoriMitraPenelitian.FirstOrDefault(p => p.Uuid.ToString() == uuid);
                Assert.NotNull(dataUpdate);
                Assert.Equal(namaAfter, dataUpdate.Nama);

                using (var scope = Factory.Services.CreateScope())
                {
                    var handler = scope.ServiceProvider
                        .GetService<IRequestHandler<UpdateKategoriMitraPenelitianCommand, Result>>();
                    Assert.NotNull(handler);
                    Assert.IsType<UpdateKategoriMitraPenelitianCommandHandler>(handler);
                }
            }

            // --- DELETE ---
            if (mode == "deleted")
            {
                var deleteCommand = new DeleteKategoriMitraPenelitianCommand(uuid);
                var deleteResult = await Sender.Send(deleteCommand);

                Assert.True(deleteResult.IsSuccess);
                var dataDeleted = DBContext.KategoriMitraPenelitian.FirstOrDefault(p => p.Uuid.ToString() == uuid);
                Assert.Null(dataDeleted);

                using (var scope = Factory.Services.CreateScope())
                {
                    var handler = scope.ServiceProvider
                        .GetService<IRequestHandler<DeleteKategoriMitraPenelitianCommand, Result>>();
                    Assert.NotNull(handler);
                    Assert.IsType<DeleteKategoriMitraPenelitianCommandHandler>(handler);
                }
            }
        }

        [Fact]
        public async Task Update_ShouldThrow_WhenNotExist()
        {
            var guid = Guid.NewGuid().ToString();
            var namaBefore = "tes";

            var updateCommand = new UpdateKategoriMitraPenelitianCommand(guid, namaBefore);
            var updateResult = await Sender.Send(updateCommand);

            Assert.True(updateResult.IsFailure);
            Assert.Equal("KategoriMitraPenelitian.NotFound", updateResult.Error.Code);
        }

        [Fact]
        public async Task Delete_ShouldThrow_WhenNotExist()
        {
            var guid = Guid.NewGuid().ToString();

            var deleteCommand = new DeleteKategoriMitraPenelitianCommand(guid);
            var deleteResult = await Sender.Send(deleteCommand);

            Assert.True(deleteResult.IsFailure);
            Assert.Equal("KategoriMitraPenelitian.NotFound", deleteResult.Error.Code);
        }

        [Fact]
        public async Task Handle_All_ReturnsSuccess_WhenDataExists()
        {
            // Arrange
            var mockConnectionFactory = new Mock<IDbConnectionFactory>();
            var mockConnection = new Mock<DbConnection>();

            var fakeDataList = new List<KategoriMitraPenelitianResponse>
            {
                new() { Uuid = Guid.NewGuid().ToString(), Nama = "Mitra 1" },
                new() { Uuid = Guid.NewGuid().ToString(), Nama = "Mitra 2" }
            };

            mockConnection.SetupDapperAsync(c =>
                c.QueryAsync<KategoriMitraPenelitianResponse>(
                    It.IsAny<string>(),
                    It.IsAny<object>(),
                    It.IsAny<IDbTransaction>(),
                    It.IsAny<int?>(),
                    It.IsAny<System.Data.CommandType?>()
                )
            ).ReturnsAsync(fakeDataList);

            mockConnectionFactory.Setup(f => f.OpenConnectionAsync())
                .ReturnsAsync(mockConnection.Object);

            var handler = new GetAllKategoriMitraPenelitianQueryHandler(mockConnectionFactory.Object);
            var query = new GetAllKategoriMitraPenelitianQuery();

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(fakeDataList.Count, result.Value.Count);
        }

        [Fact]
        public async Task Handle_All_ReturnsFailure_WhenNoData()
        {
            // Arrange
            var mockConnectionFactory = new Mock<IDbConnectionFactory>();
            var mockConnection = new Mock<DbConnection>();

            mockConnection.SetupDapperAsync(c =>
                c.QueryAsync<KategoriMitraPenelitianResponse>(
                    It.IsAny<string>(),
                    It.IsAny<object>(),
                    It.IsAny<IDbTransaction>(),
                    It.IsAny<int?>(),
                    It.IsAny<System.Data.CommandType?>()
                )
            ).ReturnsAsync(new List<KategoriMitraPenelitianResponse>());

            mockConnectionFactory.Setup(f => f.OpenConnectionAsync())
                .ReturnsAsync(mockConnection.Object);

            var handler = new GetAllKategoriMitraPenelitianQueryHandler(mockConnectionFactory.Object);
            var query = new GetAllKategoriMitraPenelitianQuery();

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
        }

        [Fact]
        public async Task Handle_Single_ReturnsSuccess_WhenDataExists()
        {
            // Arrange
            var mockConnectionFactory = new Mock<IDbConnectionFactory>();
            var mockConnection = new Mock<DbConnection>();
            var uuid = Guid.NewGuid();

            var fakeData = new KategoriMitraPenelitianResponse
            {
                Uuid = uuid.ToString(),
                Nama = "Mitra 1"
            };

            mockConnection.SetupDapperAsync(c =>
                c.QuerySingleOrDefaultAsync<KategoriMitraPenelitianResponse>(
                    It.IsAny<string>(),
                    It.IsAny<object>(),
                    It.IsAny<IDbTransaction>(),
                    It.IsAny<int?>(),
                    It.IsAny<System.Data.CommandType?>()
                )
            ).ReturnsAsync(fakeData);

            mockConnectionFactory.Setup(f => f.OpenConnectionAsync())
                .ReturnsAsync(mockConnection.Object);

            var handler = new GetKategoriMitraPenelitianQueryHandler(mockConnectionFactory.Object);
            var query = new GetKategoriMitraPenelitianQuery(uuid);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(fakeData.Nama, result.Value.Nama);
        }

        [Fact]
        public async Task Handle_Single_ReturnsFailure_WhenDataNotFound()
        {
            // Arrange
            var mockConnectionFactory = new Mock<IDbConnectionFactory>();
            var mockConnection = new Mock<DbConnection>();

            mockConnection.SetupDapperAsync(c =>
                c.QuerySingleOrDefaultAsync<KategoriMitraPenelitianResponse>(
                    It.IsAny<string>(),
                    It.IsAny<object>(),
                    It.IsAny<IDbTransaction>(),
                    It.IsAny<int?>(),
                    It.IsAny<System.Data.CommandType?>()
                )
            ).ReturnsAsync((KategoriMitraPenelitianResponse?)null);

            mockConnectionFactory.Setup(f => f.OpenConnectionAsync())
                .ReturnsAsync(mockConnection.Object);

            var handler = new GetKategoriMitraPenelitianQueryHandler(mockConnectionFactory.Object);
            var query = new GetKategoriMitraPenelitianQuery(Guid.NewGuid());

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
        }
    }
}
