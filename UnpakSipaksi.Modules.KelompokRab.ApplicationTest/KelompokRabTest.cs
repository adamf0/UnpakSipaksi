using Dapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Moq.Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KelompokRab.Application.CreateKelompokRab;
using UnpakSipaksi.Modules.KelompokRab.Application.DeleteKelompokRab;
using UnpakSipaksi.Modules.KelompokRab.Application.GetAllKelompokRab;
using UnpakSipaksi.Modules.KelompokRab.Application.GetKelompokRab;
using UnpakSipaksi.Modules.KelompokRab.Application.UpdateKelompokRab;
using UnpakSipaksi.Modules.KelompokRab.Domain.KelompokRab;
using Xunit;

namespace UnpakSipaksi.Modules.KelompokRab.ApplicationTest
{
    public class KelompokRabTest : BaseIntegrationTest
    {
        public KelompokRabTest(IntegrationTestWebAppFactory factory) : base(factory)
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
                var command = new CreateKelompokRabCommand(nama);
                result = await Sender.Send(command);
            }
            else if (mode == "updated")
            {
                var command = new UpdateKelompokRabCommand(uuid, nama);
                result = await Sender.Send(command);
            }
            else
            {
                var command = new DeleteKelompokRabCommand(uuid);
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
            var createCommand = new CreateKelompokRabCommand(namaBefore);
            var createResult = await Sender.Send(createCommand);

            Assert.True(createResult.IsSuccess);
            var dataCreate = DBContext.KelompokRab.FirstOrDefault(p => p.Uuid == createResult!.Value);
            Assert.NotNull(dataCreate);
            Assert.Equal(namaBefore, dataCreate.Nama);

            using (var scope = Factory.Services.CreateScope())
            {
                var handler = scope.ServiceProvider
                    .GetService<IRequestHandler<CreateKelompokRabCommand, Result<Guid>>>();
                Assert.NotNull(handler);
                Assert.IsType<CreateKelompokRabCommandHandler>(handler);
            }

            var uuid = createResult.Value.ToString();

            // --- UPDATE ---
            if (mode == "updated")
            {
                var namaAfter = (string)afterData![0];
                var updateCommand = new UpdateKelompokRabCommand(uuid, namaAfter);
                var updateResult = await Sender.Send(updateCommand);

                Assert.True(updateResult.IsSuccess);
                var dataUpdate = DBContext.KelompokRab.FirstOrDefault(p => p.Uuid.ToString() == uuid);
                Assert.NotNull(dataUpdate);
                Assert.Equal(namaAfter, dataUpdate.Nama);

                using (var scope = Factory.Services.CreateScope())
                {
                    var handler = scope.ServiceProvider
                        .GetService<IRequestHandler<UpdateKelompokRabCommand, Result>>();
                    Assert.NotNull(handler);
                    Assert.IsType<UpdateKelompokRabCommandHandler>(handler);
                }
            }

            // --- DELETE ---
            if (mode == "deleted")
            {
                var deleteCommand = new DeleteKelompokRabCommand(uuid);
                var deleteResult = await Sender.Send(deleteCommand);

                Assert.True(deleteResult.IsSuccess);
                var dataDeleted = DBContext.KelompokRab.FirstOrDefault(p => p.Uuid.ToString() == uuid);
                Assert.Null(dataDeleted);

                using (var scope = Factory.Services.CreateScope())
                {
                    var handler = scope.ServiceProvider
                        .GetService<IRequestHandler<DeleteKelompokRabCommand, Result>>();
                    Assert.NotNull(handler);
                    Assert.IsType<DeleteKelompokRabCommandHandler>(handler);
                }
            }
        }

        [Fact]
        public async Task Update_ShouldThrow_WhenNotExist()
        {
            var guid = Guid.NewGuid().ToString();
            var namaBefore = "tes";

            var updateCommand = new UpdateKelompokRabCommand(guid, namaBefore);
            var updateResult = await Sender.Send(updateCommand);

            Assert.True(updateResult.IsFailure);
            Assert.Equal("KelompokRab.NotFound", updateResult.Error.Code);
        }

        [Fact]
        public async Task Delete_ShouldThrow_WhenNotExist()
        {
            var guid = Guid.NewGuid().ToString();

            var deleteCommand = new DeleteKelompokRabCommand(guid);
            var deleteResult = await Sender.Send(deleteCommand);

            Assert.True(deleteResult.IsFailure);
            Assert.Equal("KelompokRab.NotFound", deleteResult.Error.Code);
        }

        [Fact]
        public async Task GetAll_ReturnsList_WhenDataExists()
        {
            var mockFactory = new Mock<IDbConnectionFactory>();
            var mockConn = new Mock<DbConnection>();

            var fake = new List<KelompokRabResponse>
            {
                new() { Uuid = Guid.NewGuid().ToString(), Nama = "Data 1" }
            };

            mockConn.SetupDapperAsync(c =>
                c.QueryAsync<KelompokRabResponse>(It.IsAny<string>(), null, null, null, null)
            ).ReturnsAsync(fake);

            mockFactory.Setup(f => f.OpenConnectionAsync())
                .Returns(new ValueTask<DbConnection>(mockConn.Object));

            var handler = new GetAllKelompokRabQueryHandler(mockFactory.Object);
            var result = await handler.Handle(new GetAllKelompokRabQuery(), CancellationToken.None);

            Assert.True(result.IsSuccess);
            Assert.Single(result.Value);
            Assert.Equal("Data 1", result.Value[0].Nama);
        }

        [Fact]
        public async Task GetAll_ReturnsFailure_WhenEmpty()
        {
            var mockFactory = new Mock<IDbConnectionFactory>();
            var mockConn = new Mock<DbConnection>();

            mockConn.SetupDapperAsync(c =>
                c.QueryAsync<KelompokRabResponse>(It.IsAny<string>(), null, null, null, null)
            ).ReturnsAsync(new List<KelompokRabResponse>());

            mockFactory.Setup(f => f.OpenConnectionAsync())
                .ReturnsAsync(mockConn.Object);

            var handler = new GetAllKelompokRabQueryHandler(mockFactory.Object);
            var result = await handler.Handle(new GetAllKelompokRabQuery(), CancellationToken.None);

            Assert.False(result.IsSuccess);
            Assert.Equal(KelompokRabErrors.EmptyData().Code, result.Error.Code);
        }

        [Fact]
        public async Task GetDefault_ReturnsSuccess_WhenFound()
        {
            var mockFactory = new Mock<IDbConnectionFactory>();
            var mockConn = new Mock<DbConnection>();

            var uuid = Guid.NewGuid();

            var fake = new KelompokRabDefaultResponse
            {
                Id = "1",
                Uuid = uuid.ToString(),
                Nama = "Default Data"
            };

            mockConn.SetupDapperAsync(c =>
                c.QuerySingleOrDefaultAsync<KelompokRabDefaultResponse?>(It.IsAny<string>(),
                    It.IsAny<object>(), null, null, null)
            ).ReturnsAsync(fake);

            mockFactory.Setup(f => f.OpenConnectionAsync())
                .Returns(new ValueTask<DbConnection>(mockConn.Object));

            var handler = new GetKelompokRabDefaultQueryHandler(mockFactory.Object);
            var query = new GetKelompokRabDefaultQuery(uuid);
            var result = await handler.Handle(query, CancellationToken.None);

            Assert.True(result.IsSuccess);
            Assert.Equal("Default Data", result.Value.Nama);
        }

        [Fact]
        public async Task GetDefault_ReturnsFailure_WhenNotFound()
        {
            var mockFactory = new Mock<IDbConnectionFactory>();
            var mockConn = new Mock<DbConnection>();

            mockConn.SetupDapperAsync(c =>
                c.QuerySingleOrDefaultAsync<KelompokRabDefaultResponse?>(It.IsAny<string>(),
                    It.IsAny<object>(), null, null, null)
            ).ReturnsAsync((KelompokRabDefaultResponse?)null);

            mockFactory.Setup(f => f.OpenConnectionAsync())
                .Returns(new ValueTask<DbConnection>(mockConn.Object));

            var handler = new GetKelompokRabDefaultQueryHandler(mockFactory.Object);
            var query = new GetKelompokRabDefaultQuery(Guid.NewGuid());
            var result = await handler.Handle(query, CancellationToken.None);

            Assert.False(result.IsSuccess);
            Assert.Equal(KelompokRabErrors.NotFound(query.KelompokRabUuid).Code, result.Error.Code);
        }

        [Fact]
        public async Task GetByUuid_ReturnsSuccess_WhenFound()
        {
            var mockFactory = new Mock<IDbConnectionFactory>();
            var mockConn = new Mock<DbConnection>();

            var uuid = Guid.NewGuid();

            var fake = new KelompokRabResponse
            {
                Uuid = uuid.ToString(),
                Nama = "Some Data"
            };

            mockConn.SetupDapperAsync(c =>
                c.QuerySingleOrDefaultAsync<KelompokRabResponse?>(It.IsAny<string>(),
                    It.IsAny<object>(), null, null, null)
            ).ReturnsAsync(fake);

            mockFactory.Setup(f => f.OpenConnectionAsync())
                .Returns(new ValueTask<DbConnection>(mockConn.Object));

            var handler = new GetKelompokRabQueryHandler(mockFactory.Object);
            var result = await handler.Handle(new GetKelompokRabQuery(uuid), CancellationToken.None);

            Assert.True(result.IsSuccess);
            Assert.Equal("Some Data", result.Value.Nama);
        }

        [Fact]
        public async Task GetByUuid_ReturnsFailure_WhenNotFound()
        {
            var mockFactory = new Mock<IDbConnectionFactory>();
            var mockConn = new Mock<DbConnection>();

            mockConn.SetupDapperAsync(c =>
                c.QuerySingleOrDefaultAsync<KelompokRabResponse?>(It.IsAny<string>(),
                    It.IsAny<object>(), null, null, null)
            ).ReturnsAsync((KelompokRabResponse?)null);

            mockFactory.Setup(f => f.OpenConnectionAsync())
                .Returns(new ValueTask<DbConnection>(mockConn.Object));

            var handler = new GetKelompokRabQueryHandler(mockFactory.Object);
            var uuid = Guid.NewGuid();
            var result = await handler.Handle(new GetKelompokRabQuery(uuid), CancellationToken.None);

            Assert.False(result.IsSuccess);
            Assert.Equal(KelompokRabErrors.NotFound(uuid).Code, result.Error.Code);
        }
    }
}
