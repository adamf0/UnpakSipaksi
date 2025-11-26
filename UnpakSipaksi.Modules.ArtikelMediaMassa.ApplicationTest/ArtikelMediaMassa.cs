using Dapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Moq.Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.ArtikelMediaMassa.Application.CreateArtikelMediaMassa;
using UnpakSipaksi.Modules.ArtikelMediaMassa.Application.DeleteArtikelMediaMassa;
using UnpakSipaksi.Modules.ArtikelMediaMassa.Application.GetAllArtikelMediaMassa;
using UnpakSipaksi.Modules.ArtikelMediaMassa.Application.GetArtikelMediaMassa;
using UnpakSipaksi.Modules.ArtikelMediaMassa.Application.UpdateArtikelMediaMassa;
using Xunit;

namespace UnpakSipaksi.Modules.ArtikelMediaMassa.ApplicationTest
{
    public class ArtikelMediaMassaTest : BaseIntegrationTest
    {
        public ArtikelMediaMassaTest(IntegrationTestWebAppFactory factory) : base(factory)
        {

        }

        public static IEnumerable<object[]> InvalidData()
        {
            var valid = Guid.NewGuid().ToString();
            var empty = "";

            yield return new object[] { empty, "", 10, "'Nama' tidak boleh kosong.", "created" };
            yield return new object[] { empty, "tes", -10, "'Nilai' tidak boleh negative.", "created" };

            yield return new object[] { valid, "", 10, "'Nama' tidak boleh kosong.", "updated" };
            yield return new object[] { valid, "tes", -10, "'Nilai' tidak boleh negative.", "updated" };
            yield return new object[] { empty, "tes", 10, "'Uuid' tidak boleh kosong.", "updated" };
            yield return new object[] { "no-guid", "tes", 10, "'Uuid' harus dalam format UUID v4 yang valid.", "updated" };

            yield return new object[] { empty, "tes", 10, "'Uuid' tidak boleh kosong.", "deleted" };
            yield return new object[] { "no-guid", "tes", 10, "'Uuid' harus dalam format UUID v4 yang valid.", "deleted" };
        }

        public static IEnumerable<object?[]> ValidData()
        {
            yield return new object?[] { new object[] { "tes", 1000 }, null, "created" };
            yield return new object?[] { new object[] { "tes", 1000 }, new object[] { "tes2", 2000 }, "updated" };
            yield return new object?[] { new object[] { "tes", 1000 }, null, "deleted" };
        }

        [Theory]
        [MemberData(nameof(InvalidData))]
        public async Task CreateUpdateDelete_ShouldThrow_WhenInvalidFluentValidation(
            string uuid,
            string nama,
            int nilai,
            string message,
            string mode)
        {
            // Act
            Result? result = null;
            if (mode == "created")
            {
                var command = new CreateArtikelMediaMassaCommand(nama, nilai);
                result = await Sender.Send(command);
            }
            else if (mode == "updated")
            {
                var command = new UpdateArtikelMediaMassaCommand(uuid, nama, nilai);
                result = await Sender.Send(command);
            }
            else
            {
                var command = new DeleteArtikelMediaMassaCommand(uuid);
                result = await Sender.Send(command);
            }

            // Assert
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
            var nilaiBefore = (int)beforeData[1];

            var createCommand = new CreateArtikelMediaMassaCommand(namaBefore, nilaiBefore);
            var createResult = await Sender.Send(createCommand);

            Assert.True(createResult.IsSuccess);
            var dataCreate = DBContext.ArtikelMediaMassa.FirstOrDefault(p => p.Uuid == createResult!.Value);

            Assert.NotNull(dataCreate);
            Assert.Equal(namaBefore, dataCreate.Nama);
            Assert.Equal(nilaiBefore, dataCreate.Nilai);

            using (var scope = Factory.Services.CreateScope())
            {
                var handler = scope.ServiceProvider
                    .GetService<IRequestHandler<CreateArtikelMediaMassaCommand, Result<Guid>>>();

                Assert.NotNull(handler);
                Assert.IsType<CreateArtikelMediaMassaCommandHandler>(handler);
            }

            var newUuid = createResult.Value.ToString();

            // --- UPDATE / DELETE ---
            if (mode == "updated")
            {
                var namaAfter = (string)afterData![0];
                var nilaiAfter = (int)afterData[1];

                var updateCommand = new UpdateArtikelMediaMassaCommand(newUuid, namaAfter, nilaiAfter);
                var updateResult = await Sender.Send(updateCommand);

                Assert.True(updateResult.IsSuccess);
                var dataUpdate = DBContext.ArtikelMediaMassa.FirstOrDefault(p => p.Uuid == createResult!.Value);

                Assert.NotNull(dataUpdate);
                Assert.Equal(namaAfter, dataUpdate.Nama);
                Assert.Equal(nilaiAfter, dataUpdate.Nilai);

                using (var scope = Factory.Services.CreateScope())
                {
                    var handler = scope.ServiceProvider
                        .GetService<IRequestHandler<UpdateArtikelMediaMassaCommand, Result>>();

                    Assert.NotNull(handler);
                    Assert.IsType<UpdateArtikelMediaMassaCommandHandler>(handler);
                }
            }
            else if (mode == "deleted")
            {
                var deleteCommand = new DeleteArtikelMediaMassaCommand(newUuid);
                var deleteResult = await Sender.Send(deleteCommand);

                Assert.True(deleteResult.IsSuccess);
            }
        }

        [Fact]
        public async Task Create_ShouldThrow_WhenInvalidRuleDomain()
        {
            var namaBefore = "tes";
            var nilaiBefore = int.MaxValue;

            var createCommand = new CreateArtikelMediaMassaCommand(namaBefore, nilaiBefore);
            var createResult = await Sender.Send(createCommand);

            Assert.True(createResult.IsFailure);
            Assert.Equal("ArtikelMediaMassa.InvalidNilai", createResult.Error.Code);
        }

        [Fact]
        public async Task Update_ShouldThrow_WhenNotExist()
        {
            var guid = Guid.NewGuid().ToString();
            var namaBefore = "tes";
            var nilaiBefore = 1;

            var updateCommand = new UpdateArtikelMediaMassaCommand(guid, namaBefore, nilaiBefore);
            var updateResult = await Sender.Send(updateCommand);

            Assert.True(updateResult.IsFailure);
            Assert.Equal("ArtikelMediaMassa.NotFound", updateResult.Error.Code);
        }

        [Fact]
        public async Task Update_ShouldThrow_WhenInvalidRuleDomain()
        {
            // --- CREATE ---
            var namaBefore = "tes";
            var nilaiBefore = 1;

            var createCommand = new CreateArtikelMediaMassaCommand(namaBefore, nilaiBefore);
            var createResult = await Sender.Send(createCommand);

            Assert.True(createResult.IsSuccess);
            var dataCreate = DBContext.ArtikelMediaMassa.FirstOrDefault(p => p.Uuid == createResult!.Value);

            Assert.NotNull(dataCreate);
            Assert.Equal(namaBefore, dataCreate.Nama);
            Assert.Equal(nilaiBefore, dataCreate.Nilai);

            var newUuid = createResult.Value.ToString();

            // --- UPDATE ---
            //if (mode == "updated")
            //{
            var namaAfter = "tes2";
            var nilaiAfter = int.MaxValue;

            var updateCommand = new UpdateArtikelMediaMassaCommand(newUuid, namaAfter, nilaiAfter);
            var updateResult = await Sender.Send(updateCommand);

            Assert.True(updateResult.IsFailure);
            Assert.Equal("ArtikelMediaMassa.InvalidNilai", updateResult.Error.Code);
            //}
        }

        [Fact]
        public async Task Delete_ShouldThrow_WhenNotExist()
        {
            var guid = Guid.NewGuid().ToString();

            var deleteCommand = new DeleteArtikelMediaMassaCommand(guid);
            var deleteResult = await Sender.Send(deleteCommand);

            Assert.True(deleteResult.IsFailure);
            Assert.Equal("ArtikelMediaMassa.NotFound", deleteResult.Error.Code);
        }

        [Fact]
        public async Task Handle_ReturnsList_WhenDataExists()
        {
            // Arrange
            var mockConnectionFactory = new Mock<IDbConnectionFactory>();
            var mockConnection = new Mock<DbConnection>();

            var fakeData = new List<ArtikelMediaMassaResponse>
            {
                new ArtikelMediaMassaResponse
                {
                    Uuid = "123",
                    Nama = "Penelitian 1",
                    Nilai = 20
                }
            };

            // Setup Dapper extension
            mockConnection.SetupDapperAsync(c => c.QueryAsync<ArtikelMediaMassaResponse>(
            It.IsAny<string>(), null, null, null, null))
            .ReturnsAsync(fakeData);

            mockConnectionFactory
             .Setup(f => f.OpenConnectionAsync())
             .Returns(new ValueTask<DbConnection>(mockConnection.Object));

            var handler = new GetAllArtikelMediaMassaQueryHandler(mockConnectionFactory.Object);
            var query = new GetAllArtikelMediaMassaQuery();

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotEmpty(result.Value);
            Assert.Equal(fakeData.First().Nama, result.Value.First().Nama);
        }

        [Fact]
        public async Task Handle_ReturnsFailure_WhenNoData()
        {
            // Arrange
            var mockConnectionFactory = new Mock<IDbConnectionFactory>();
            var mockConnection = new Mock<DbConnection>();

            // Empty data
            mockConnection.SetupDapperAsync(c => c.QueryAsync<ArtikelMediaMassaResponse>(
                It.IsAny<string>(), null, null, null, null))
                .ReturnsAsync(new List<ArtikelMediaMassaResponse>());

            mockConnectionFactory.Setup(f => f.OpenConnectionAsync())
                .ReturnsAsync(mockConnection.Object);

            var handler = new GetAllArtikelMediaMassaQueryHandler(mockConnectionFactory.Object);
            var query = new GetAllArtikelMediaMassaQuery();

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

            var fakeData = new ArtikelMediaMassaResponse
            {
                Uuid = uuid.ToString(),
                Nama = "Penelitian 1",
                Nilai = 20
            };

            mockConnection.SetupDapperAsync(c =>
                c.QuerySingleOrDefaultAsync<ArtikelMediaMassaResponse?>(
                    It.IsAny<string>(),
                    It.IsAny<object>(),
                    null, null, null
                )
            ).ReturnsAsync(fakeData);

            mockConnectionFactory
                .Setup(f => f.OpenConnectionAsync())
                .Returns(new ValueTask<DbConnection>(mockConnection.Object));

            var handler = new GetArtikelMediaMassaQueryHandler(mockConnectionFactory.Object);
            var query = new GetArtikelMediaMassaQuery(uuid);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal("Penelitian 1", result.Value.Nama);
        }
        [Fact]
        public async Task Handle_ReturnsFailure_WhenDataNotFound()
        {
            // Arrange
            var mockConnectionFactory = new Mock<IDbConnectionFactory>();
            var mockConnection = new Mock<DbConnection>();

            // Return null
            mockConnection.SetupDapperAsync(c =>
                c.QuerySingleOrDefaultAsync<ArtikelMediaMassaResponse?>(
                    It.IsAny<string>(),
                    It.IsAny<object>(),
                    null, null, null
                )
            ).ReturnsAsync((ArtikelMediaMassaResponse?)null);

            mockConnectionFactory
                .Setup(f => f.OpenConnectionAsync())
                .Returns(new ValueTask<DbConnection>(mockConnection.Object));

            var handler = new GetArtikelMediaMassaQueryHandler(mockConnectionFactory.Object);
            var query = new GetArtikelMediaMassaQuery(Guid.NewGuid());

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
        }
        [Fact]
        public async Task Handle_ReturnsSuccess_Default_WhenDataExists()
        {
            // Arrange
            var mockConnectionFactory = new Mock<IDbConnectionFactory>();
            var mockConnection = new Mock<DbConnection>();
            var uuid = Guid.NewGuid();

            var fakeData = new ArtikelMediaMassaDefaultResponse
            {
                Id = "1",
                Uuid = uuid.ToString(),
                Nama = "Penelitian Default",
                Nilai = 20
            };

            mockConnection.SetupDapperAsync(c =>
                c.QuerySingleOrDefaultAsync<ArtikelMediaMassaDefaultResponse?>(
                    It.IsAny<string>(),
                    It.IsAny<object>(),
                    null, null, null
                )
            ).ReturnsAsync(fakeData);

            mockConnectionFactory
                .Setup(f => f.OpenConnectionAsync())
                .Returns(new ValueTask<DbConnection>(mockConnection.Object));

            var handler = new GetArtikelMediaMassaDefaultQueryHandler(mockConnectionFactory.Object);
            var query = new GetArtikelMediaMassaDefaultQuery(uuid);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal("Penelitian Default", result.Value.Nama);
        }
        [Fact]
        public async Task Handle_ReturnsFailure_Default_WhenDataNotFound()
        {
            // Arrange
            var mockConnectionFactory = new Mock<IDbConnectionFactory>();
            var mockConnection = new Mock<DbConnection>();

            mockConnection.SetupDapperAsync(c =>
                c.QuerySingleOrDefaultAsync<ArtikelMediaMassaDefaultResponse?>(
                    It.IsAny<string>(),
                    It.IsAny<object>(),
                    null, null, null
                )
            ).ReturnsAsync((ArtikelMediaMassaDefaultResponse?)null);

            mockConnectionFactory
                .Setup(f => f.OpenConnectionAsync())
                .Returns(new ValueTask<DbConnection>(mockConnection.Object));

            var handler = new GetArtikelMediaMassaDefaultQueryHandler(mockConnectionFactory.Object);
            var query = new GetArtikelMediaMassaDefaultQuery(Guid.NewGuid());

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
        }
    }
}
