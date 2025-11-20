using Dapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Moq.Dapper;
using System.Data;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KategoriProgramPengabdian.Application.CreateKategoriProgramPengabdian;
using UnpakSipaksi.Modules.KategoriProgramPengabdian.Application.DeleteKategoriProgramPengabdian;
using UnpakSipaksi.Modules.KategoriProgramPengabdian.Application.GetAllKategoriProgramPengabdian;
using UnpakSipaksi.Modules.KategoriProgramPengabdian.Application.GetKategoriProgramPengabdian;
using UnpakSipaksi.Modules.KategoriProgramPengabdian.Application.UpdateKategoriProgramPengabdian;
using Xunit;

namespace UnpakSipaksi.Modules.KategoriProgramPengabdian.ApplicationTest
{
    public class KategoriProgramPengabdianTest : BaseIntegrationTest
    {
        public KategoriProgramPengabdianTest(IntegrationTestWebAppFactory factory) : base(factory)
        {
        }

        public static IEnumerable<object[]> InvalidData()
        {
            var validUuid = Guid.NewGuid().ToString();
            var empty = "";

            // CREATE
            yield return new object[] { validUuid, "", "[]", "'Nama' tidak boleh kosong.", "created" };
            yield return new object[] { validUuid, "nama", "", "'Rule' tidak boleh kosong.", "created" };

            // UPDATE
            yield return new object[] { validUuid, "", "[]", "'Nama' tidak boleh kosong.", "updated" };
            yield return new object[] { validUuid, "nama", "", "'Rule' tidak boleh kosong.", "updated" };
            yield return new object[] { empty, "tes", "[]", "'Uuid' tidak boleh kosong.", "updated" };
            yield return new object[] { "no-guid", "tes", "[]", "'Uuid' harus dalam format UUID v4 yang valid.", "updated" };

            // DELETE
            yield return new object[] { empty, "tes", "[]", "'Uuid' tidak boleh kosong.", "deleted" };
            yield return new object[] { "no-guid", "tes", "[]", "'Uuid' harus dalam format UUID v4 yang valid.", "deleted" };
        }

        public static IEnumerable<object?[]> ValidData()
        {
            var validUuid = Guid.NewGuid().ToString();
            yield return new object?[] { new object[] { "tes", "[]" }, null, "created" };
            yield return new object?[] { new object[] { "tes", "[]" }, new object[] { "tes updated", "[]" }, "updated" };
            yield return new object?[] { new object[] { "tes", "[]" }, null, "deleted" };
        }

        [Theory]
        [MemberData(nameof(InvalidData))]
        public async Task CreateUpdateDelete_ShouldThrow_WhenInvalidFluentValidation(
            string uuid,
            string nama,
            string rule,
            string message,
            string mode)
        {
            Result? result = null;

            if (mode == "created")
            {
                var command = new CreateKategoriProgramPengabdianCommand(nama, rule);
                result = await Sender.Send(command);
            }
            else if (mode == "updated")
            {
                var command = new UpdateKategoriProgramPengabdianCommand(uuid, nama, rule);
                result = await Sender.Send(command);
            }
            else
            {
                var command = new DeleteKategoriProgramPengabdianCommand(uuid);
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
            var ruleBefore = (string)beforeData[1];

            // --- CREATE ---
            var createCommand = new CreateKategoriProgramPengabdianCommand(namaBefore, ruleBefore);
            var createResult = await Sender.Send(createCommand);

            Assert.True(createResult.IsSuccess);
            var dataCreate = DBContext.KategoriProgramPengabdian.FirstOrDefault(p => p.Uuid == createResult!.Value);
            Assert.NotNull(dataCreate);
            Assert.Equal(namaBefore, dataCreate.Nama);
            Assert.Equal(ruleBefore, dataCreate.Rule);

            using (var scope = Factory.Services.CreateScope())
            {
                var handler = scope.ServiceProvider
                    .GetService<IRequestHandler<CreateKategoriProgramPengabdianCommand, Result<Guid>>>();
                Assert.NotNull(handler);
                Assert.IsType<CreateKategoriProgramPengabdianCommandHandler>(handler);
            }

            var uuid = createResult.Value.ToString();

            // --- UPDATE ---
            if (mode == "updated")
            {
                var namaAfter = (string)afterData![0];
                var ruleAfter = (string)afterData[1];
                var updateCommand = new UpdateKategoriProgramPengabdianCommand(uuid, namaAfter, ruleAfter);
                var updateResult = await Sender.Send(updateCommand);

                Assert.True(updateResult.IsSuccess);
                var dataUpdate = DBContext.KategoriProgramPengabdian.FirstOrDefault(p => p.Uuid.ToString() == uuid);
                Assert.NotNull(dataUpdate);
                Assert.Equal(namaAfter, dataUpdate.Nama);
                Assert.Equal(ruleAfter, dataUpdate.Rule);

                using (var scope = Factory.Services.CreateScope())
                {
                    var handler = scope.ServiceProvider
                        .GetService<IRequestHandler<UpdateKategoriProgramPengabdianCommand, Result>>();
                    Assert.NotNull(handler);
                    Assert.IsType<UpdateKategoriProgramPengabdianCommandHandler>(handler);
                }
            }

            // --- DELETE ---
            if (mode == "deleted")
            {
                var deleteCommand = new DeleteKategoriProgramPengabdianCommand(uuid);
                var deleteResult = await Sender.Send(deleteCommand);

                Assert.True(deleteResult.IsSuccess);
                var dataDeleted = DBContext.KategoriProgramPengabdian.FirstOrDefault(p => p.Uuid.ToString() == uuid);
                Assert.Null(dataDeleted);

                using (var scope = Factory.Services.CreateScope())
                {
                    var handler = scope.ServiceProvider
                        .GetService<IRequestHandler<DeleteKategoriProgramPengabdianCommand, Result>>();
                    Assert.NotNull(handler);
                    Assert.IsType<DeleteKategoriProgramPengabdianCommandHandler>(handler);
                }
            }
        }
        [Fact]
        public async Task Create_ShouldThrow_WhenInvalidRuleDomain()
        {
            var nama = "tes";
            var rule = "**"; // contoh melanggar aturan domain

            var command = new CreateKategoriProgramPengabdianCommand(nama, rule);
            var result = await Sender.Send(command);

            Assert.True(result.IsFailure);
            Assert.Equal("KategoriProgramPengabdian.InvalidFormatRule", result.Error.Code);
        }

        [Fact]
        public async Task Update_ShouldThrow_WhenNotExist()
        {
            var guid = Guid.NewGuid().ToString();
            var nama = "tes";
            var rule = "[]";

            var command = new UpdateKategoriProgramPengabdianCommand(guid, nama, rule);
            var result = await Sender.Send(command);

            Assert.True(result.IsFailure);
            Assert.Equal("KategoriProgramPengabdian.NotFound", result.Error.Code);
        }

        [Fact]
        public async Task Update_ShouldThrow_WhenInvalidRuleDomain()
        {
            // --- CREATE ---
            var namaBefore = "tes";
            var ruleBefore = "[]";

            var createCommand = new CreateKategoriProgramPengabdianCommand(namaBefore, ruleBefore);
            var createResult = await Sender.Send(createCommand);

            Assert.True(createResult.IsSuccess);
            var dataCreate = DBContext.KategoriProgramPengabdian.FirstOrDefault(p => p.Uuid == createResult!.Value);

            Assert.NotNull(dataCreate);
            Assert.Equal(namaBefore, dataCreate.Nama);
            Assert.Equal(ruleBefore, dataCreate.Rule);

            var newUuid = createResult.Value.ToString();

            // --- UPDATE (melanggar aturan domain)
            var namaAfter = "tes2";
            var ruleAfter = "**";

            var updateCommand = new UpdateKategoriProgramPengabdianCommand(newUuid, namaAfter, ruleAfter);
            var updateResult = await Sender.Send(updateCommand);

            Assert.True(updateResult.IsFailure);
            Assert.Equal("KategoriProgramPengabdian.InvalidFormatRule", updateResult.Error.Code);
        }

        [Fact]
        public async Task Delete_ShouldThrow_WhenNotExist()
        {
            var guid = Guid.NewGuid().ToString();

            var command = new DeleteKategoriProgramPengabdianCommand(guid);
            var result = await Sender.Send(command);

            Assert.True(result.IsFailure);
            Assert.Equal("KategoriProgramPengabdian.NotFound", result.Error.Code);
        }

        [Fact]
        public async Task Handle_All_ReturnsSuccess_WhenDataExists()
        {
            // Arrange
            var mockConnectionFactory = new Mock<IDbConnectionFactory>();
            var mockConnection = new Mock<DbConnection>();

            var fakeDataList = new List<KategoriProgramPengabdianResponse>
        {
            new() { Uuid = Guid.NewGuid().ToString(), Nama = "Program 1", Rule = "[]" },
            new() { Uuid = Guid.NewGuid().ToString(), Nama = "Program 2", Rule = "[]" }
        };

            mockConnection.SetupDapperAsync(c =>
                c.QueryAsync<KategoriProgramPengabdianResponse>(
                    It.IsAny<string>(),
                    It.IsAny<object>(),
                    It.IsAny<IDbTransaction>(),
                    It.IsAny<int?>(),
                    It.IsAny<System.Data.CommandType?>()
                )
            ).ReturnsAsync(fakeDataList);

            mockConnectionFactory.Setup(f => f.OpenConnectionAsync())
                .ReturnsAsync(mockConnection.Object);

            var handler = new GetAllKategoriProgramPengabdianQueryHandler(mockConnectionFactory.Object);
            var query = new GetAllKategoriProgramPengabdianQuery();

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
                c.QueryAsync<KategoriProgramPengabdianResponse>(
                    It.IsAny<string>(),
                    It.IsAny<object>(),
                    It.IsAny<IDbTransaction>(),
                    It.IsAny<int?>(),
                    It.IsAny<System.Data.CommandType?>()
                )
            ).ReturnsAsync(new List<KategoriProgramPengabdianResponse>());

            mockConnectionFactory.Setup(f => f.OpenConnectionAsync())
                .ReturnsAsync(mockConnection.Object);

            var handler = new GetAllKategoriProgramPengabdianQueryHandler(mockConnectionFactory.Object);
            var query = new GetAllKategoriProgramPengabdianQuery();

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

            var fakeData = new KategoriProgramPengabdianResponse
            {
                Uuid = uuid.ToString(),
                Nama = "Program 1",
                Rule = "[]"
            };

            mockConnection.SetupDapperAsync(c =>
                c.QuerySingleOrDefaultAsync<KategoriProgramPengabdianResponse>(
                    It.IsAny<string>(),
                    It.IsAny<object>(),
                    It.IsAny<IDbTransaction>(),
                    It.IsAny<int?>(),
                    It.IsAny<System.Data.CommandType?>()
                )
            ).ReturnsAsync(fakeData);

            mockConnectionFactory.Setup(f => f.OpenConnectionAsync())
                .ReturnsAsync(mockConnection.Object);

            var handler = new GetKategoriProgramPengabdianQueryHandler(mockConnectionFactory.Object);
            var query = new GetKategoriProgramPengabdianQuery(uuid);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(fakeData.Nama, result.Value.Nama);
            Assert.Equal(fakeData.Rule, result.Value.Rule);
        }

        [Fact]
        public async Task Handle_Single_ReturnsFailure_WhenDataNotFound()
        {
            // Arrange
            var mockConnectionFactory = new Mock<IDbConnectionFactory>();
            var mockConnection = new Mock<DbConnection>();

            mockConnection.SetupDapperAsync(c =>
                c.QuerySingleOrDefaultAsync<KategoriProgramPengabdianResponse>(
                    It.IsAny<string>(),
                    It.IsAny<object>(),
                    It.IsAny<IDbTransaction>(),
                    It.IsAny<int?>(),
                    It.IsAny<System.Data.CommandType?>()
                )
            ).ReturnsAsync((KategoriProgramPengabdianResponse?)null);

            mockConnectionFactory.Setup(f => f.OpenConnectionAsync())
                .ReturnsAsync(mockConnection.Object);

            var handler = new GetKategoriProgramPengabdianQueryHandler(mockConnectionFactory.Object);
            var query = new GetKategoriProgramPengabdianQuery(Guid.NewGuid());

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

            var fakeData = new KategoriProgramPengabdianDefaultResponse
            {
                Uuid = uuid.ToString(),
                Nama = "Program 1",
            };

            mockConnection.SetupDapperAsync(c =>
                c.QuerySingleOrDefaultAsync<KategoriProgramPengabdianDefaultResponse>(
                    It.IsAny<string>(),
                    It.IsAny<object>(),
                    It.IsAny<IDbTransaction>(),
                    It.IsAny<int?>(),
                    It.IsAny<System.Data.CommandType?>()
                )
            ).ReturnsAsync(fakeData);

            mockConnectionFactory.Setup(f => f.OpenConnectionAsync())
                .ReturnsAsync(mockConnection.Object);

            var handler = new GetKategoriProgramPengabdianDefaultQueryHandler(mockConnectionFactory.Object);
            var query = new GetKategoriProgramPengabdianDefaultQuery(uuid);

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
                c.QuerySingleOrDefaultAsync<KategoriProgramPengabdianDefaultResponse>(
                    It.IsAny<string>(),
                    It.IsAny<object>(),
                    It.IsAny<IDbTransaction>(),
                    It.IsAny<int?>(),
                    It.IsAny<System.Data.CommandType?>()
                )
            ).ReturnsAsync((KategoriProgramPengabdianDefaultResponse?)null);

            mockConnectionFactory.Setup(f => f.OpenConnectionAsync())
                .ReturnsAsync(mockConnection.Object);

            var handler = new GetKategoriProgramPengabdianDefaultQueryHandler(mockConnectionFactory.Object);
            var query = new GetKategoriProgramPengabdianDefaultQuery(Guid.NewGuid());

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
        }
    }
}
