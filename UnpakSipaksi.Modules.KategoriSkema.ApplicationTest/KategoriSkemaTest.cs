using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Moq.Dapper;
using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KategoriSkema.Application.CreateKategoriSkema;
using UnpakSipaksi.Modules.KategoriSkema.Application.DeleteKategoriSkema;
using UnpakSipaksi.Modules.KategoriSkema.Application.GetAllKategoriSkema;
using UnpakSipaksi.Modules.KategoriSkema.Application.GetKategoriSkema;
using UnpakSipaksi.Modules.KategoriSkema.Application.UpdateKategoriSkema;
using Xunit;

namespace UnpakSipaksi.Modules.KategoriSkema.ApplicationTest
{
    public class KategoriSkemaTest : BaseIntegrationTest
    {
        public KategoriSkemaTest(IntegrationTestWebAppFactory factory) : base(factory)
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
                var command = new CreateKategoriSkemaCommand(nama, rule);
                result = await Sender.Send(command);
            }
            else if (mode == "updated")
            {
                var command = new UpdateKategoriSkemaCommand(uuid, nama, rule);
                result = await Sender.Send(command);
            }
            else
            {
                var command = new DeleteKategoriSkemaCommand(uuid);
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
            var createCommand = new CreateKategoriSkemaCommand(namaBefore, ruleBefore);
            var createResult = await Sender.Send(createCommand);

            Assert.True(createResult.IsSuccess);
            var dataCreate = DBContext.KategoriSkema.FirstOrDefault(p => p.Uuid == createResult!.Value);
            Assert.NotNull(dataCreate);
            Assert.Equal(namaBefore, dataCreate.Nama);
            Assert.Equal(ruleBefore, dataCreate.Rule);

            using (var scope = Factory.Services.CreateScope())
            {
                var handler = scope.ServiceProvider
                    .GetService<IRequestHandler<CreateKategoriSkemaCommand, Result<Guid>>>();
                Assert.NotNull(handler);
                Assert.IsType<CreateKategoriSkemaCommandHandler>(handler);
            }

            var uuid = createResult.Value.ToString();

            // --- UPDATE ---
            if (mode == "updated")
            {
                var namaAfter = (string)afterData![0];
                var ruleAfter = (string)afterData[1];
                var updateCommand = new UpdateKategoriSkemaCommand(uuid, namaAfter, ruleAfter);
                var updateResult = await Sender.Send(updateCommand);

                Assert.True(updateResult.IsSuccess);
                var dataUpdate = DBContext.KategoriSkema.FirstOrDefault(p => p.Uuid.ToString() == uuid);
                Assert.NotNull(dataUpdate);
                Assert.Equal(namaAfter, dataUpdate.Nama);
                Assert.Equal(ruleAfter, dataUpdate.Rule);

                using (var scope = Factory.Services.CreateScope())
                {
                    var handler = scope.ServiceProvider
                        .GetService<IRequestHandler<UpdateKategoriSkemaCommand, Result>>();
                    Assert.NotNull(handler);
                    Assert.IsType<UpdateKategoriSkemaCommandHandler>(handler);
                }
            }

            // --- DELETE ---
            if (mode == "deleted")
            {
                var deleteCommand = new DeleteKategoriSkemaCommand(uuid);
                var deleteResult = await Sender.Send(deleteCommand);

                Assert.True(deleteResult.IsSuccess);
                var dataDeleted = DBContext.KategoriSkema.FirstOrDefault(p => p.Uuid.ToString() == uuid);
                Assert.Null(dataDeleted);

                using (var scope = Factory.Services.CreateScope())
                {
                    var handler = scope.ServiceProvider
                        .GetService<IRequestHandler<DeleteKategoriSkemaCommand, Result>>();
                    Assert.NotNull(handler);
                    Assert.IsType<DeleteKategoriSkemaCommandHandler>(handler);
                }
            }
        }
        [Fact]
        public async Task Create_ShouldThrow_WhenInvalidRuleDomain()
        {
            var nama = "tes";
            var rule = "**"; // contoh melanggar aturan domain

            var command = new CreateKategoriSkemaCommand(nama, rule);
            var result = await Sender.Send(command);

            Assert.True(result.IsFailure);
            Assert.Equal("KategoriSkema.InvalidFormatRule", result.Error.Code);
        }

        [Fact]
        public async Task Update_ShouldThrow_WhenNotExist()
        {
            var guid = Guid.NewGuid().ToString();
            var nama = "tes";
            var rule = "[]";

            var command = new UpdateKategoriSkemaCommand(guid, nama, rule);
            var result = await Sender.Send(command);

            Assert.True(result.IsFailure);
            Assert.Equal("KategoriSkema.NotFound", result.Error.Code);
        }

        [Fact]
        public async Task Update_ShouldThrow_WhenInvalidRuleDomain()
        {
            // --- CREATE ---
            var namaBefore = "tes";
            var ruleBefore = "[]";

            var createCommand = new CreateKategoriSkemaCommand(namaBefore, ruleBefore);
            var createResult = await Sender.Send(createCommand);

            Assert.True(createResult.IsSuccess);
            var dataCreate = DBContext.KategoriSkema.FirstOrDefault(p => p.Uuid == createResult!.Value);

            Assert.NotNull(dataCreate);
            Assert.Equal(namaBefore, dataCreate.Nama);
            Assert.Equal(ruleBefore, dataCreate.Rule);

            var newUuid = createResult.Value.ToString();

            // --- UPDATE (melanggar aturan domain)
            var namaAfter = "tes2";
            var ruleAfter = "**";

            var updateCommand = new UpdateKategoriSkemaCommand(newUuid, namaAfter, ruleAfter);
            var updateResult = await Sender.Send(updateCommand);

            Assert.True(updateResult.IsFailure);
            Assert.Equal("KategoriSkema.InvalidFormatRule", updateResult.Error.Code);
        }

        [Fact]
        public async Task Delete_ShouldThrow_WhenNotExist()
        {
            var guid = Guid.NewGuid().ToString();

            var command = new DeleteKategoriSkemaCommand(guid);
            var result = await Sender.Send(command);

            Assert.True(result.IsFailure);
            Assert.Equal("KategoriSkema.NotFound", result.Error.Code);
        }

        public class KategoriSkemaQueryHandlerTests
        {
            [Fact]
            public async Task GetAllKategoriSkema_Handle_ReturnsList_WhenDataExists()
            {
                // Arrange
                var mockConnectionFactory = new Mock<IDbConnectionFactory>();
                var mockConnection = new Mock<DbConnection>();

                var fakeData = new List<KategoriSkemaResponse>
            {
                new KategoriSkemaResponse
                {
                    Uuid = Guid.NewGuid().ToString(),
                    Nama = "Skema 1",
                    Rule = "[]"
                }
            };

                mockConnection.SetupDapperAsync(c =>
                    c.QueryAsync<KategoriSkemaResponse>(It.IsAny<string>(), null, null, null, null))
                    .ReturnsAsync(fakeData);

                mockConnectionFactory.Setup(f => f.OpenConnectionAsync())
                    .Returns(new ValueTask<DbConnection>(mockConnection.Object));

                var handler = new GetAllKategoriSkemaQueryHandler(mockConnectionFactory.Object);
                var query = new GetAllKategoriSkemaQuery();

                // Act
                var result = await handler.Handle(query, CancellationToken.None);

                // Assert
                Assert.True(result.IsSuccess);
                Assert.NotEmpty(result.Value);
                Assert.Equal(fakeData.First().Nama, result.Value.First().Nama);
            }

            [Fact]
            public async Task GetAllKategoriSkema_Handle_ReturnsFailure_WhenNoData()
            {
                // Arrange
                var mockConnectionFactory = new Mock<IDbConnectionFactory>();
                var mockConnection = new Mock<DbConnection>();

                mockConnection.SetupDapperAsync(c =>
                    c.QueryAsync<KategoriSkemaResponse>(It.IsAny<string>(), null, null, null, null))
                    .ReturnsAsync(new List<KategoriSkemaResponse>());

                mockConnectionFactory.Setup(f => f.OpenConnectionAsync())
                    .Returns(new ValueTask<DbConnection>(mockConnection.Object));

                var handler = new GetAllKategoriSkemaQueryHandler(mockConnectionFactory.Object);
                var query = new GetAllKategoriSkemaQuery();

                // Act
                var result = await handler.Handle(query, CancellationToken.None);

                // Assert
                Assert.False(result.IsSuccess);
            }

            [Fact]
            public async Task GetKategoriSkema_Handle_ReturnsSuccess_WhenDataExists()
            {
                // Arrange
                var mockConnectionFactory = new Mock<IDbConnectionFactory>();
                var mockConnection = new Mock<DbConnection>();
                var uuid = Guid.NewGuid();

                var fakeData = new KategoriSkemaResponse
                {
                    Uuid = uuid.ToString(),
                    Nama = "Skema 1",
                    Rule = "[]"
                };

                mockConnection.SetupDapperAsync(c =>
                    c.QuerySingleOrDefaultAsync<KategoriSkemaResponse?>(It.IsAny<string>(), It.IsAny<object>(), null, null, null))
                    .ReturnsAsync(fakeData);

                mockConnectionFactory.Setup(f => f.OpenConnectionAsync())
                    .Returns(new ValueTask<DbConnection>(mockConnection.Object));

                var handler = new GetKategoriSkemaQueryHandler(mockConnectionFactory.Object);
                var query = new GetKategoriSkemaQuery(uuid);

                // Act
                var result = await handler.Handle(query, CancellationToken.None);

                // Assert
                Assert.True(result.IsSuccess);
                Assert.Equal("Skema 1", result.Value.Nama);
            }

            [Fact]
            public async Task GetKategoriSkema_Handle_ReturnsFailure_WhenDataNotFound()
            {
                // Arrange
                var mockConnectionFactory = new Mock<IDbConnectionFactory>();
                var mockConnection = new Mock<DbConnection>();

                mockConnection.SetupDapperAsync(c =>
                    c.QuerySingleOrDefaultAsync<KategoriSkemaResponse?>(It.IsAny<string>(), It.IsAny<object>(), null, null, null))
                    .ReturnsAsync((KategoriSkemaResponse?)null);

                mockConnectionFactory.Setup(f => f.OpenConnectionAsync())
                    .Returns(new ValueTask<DbConnection>(mockConnection.Object));

                var handler = new GetKategoriSkemaQueryHandler(mockConnectionFactory.Object);
                var query = new GetKategoriSkemaQuery(Guid.NewGuid());

                // Act
                var result = await handler.Handle(query, CancellationToken.None);

                // Assert
                Assert.False(result.IsSuccess);
            }

            [Fact]
            public async Task GetKategoriSkemaDefault_Handle_ReturnsSuccess_WhenDataExists()
            {
                // Arrange
                var mockConnectionFactory = new Mock<IDbConnectionFactory>();
                var mockConnection = new Mock<DbConnection>();
                var uuid = Guid.NewGuid();

                var fakeData = new KategoriSkemaDefaultResponse
                {
                    Id = "1",
                    Uuid = uuid.ToString(),
                    Nama = "Skema Default",
                    Rule = "[]"
                };

                mockConnection.SetupDapperAsync(c =>
                    c.QuerySingleOrDefaultAsync<KategoriSkemaDefaultResponse?>(It.IsAny<string>(), It.IsAny<object>(), null, null, null))
                    .ReturnsAsync(fakeData);

                mockConnectionFactory.Setup(f => f.OpenConnectionAsync())
                    .Returns(new ValueTask<DbConnection>(mockConnection.Object));

                var handler = new GetKategoriSkemaDefaultQueryHandler(mockConnectionFactory.Object);
                var query = new GetKategoriSkemaDefaultQuery(uuid);

                // Act
                var result = await handler.Handle(query, CancellationToken.None);

                // Assert
                Assert.True(result.IsSuccess);
                Assert.Equal("Skema Default", result.Value.Nama);
            }

            [Fact]
            public async Task GetKategoriSkemaDefault_Handle_ReturnsFailure_WhenDataNotFound()
            {
                // Arrange
                var mockConnectionFactory = new Mock<IDbConnectionFactory>();
                var mockConnection = new Mock<DbConnection>();

                mockConnection.SetupDapperAsync(c =>
                    c.QuerySingleOrDefaultAsync<KategoriSkemaDefaultResponse?>(It.IsAny<string>(), It.IsAny<object>(), null, null, null))
                    .ReturnsAsync((KategoriSkemaDefaultResponse?)null);

                mockConnectionFactory.Setup(f => f.OpenConnectionAsync())
                    .Returns(new ValueTask<DbConnection>(mockConnection.Object));

                var handler = new GetKategoriSkemaDefaultQueryHandler(mockConnectionFactory.Object);
                var query = new GetKategoriSkemaDefaultQuery(Guid.NewGuid());

                // Act
                var result = await handler.Handle(query, CancellationToken.None);

                // Assert
                Assert.False(result.IsSuccess);
            }
        }
    }
}
