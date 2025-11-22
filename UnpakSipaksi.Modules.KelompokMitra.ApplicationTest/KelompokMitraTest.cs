using Dapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Moq.Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KelompokMitra.Application.CreateKelompokMitra;
using UnpakSipaksi.Modules.KelompokMitra.Application.DeleteKelompokMitra;
using UnpakSipaksi.Modules.KelompokMitra.Application.GetAllKelompokMitra;
using UnpakSipaksi.Modules.KelompokMitra.Application.GetKelompokMitra;
using UnpakSipaksi.Modules.KelompokMitra.Application.UpdateKelompokMitra;
using UnpakSipaksi.Modules.KelompokMitra.Domain.KelompokMitra;
using Xunit;

namespace UnpakSipaksi.Modules.KelompokMitra.ApplicationTest
{
    public class KelompokMitraTest : BaseIntegrationTest
    {
        public KelompokMitraTest(IntegrationTestWebAppFactory factory) : base(factory)
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
                var command = new CreateKelompokMitraCommand(nama);
                result = await Sender.Send(command);
            }
            else if (mode == "updated")
            {
                var command = new UpdateKelompokMitraCommand(uuid, nama);
                result = await Sender.Send(command);
            }
            else
            {
                var command = new DeleteKelompokMitraCommand(uuid);
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
            var createCommand = new CreateKelompokMitraCommand(namaBefore);
            var createResult = await Sender.Send(createCommand);

            Assert.True(createResult.IsSuccess);
            var dataCreate = DBContext.KelompokMitra.FirstOrDefault(p => p.Uuid == createResult!.Value);
            Assert.NotNull(dataCreate);
            Assert.Equal(namaBefore, dataCreate.Nama);

            using (var scope = Factory.Services.CreateScope())
            {
                var handler = scope.ServiceProvider
                    .GetService<IRequestHandler<CreateKelompokMitraCommand, Result<Guid>>>();
                Assert.NotNull(handler);
                Assert.IsType<CreateKelompokMitraCommandHandler>(handler);
            }

            var uuid = createResult.Value.ToString();

            // --- UPDATE ---
            if (mode == "updated")
            {
                var namaAfter = (string)afterData![0];
                var updateCommand = new UpdateKelompokMitraCommand(uuid, namaAfter);
                var updateResult = await Sender.Send(updateCommand);

                Assert.True(updateResult.IsSuccess);
                var dataUpdate = DBContext.KelompokMitra.FirstOrDefault(p => p.Uuid.ToString() == uuid);
                Assert.NotNull(dataUpdate);
                Assert.Equal(namaAfter, dataUpdate.Nama);

                using (var scope = Factory.Services.CreateScope())
                {
                    var handler = scope.ServiceProvider
                        .GetService<IRequestHandler<UpdateKelompokMitraCommand, Result>>();
                    Assert.NotNull(handler);
                    Assert.IsType<UpdateKelompokMitraCommandHandler>(handler);
                }
            }

            // --- DELETE ---
            if (mode == "deleted")
            {
                var deleteCommand = new DeleteKelompokMitraCommand(uuid);
                var deleteResult = await Sender.Send(deleteCommand);

                Assert.True(deleteResult.IsSuccess);
                var dataDeleted = DBContext.KelompokMitra.FirstOrDefault(p => p.Uuid.ToString() == uuid);
                Assert.Null(dataDeleted);

                using (var scope = Factory.Services.CreateScope())
                {
                    var handler = scope.ServiceProvider
                        .GetService<IRequestHandler<DeleteKelompokMitraCommand, Result>>();
                    Assert.NotNull(handler);
                    Assert.IsType<DeleteKelompokMitraCommandHandler>(handler);
                }
            }
        }

        [Fact]
        public async Task Update_ShouldThrow_WhenNotExist()
        {
            var guid = Guid.NewGuid().ToString();
            var namaBefore = "tes";

            var updateCommand = new UpdateKelompokMitraCommand(guid, namaBefore);
            var updateResult = await Sender.Send(updateCommand);

            Assert.True(updateResult.IsFailure);
            Assert.Equal("KelompokMitra.NotFound", updateResult.Error.Code);
        }

        [Fact]
        public async Task Delete_ShouldThrow_WhenNotExist()
        {
            var guid = Guid.NewGuid().ToString();

            var deleteCommand = new DeleteKelompokMitraCommand(guid);
            var deleteResult = await Sender.Send(deleteCommand);

            Assert.True(deleteResult.IsFailure);
            Assert.Equal("KelompokMitra.NotFound", deleteResult.Error.Code);
        }

        [Fact]
        public async Task GetAllKelompokMitra_ReturnsSuccess_WhenDataExists()
        {
            var mockFactory = new Mock<IDbConnectionFactory>();
            var mockConn = new Mock<DbConnection>();

            var fake = new List<KelompokMitraResponse>
            {
                new KelompokMitraResponse { Uuid = Guid.NewGuid().ToString(), Nama = "Mitra A" }
            };

            mockConn.SetupDapperAsync(c =>
                c.QueryAsync<KelompokMitraResponse>(It.IsAny<string>(), null, null, null, null)
            ).ReturnsAsync(fake);

            mockFactory
                .Setup(f => f.OpenConnectionAsync())
                .Returns(new ValueTask<DbConnection>(mockConn.Object));

            var handler = new GetAllKelompokMitraQueryHandler(mockFactory.Object);

            var result = await handler.Handle(new GetAllKelompokMitraQuery(), CancellationToken.None);

            Assert.True(result.IsSuccess);
            Assert.Single(result.Value);
            Assert.Equal(fake[0].Nama, result.Value[0].Nama);
        }

        [Fact]
        public async Task GetAllKelompokMitra_ReturnsFailure_WhenEmptyData()
        {
            var mockFactory = new Mock<IDbConnectionFactory>();
            var mockConn = new Mock<DbConnection>();

            mockConn.SetupDapperAsync(c =>
                c.QueryAsync<KelompokMitraResponse>(It.IsAny<string>(), null, null, null, null)
            ).ReturnsAsync(new List<KelompokMitraResponse>());

            mockFactory
                .Setup(f => f.OpenConnectionAsync())
                .Returns(new ValueTask<DbConnection>(mockConn.Object));

            var handler = new GetAllKelompokMitraQueryHandler(mockFactory.Object);

            var result = await handler.Handle(new GetAllKelompokMitraQuery(), CancellationToken.None);

            Assert.True(result.IsFailure);
            Assert.Equal(KelompokMitraErrors.EmptyData().Code, result.Error.Code);
        }

        [Fact]
        public async Task GetKelompokMitraById_ReturnsSuccess_WhenDataExists()
        {
            var mockFactory = new Mock<IDbConnectionFactory>();
            var mockConn = new Mock<DbConnection>();
            var uuid = Guid.NewGuid();

            var fake = new KelompokMitraResponse
            {
                Uuid = uuid.ToString(),
                Nama = "Mitra X"
            };

            mockConn.SetupDapperAsync(c =>
                c.QuerySingleOrDefaultAsync<KelompokMitraResponse?>(
                    It.IsAny<string>(), It.IsAny<object>(), null, null, null
                )
            ).ReturnsAsync(fake);

            mockFactory
                .Setup(f => f.OpenConnectionAsync())
                .Returns(new ValueTask<DbConnection>(mockConn.Object));

            var handler = new GetKelompokMitraQueryHandler(mockFactory.Object);
            var query = new GetKelompokMitraQuery(uuid);

            var result = await handler.Handle(query, CancellationToken.None);

            Assert.True(result.IsSuccess);
            Assert.Equal("Mitra X", result.Value.Nama);
        }

        [Fact]
        public async Task GetKelompokMitraById_ReturnsFailure_WhenNotFound()
        {
            var mockFactory = new Mock<IDbConnectionFactory>();
            var mockConn = new Mock<DbConnection>();

            mockConn.SetupDapperAsync(c =>
                c.QuerySingleOrDefaultAsync<KelompokMitraResponse?>(
                    It.IsAny<string>(), It.IsAny<object>(), null, null, null
                )
            ).ReturnsAsync((KelompokMitraResponse?)null);

            mockFactory
                .Setup(f => f.OpenConnectionAsync())
                .Returns(new ValueTask<DbConnection>(mockConn.Object));

            var handler = new GetKelompokMitraQueryHandler(mockFactory.Object);
            var query = new GetKelompokMitraQuery(Guid.NewGuid());

            var result = await handler.Handle(query, CancellationToken.None);

            Assert.True(result.IsFailure);
        }

        [Fact]
        public async Task GetKelompokMitraDefault_ReturnsSuccess_WhenDataExists()
        {
            var mockFactory = new Mock<IDbConnectionFactory>();
            var mockConn = new Mock<DbConnection>();
            var uuid = Guid.NewGuid();

            var fake = new KelompokMitraDefaultResponse
            {
                Id = "1",
                Uuid = uuid.ToString(),
                Nama = "Default Mitra"
            };

            mockConn.SetupDapperAsync(c =>
                c.QuerySingleOrDefaultAsync<KelompokMitraDefaultResponse?>(
                    It.IsAny<string>(), It.IsAny<object>(), null, null, null
                )
            ).ReturnsAsync(fake);

            mockFactory
                .Setup(f => f.OpenConnectionAsync())
                .Returns(new ValueTask<DbConnection>(mockConn.Object));

            var handler = new GetKelompokMitraDefaultQueryHandler(mockFactory.Object);
            var query = new GetKelompokMitraDefaultQuery(uuid);

            var result = await handler.Handle(query, CancellationToken.None);

            Assert.True(result.IsSuccess);
            Assert.Equal("Default Mitra", result.Value.Nama);
        }

        [Fact]
        public async Task GetKelompokMitraDefault_ReturnsFailure_WhenNotFound()
        {
            var mockFactory = new Mock<IDbConnectionFactory>();
            var mockConn = new Mock<DbConnection>();

            mockConn.SetupDapperAsync(c =>
                c.QuerySingleOrDefaultAsync<KelompokMitraDefaultResponse?>(
                    It.IsAny<string>(), It.IsAny<object>(), null, null, null
                )
            ).ReturnsAsync((KelompokMitraDefaultResponse?)null);

            mockFactory
                .Setup(f => f.OpenConnectionAsync())
                .Returns(new ValueTask<DbConnection>(mockConn.Object));

            var handler = new GetKelompokMitraDefaultQueryHandler(mockFactory.Object);
            var query = new GetKelompokMitraDefaultQuery(Guid.NewGuid());

            var result = await handler.Handle(query, CancellationToken.None);

            Assert.True(result.IsFailure);
        }
    }
}
