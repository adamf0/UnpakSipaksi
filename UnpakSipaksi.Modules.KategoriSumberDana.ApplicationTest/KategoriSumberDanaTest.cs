using Dapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Moq.Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KategoriSumberDana.Application.CreateKategoriSumberDana;
using UnpakSipaksi.Modules.KategoriSumberDana.Application.DeleteKategoriSumberDana;
using UnpakSipaksi.Modules.KategoriSumberDana.Application.GetAllKategoriSumberDana;
using UnpakSipaksi.Modules.KategoriSumberDana.Application.GetKategoriSumberDana;
using UnpakSipaksi.Modules.KategoriSumberDana.Application.UpdateKategoriSumberDana;
using Xunit;

namespace UnpakSipaksi.Modules.KategoriSumberDana.ApplicationTest
{
    public class KategoriSumberDanaTest : BaseIntegrationTest
    {
        public KategoriSumberDanaTest(IntegrationTestWebAppFactory factory) : base(factory)
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
                var command = new CreateKategoriSumberDanaCommand(nama);
                result = await Sender.Send(command);
            }
            else if (mode == "updated")
            {
                var command = new UpdateKategoriSumberDanaCommand(uuid, nama);
                result = await Sender.Send(command);
            }
            else
            {
                var command = new DeleteKategoriSumberDanaCommand(uuid);
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
            var createCommand = new CreateKategoriSumberDanaCommand(namaBefore);
            var createResult = await Sender.Send(createCommand);

            Assert.True(createResult.IsSuccess);
            var dataCreate = DBContext.KategoriSumberDana.FirstOrDefault(p => p.Uuid == createResult!.Value);
            Assert.NotNull(dataCreate);
            Assert.Equal(namaBefore, dataCreate.Nama);

            using (var scope = Factory.Services.CreateScope())
            {
                var handler = scope.ServiceProvider
                    .GetService<IRequestHandler<CreateKategoriSumberDanaCommand, Result<Guid>>>();
                Assert.NotNull(handler);
                Assert.IsType<CreateKategoriSumberDanaCommandHandler>(handler);
            }

            var uuid = createResult.Value.ToString();

            // --- UPDATE ---
            if (mode == "updated")
            {
                var namaAfter = (string)afterData![0];
                var updateCommand = new UpdateKategoriSumberDanaCommand(uuid, namaAfter);
                var updateResult = await Sender.Send(updateCommand);

                Assert.True(updateResult.IsSuccess);
                var dataUpdate = DBContext.KategoriSumberDana.FirstOrDefault(p => p.Uuid.ToString() == uuid);
                Assert.NotNull(dataUpdate);
                Assert.Equal(namaAfter, dataUpdate.Nama);

                using (var scope = Factory.Services.CreateScope())
                {
                    var handler = scope.ServiceProvider
                        .GetService<IRequestHandler<UpdateKategoriSumberDanaCommand, Result>>();
                    Assert.NotNull(handler);
                    Assert.IsType<UpdateKategoriSumberDanaCommandHandler>(handler);
                }
            }

            // --- DELETE ---
            if (mode == "deleted")
            {
                var deleteCommand = new DeleteKategoriSumberDanaCommand(uuid);
                var deleteResult = await Sender.Send(deleteCommand);

                Assert.True(deleteResult.IsSuccess);
                var dataDeleted = DBContext.KategoriSumberDana.FirstOrDefault(p => p.Uuid.ToString() == uuid);
                Assert.Null(dataDeleted);

                using (var scope = Factory.Services.CreateScope())
                {
                    var handler = scope.ServiceProvider
                        .GetService<IRequestHandler<DeleteKategoriSumberDanaCommand, Result>>();
                    Assert.NotNull(handler);
                    Assert.IsType<DeleteKategoriSumberDanaCommandHandler>(handler);
                }
            }
        }

        [Fact]
        public async Task Update_ShouldThrow_WhenNotExist()
        {
            var guid = Guid.NewGuid().ToString();
            var namaBefore = "tes";

            var updateCommand = new UpdateKategoriSumberDanaCommand(guid, namaBefore);
            var updateResult = await Sender.Send(updateCommand);

            Assert.True(updateResult.IsFailure);
            Assert.Equal("KategoriSumberDana.NotFound", updateResult.Error.Code);
        }

        [Fact]
        public async Task Delete_ShouldThrow_WhenNotExist()
        {
            var guid = Guid.NewGuid().ToString();

            var deleteCommand = new DeleteKategoriSumberDanaCommand(guid);
            var deleteResult = await Sender.Send(deleteCommand);

            Assert.True(deleteResult.IsFailure);
            Assert.Equal("KategoriSumberDana.NotFound", deleteResult.Error.Code);
        }

        [Fact]
        public async Task GetAllKategoriSumberDana_Handle_ReturnsList_WhenDataExists()
        {
            // Arrange
            var mockConnectionFactory = new Mock<IDbConnectionFactory>();
            var mockConnection = new Mock<DbConnection>();

            var fakeData = new List<KategoriSumberDanaResponse>
            {
                new KategoriSumberDanaResponse
                {
                    Uuid = Guid.NewGuid().ToString(),
                    Nama = "Sumber Dana 1"
                }
            };

            mockConnection.SetupDapperAsync(c =>
                c.QueryAsync<KategoriSumberDanaResponse>(It.IsAny<string>(), null, null, null, null))
                .ReturnsAsync(fakeData);

            mockConnectionFactory.Setup(f => f.OpenConnectionAsync())
                .Returns(new ValueTask<DbConnection>(mockConnection.Object));

            var handler = new GetAllKategoriSumberDanaQueryHandler(mockConnectionFactory.Object);
            var query = new GetAllKategoriSumberDanaQuery();

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotEmpty(result.Value);
            Assert.Equal(fakeData.First().Nama, result.Value.First().Nama);
        }

        [Fact]
        public async Task GetAllKategoriSumberDana_Handle_ReturnsFailure_WhenNoData()
        {
            // Arrange
            var mockConnectionFactory = new Mock<IDbConnectionFactory>();
            var mockConnection = new Mock<DbConnection>();

            mockConnection.SetupDapperAsync(c =>
                c.QueryAsync<KategoriSumberDanaResponse>(It.IsAny<string>(), null, null, null, null))
                .ReturnsAsync(new List<KategoriSumberDanaResponse>());

            mockConnectionFactory.Setup(f => f.OpenConnectionAsync())
                .Returns(new ValueTask<DbConnection>(mockConnection.Object));

            var handler = new GetAllKategoriSumberDanaQueryHandler(mockConnectionFactory.Object);
            var query = new GetAllKategoriSumberDanaQuery();

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
        }

        [Fact]
        public async Task GetKategoriSumberDana_Handle_ReturnsSuccess_WhenDataExists()
        {
            // Arrange
            var mockConnectionFactory = new Mock<IDbConnectionFactory>();
            var mockConnection = new Mock<DbConnection>();
            var uuid = Guid.NewGuid();

            var fakeData = new KategoriSumberDanaResponse
            {
                Uuid = uuid.ToString(),
                Nama = "Sumber Dana 1"
            };

            mockConnection.SetupDapperAsync(c =>
                c.QuerySingleOrDefaultAsync<KategoriSumberDanaResponse?>(It.IsAny<string>(), It.IsAny<object>(), null, null, null))
                .ReturnsAsync(fakeData);

            mockConnectionFactory.Setup(f => f.OpenConnectionAsync())
                .Returns(new ValueTask<DbConnection>(mockConnection.Object));

            var handler = new GetKategoriSumberDanaQueryHandler(mockConnectionFactory.Object);
            var query = new GetKategoriSumberDanaQuery(uuid);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal("Sumber Dana 1", result.Value.Nama);
        }

        [Fact]
        public async Task GetKategoriSumberDana_Handle_ReturnsFailure_WhenDataNotFound()
        {
            // Arrange
            var mockConnectionFactory = new Mock<IDbConnectionFactory>();
            var mockConnection = new Mock<DbConnection>();

            mockConnection.SetupDapperAsync(c =>
                c.QuerySingleOrDefaultAsync<KategoriSumberDanaResponse?>(It.IsAny<string>(), It.IsAny<object>(), null, null, null))
                .ReturnsAsync((KategoriSumberDanaResponse?)null);

            mockConnectionFactory.Setup(f => f.OpenConnectionAsync())
                .Returns(new ValueTask<DbConnection>(mockConnection.Object));

            var handler = new GetKategoriSumberDanaQueryHandler(mockConnectionFactory.Object);
            var query = new GetKategoriSumberDanaQuery(Guid.NewGuid());

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
        }
    }
}
