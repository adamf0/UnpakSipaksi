using Dapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Moq.Dapper;
using System.Data.Common;
using System.Reflection;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.AkurasiPenelitian.Application.GetAkurasiPenelitian;
using UnpakSipaksi.Modules.KesesuaianJadwal.Application.CreateKesesuaianJadwal;
using UnpakSipaksi.Modules.KesesuaianJadwal.Application.DeleteKesesuaianJadwal;
using UnpakSipaksi.Modules.KesesuaianJadwal.Application.GetAllKesesuaianJadwal;
using UnpakSipaksi.Modules.KesesuaianJadwal.Application.GetBobotKesesuaianJadwal;
using UnpakSipaksi.Modules.KesesuaianJadwal.Application.GetBobotKesesuaianJadwal;
using UnpakSipaksi.Modules.KesesuaianJadwal.Application.GetKesesuaianJadwal;
using UnpakSipaksi.Modules.KesesuaianJadwal.Application.UpdateKesesuaianJadwal;
using UnpakSipaksi.Modules.KesesuaianJadwal.ApplicationTest;
using UnpakSipaksi.Modules.KesesuaianJadwal.Domain.KesesuaianJadwal;
using Xunit;

namespace Application.Integration.Tests
{
    public class KesesuaianJadwalTest : BaseIntegrationTest
    {
        public KesesuaianJadwalTest(IntegrationTestWebAppFactory factory) : base(factory)
        {
        }

        public static IEnumerable<object[]> InvalidData()
        {
            var valid = Guid.NewGuid().ToString();
            var empty = "";

            // CREATE
            yield return new object[] { empty, "", 10, "'Nama' tidak boleh kosong.", "created" };
            yield return new object[] { empty, "tes", -10, "'Nilai' tidak boleh negative.", "created" };

            // UPDATE
            yield return new object[] { valid, "", 10, "'Nama' tidak boleh kosong.", "updated" };
            yield return new object[] { valid, "tes", -10, "'Nilai' tidak boleh negative.", "updated" };
            yield return new object[] { empty, "tes", 10, "'Uuid' tidak boleh kosong.", "updated" };
            yield return new object[] { "no-guid", "tes", 10, "'Uuid' harus dalam format UUID v4 yang valid.", "updated" };

            // DELETE
            yield return new object[] { empty, "tes", 10, "'Uuid' tidak boleh kosong.", "deleted" };
            yield return new object[] { "no-guid", "tes", 10, "'Uuid' harus dalam format UUID v4 yang valid.", "deleted" };
        }

        public static IEnumerable<object?[]> ValidData()
        {
            yield return new object?[] { new object[] { "tes", 100 }, null, "created" };
            yield return new object?[] { new object[] { "tes", 100 }, new object[] { "tes2", 200 }, "updated" };
            yield return new object?[] { new object[] { "tes", 100 }, null, "deleted" };
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
                var command = new CreateKesesuaianJadwalCommand(nama, nilai);
                result = await Sender.Send(command);
            }
            else if (mode == "updated")
            {
                var command = new UpdateKesesuaianJadwalCommand(uuid, nama, nilai);
                result = await Sender.Send(command);
            }
            else
            {
                var command = new DeleteKesesuaianJadwalCommand(uuid);
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

            var createCommand = new CreateKesesuaianJadwalCommand(namaBefore, nilaiBefore);
            var createResult = await Sender.Send(createCommand);

            Assert.True(createResult.IsSuccess);
            var dataCreate = DBContext.KesesuaianJadwal.FirstOrDefault(p => p.Uuid == createResult!.Value);

            Assert.NotNull(dataCreate);
            Assert.Equal(namaBefore, dataCreate.Nama);
            Assert.Equal(nilaiBefore, dataCreate.Nilai);

            using (var scope = Factory.Services.CreateScope())
            {
                var handler = scope.ServiceProvider
                    .GetService<IRequestHandler<CreateKesesuaianJadwalCommand, Result<Guid>>>();

                Assert.NotNull(handler);
                Assert.IsType<CreateKesesuaianJadwalCommandHandler>(handler);
            }

            var newUuid = createResult.Value.ToString();

            // --- UPDATE / DELETE ---
            if (mode == "updated")
            {
                var namaAfter = (string)afterData![0];
                var nilaiAfter = (int)afterData[1];

                var updateCommand = new UpdateKesesuaianJadwalCommand(newUuid, namaAfter, nilaiAfter);
                var updateResult = await Sender.Send(updateCommand);

                Assert.True(updateResult.IsSuccess);
                var dataUpdate = DBContext.KesesuaianJadwal.FirstOrDefault(p => p.Uuid == createResult!.Value);

                Assert.NotNull(dataUpdate);
                Assert.Equal(namaAfter, dataUpdate.Nama);
                Assert.Equal(nilaiAfter, dataUpdate.Nilai);

                using (var scope = Factory.Services.CreateScope())
                {
                    var handler = scope.ServiceProvider
                        .GetService<IRequestHandler<UpdateKesesuaianJadwalCommand, Result>>();

                    Assert.NotNull(handler);
                    Assert.IsType<UpdateKesesuaianJadwalCommandHandler>(handler);
                }
            }
            else if (mode == "deleted")
            {
                var deleteCommand = new DeleteKesesuaianJadwalCommand(newUuid);
                var deleteResult = await Sender.Send(deleteCommand);

                Assert.True(deleteResult.IsSuccess);

                using (var scope = Factory.Services.CreateScope())
                {
                    var handler = scope.ServiceProvider
                        .GetService<IRequestHandler<DeleteKesesuaianJadwalCommand, Result>>();

                    Assert.NotNull(handler);
                    Assert.IsType<DeleteKesesuaianJadwalCommandHandler>(handler);
                }
            }
        }

        [Fact]
        public async Task Create_ShouldThrow_WhenInvalidRuleDomain()
        {
            var nama = "tes";
            var nilai = int.MaxValue; // contoh melanggar aturan domain

            var command = new CreateKesesuaianJadwalCommand(nama, nilai);
            var result = await Sender.Send(command);

            Assert.True(result.IsFailure);
            Assert.Equal("KesesuaianJadwal.InvalidValueNilai", result.Error.Code);
        }

        [Fact]
        public async Task Update_ShouldThrow_WhenNotExist()
        {
            var guid = Guid.NewGuid().ToString();
            var nama = "tes";
            var nilai = 10;

            var command = new UpdateKesesuaianJadwalCommand(guid, nama, nilai);
            var result = await Sender.Send(command);

            Assert.True(result.IsFailure);
            Assert.Equal("KesesuaianJadwal.NotFound", result.Error.Code);
        }

        [Fact]
        public async Task Update_ShouldThrow_WhenInvalidRuleDomain()
        {
            // --- CREATE ---
            var namaBefore = "tes";
            var nilaiBefore = 10;

            var createCommand = new CreateKesesuaianJadwalCommand(namaBefore, nilaiBefore);
            var createResult = await Sender.Send(createCommand);

            Assert.True(createResult.IsSuccess);
            var dataCreate = DBContext.KesesuaianJadwal.FirstOrDefault(p => p.Uuid == createResult!.Value);

            Assert.NotNull(dataCreate);
            Assert.Equal(namaBefore, dataCreate.Nama);
            Assert.Equal(nilaiBefore, dataCreate.Nilai);

            var newUuid = createResult.Value.ToString();

            // --- UPDATE (melanggar aturan domain)
            var namaAfter = "tes2";
            var nilaiAfter = int.MaxValue;

            var updateCommand = new UpdateKesesuaianJadwalCommand(newUuid, namaAfter, nilaiAfter);
            var updateResult = await Sender.Send(updateCommand);

            Assert.True(updateResult.IsFailure);
            Assert.Equal("KesesuaianJadwal.InvalidValueNilai", updateResult.Error.Code);
        }

        [Fact]
        public async Task Delete_ShouldThrow_WhenNotExist()
        {
            var guid = Guid.NewGuid().ToString();

            var command = new DeleteKesesuaianJadwalCommand(guid);
            var result = await Sender.Send(command);

            Assert.True(result.IsFailure);
            Assert.Equal("KesesuaianJadwal.NotFound", result.Error.Code);
        }

        [Fact]
        public async Task GetAllKesesuaianJadwal_ShouldReturnList_WhenDataExists()
        {
            var mockConnectionFactory = new Mock<IDbConnectionFactory>();
            var mockConnection = new Mock<DbConnection>();

            var fakeData = new List<KesesuaianJadwalResponse>
            {
                new KesesuaianJadwalResponse
                {
                    Uuid = Guid.NewGuid().ToString(),
                    Nama = "Nama1",
                    Nilai = "5"
                }
            };

            // Setup Dapper extension
            mockConnection.SetupDapperAsync(c => c.QueryAsync<KesesuaianJadwalResponse>(
            It.IsAny<string>(), null, null, null, null))
            .ReturnsAsync(fakeData);

            mockConnectionFactory
             .Setup(f => f.OpenConnectionAsync())
             .Returns(new ValueTask<DbConnection>(mockConnection.Object));

            var handler = new GetAllKesesuaianJadwalQueryHandler(mockConnectionFactory.Object);
            var result = await handler.Handle(new GetAllKesesuaianJadwalQuery(), CancellationToken.None);

            Assert.True(result.IsSuccess);
            Assert.Single(result.Value);
        }

        [Fact]
        public async Task GetAllKesesuaianJadwal_ShouldReturnFailure_WhenEmpty()
        {
            // Arrange
            var mockConnectionFactory = new Mock<IDbConnectionFactory>();
            var mockConnection = new Mock<DbConnection>();

            // Empty data
            mockConnection.SetupDapperAsync(c => c.QueryAsync<KesesuaianJadwalResponse>(
                It.IsAny<string>(), null, null, null, null))
                .ReturnsAsync(new List<KesesuaianJadwalResponse>());

            mockConnectionFactory.Setup(f => f.OpenConnectionAsync())
                .ReturnsAsync(mockConnection.Object);

            var handler = new GetAllKesesuaianJadwalQueryHandler(mockConnectionFactory.Object);
            var result = await handler.Handle(new GetAllKesesuaianJadwalQuery(), CancellationToken.None);

            Assert.True(result.IsFailure);
        }

        [Fact]
        public async Task GetKesesuaianJadwalDefault_ShouldReturnData_WhenExists()
        {
            // Arrange
            var mockConnectionFactory = new Mock<IDbConnectionFactory>();
            var mockConnection = new Mock<DbConnection>();
            var uuid = Guid.NewGuid();

            var fakeData = new KesesuaianJadwalDefaultResponse
            {
                Id = "1",
                Uuid = uuid.ToString(),
                Nama = "Nama1",
                Nilai = 5
            };

            mockConnection.SetupDapperAsync(c =>
                c.QuerySingleOrDefaultAsync<KesesuaianJadwalDefaultResponse?>(
                    It.IsAny<string>(),
                    It.IsAny<object>(),
                    null, null, null
                )
            ).ReturnsAsync(fakeData);

            mockConnectionFactory
                .Setup(f => f.OpenConnectionAsync())
                .Returns(new ValueTask<DbConnection>(mockConnection.Object));

            var handler = new GetKesesuaianJadwalDefaultQueryHandler(mockConnectionFactory.Object);

            var result = await handler.Handle(
                new GetKesesuaianJadwalDefaultQuery(uuid),
                CancellationToken.None);

            Assert.True(result.IsSuccess);
            Assert.Equal(uuid.ToString(), result.Value.Uuid);
        }

        [Fact]
        public async Task GetKesesuaianJadwalDefault_ShouldReturnFailure_WhenNotFound()
        {
            // Arrange
            var mockConnectionFactory = new Mock<IDbConnectionFactory>();
            var mockConnection = new Mock<DbConnection>();
            var uuid = Guid.NewGuid();

            mockConnection.SetupDapperAsync(c =>
                c.QuerySingleOrDefaultAsync<KesesuaianJadwalDefaultResponse?>(
                    It.IsAny<string>(),
                    It.IsAny<object>(),
                    null, null, null
                )
            ).ReturnsAsync((KesesuaianJadwalDefaultResponse?) null);

            mockConnectionFactory
                .Setup(f => f.OpenConnectionAsync())
                .Returns(new ValueTask<DbConnection>(mockConnection.Object));

            var handler = new GetKesesuaianJadwalDefaultQueryHandler(mockConnectionFactory.Object);

            var result = await handler.Handle(
                new GetKesesuaianJadwalDefaultQuery(Guid.NewGuid()),
                CancellationToken.None);

            Assert.True(result.IsFailure);
        }

        [Fact]
        public async Task GetKesesuaianJadwalById_ShouldReturnData_WhenFound()
        {
            // Arrange
            var mockConnectionFactory = new Mock<IDbConnectionFactory>();
            var mockConnection = new Mock<DbConnection>();
            var uuid = Guid.NewGuid();

            var fakeData = new KesesuaianJadwalResponse
            {
                Uuid = uuid.ToString(),
                Nama = "Nama1",
                Nilai = "20"
            };

            mockConnection.SetupDapperAsync(c =>
                c.QuerySingleOrDefaultAsync<KesesuaianJadwalResponse?>(
                    It.IsAny<string>(),
                    It.IsAny<object>(),
                    null, null, null
                )
            ).ReturnsAsync(fakeData);

            mockConnectionFactory
                .Setup(f => f.OpenConnectionAsync())
                .Returns(new ValueTask<DbConnection>(mockConnection.Object));

            var handler = new GetKesesuaianJadwalQueryHandler(mockConnectionFactory.Object);

            var result = await handler.Handle(
                new GetKesesuaianJadwalQuery(uuid),
                CancellationToken.None);

            Assert.True(result.IsSuccess);
            Assert.Equal(uuid.ToString(), result.Value.Uuid);
        }

        [Fact]
        public async Task GetKesesuaianJadwalById_ShouldReturnFailure_WhenNotFound()
        {
            // Arrange
            var mockConnectionFactory = new Mock<IDbConnectionFactory>();
            var mockConnection = new Mock<DbConnection>();
            var uuid = Guid.NewGuid();

            mockConnection.SetupDapperAsync(c =>
                c.QuerySingleOrDefaultAsync<KesesuaianJadwalResponse?>(
                    It.IsAny<string>(),
                    It.IsAny<object>(),
                    null, null, null
                )
            ).ReturnsAsync((KesesuaianJadwalResponse?) null);

            mockConnectionFactory
                .Setup(f => f.OpenConnectionAsync())
                .Returns(new ValueTask<DbConnection>(mockConnection.Object));

            var handler = new GetKesesuaianJadwalQueryHandler(mockConnectionFactory.Object);

            var result = await handler.Handle(
                new GetKesesuaianJadwalQuery(Guid.NewGuid()),
                CancellationToken.None);

            Assert.True(result.IsFailure);
        }

        [Fact]
        public async Task GetBobotKesesuaianJadwal_ShouldReturnFailure_WhenValueIsNull()
        {
            var mockFactory = new Mock<IDbConnectionFactory>();
            var mockConn = new Mock<DbConnection>();

            mockConn.SetupDapperAsync(c =>
                c.QuerySingleOrDefaultAsync<int?>(
                    It.IsAny<string>(),
                    It.IsAny<object>(),
                    null, null, null
                )
            ).ReturnsAsync((int?)null);

            mockFactory.Setup(f => f.OpenConnectionAsync())
                .ReturnsAsync(mockConn.Object);

            var handler = new GetBobotKesesuaianJadwalQueryHandler(mockFactory.Object);

            var result = await handler.Handle(new GetBobotKesesuaianJadwalQuery(), CancellationToken.None);

            Assert.False(result.IsSuccess);
        }

        [Fact]
        public async Task GetBobotKesesuaianJadwal_ShouldReturnFailure_WhenEmptyList()
        {
            var mockFactory = new Mock<IDbConnectionFactory>();
            var mockConn = new Mock<DbConnection>();

            // Return empty list
            mockConn.SetupDapperAsync(c =>
                c.QueryAsync<int>(
                    It.IsAny<string>(), null, null, null, null
                )
            ).ReturnsAsync(new List<int>());

            mockFactory.Setup(f => f.OpenConnectionAsync())
                .ReturnsAsync(mockConn.Object);

            var handler = new GetBobotKesesuaianJadwalQueryHandler(mockFactory.Object);

            var result = await handler.Handle(new GetBobotKesesuaianJadwalQuery(), CancellationToken.None);

            Assert.True(result.IsFailure);
            Assert.Equal(KesesuaianJadwalErrors.EmptyData().Code, result.Error.Code);
        }

        [Fact]
        public async Task GetBobotKesesuaianJadwal_ShouldReturnFailure_WhenMoreThanOneValue()
        {
            var mockFactory = new Mock<IDbConnectionFactory>();
            var mockConn = new Mock<DbConnection>();

            // Return 2 values
            mockConn.SetupDapperAsync(c =>
                c.QueryAsync<int>(
                    It.IsAny<string>(), null, null, null, null
                )
            ).ReturnsAsync(new List<int> { 5, 7 });

            mockFactory.Setup(f => f.OpenConnectionAsync())
                .ReturnsAsync(mockConn.Object);

            var handler = new GetBobotKesesuaianJadwalQueryHandler(mockFactory.Object);

            var result = await handler.Handle(new GetBobotKesesuaianJadwalQuery(), CancellationToken.None);

            Assert.True(result.IsFailure);
            Assert.Equal(KesesuaianJadwalErrors.NotSameValue().Code, result.Error.Code);
        }


    }
}
