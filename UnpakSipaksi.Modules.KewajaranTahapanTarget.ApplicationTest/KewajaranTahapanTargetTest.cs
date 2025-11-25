using Dapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Moq.Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KewajaranTahapanTarget.Application.GetAllKewajaranTahapanTarget;
using UnpakSipaksi.Modules.KewajaranTahapanTarget.Application.GetBobotKewajaranTahapanTarget;
using UnpakSipaksi.Modules.KewajaranTahapanTarget.Application.GetKewajaranTahapanTarget;
using UnpakSipaksi.Modules.KewajaranTahapanTarget.Domain.KewajaranTahapanTarget;
using UnpakSipaksi.Modules.KewajaranTahapanTarget.Application.CreateKewajaranTahapanTarget;
using UnpakSipaksi.Modules.KewajaranTahapanTarget.Application.DeleteKewajaranTahapanTarget;
using UnpakSipaksi.Modules.KewajaranTahapanTarget.Application.UpdateKewajaranTahapanTarget;
using UnpakSipaksi.Modules.KewajaranTahapanTarget.ApplicationTest;
using Xunit;

namespace Application.Integration.Tests
{
    public class KewajaranTahapanTargetTest : BaseIntegrationTest
    {
        public KewajaranTahapanTargetTest(IntegrationTestWebAppFactory factory) : base(factory)
        {
        }

        public static IEnumerable<object[]> InvalidData()
        {
            var valid = Guid.NewGuid().ToString();
            var empty = "";

            // CREATE
            yield return new object[] { empty, "", 10, "'Nama' tidak boleh kosong.", "created" };
            yield return new object[] { empty, "Tes", -100, "'Nilai' tidak boleh negative.", "created" };

            // UPDATE
            yield return new object[] { valid, "", 10, "'Nama' tidak boleh kosong.", "updated" };
            yield return new object[] { empty, "Tes", 10, "'Uuid' tidak boleh kosong.", "updated" };
            yield return new object[] { "no-guid", "Tes", 10, "'Uuid' harus dalam format UUID v4 yang valid.", "updated" };
            yield return new object[] { valid, "Tes", -10, "'Nilai' tidak boleh negative.", "updated" };

            // DELETE
            yield return new object[] { empty, "Tes", 10, "'Uuid' tidak boleh kosong.", "deleted" };
            yield return new object[] { "no-guid", "Tes", 10, "'Uuid' harus dalam format UUID v4 yang valid.", "deleted" };
        }

        public static IEnumerable<object?[]> ValidData()
        {
            yield return new object?[] { new object[] { "Tes", 1000 }, null, "created" };
            yield return new object?[] { new object[] { "Tes", 1000 }, new object[] { "Tes Updated", 2000 }, "updated" };
            yield return new object?[] { new object[] { "Tes", 1000 }, null, "deleted" };
        }

        [Theory]
        [MemberData(nameof(InvalidData))]
        public async Task CreateUpdateDelete_ShouldThrow_WhenInvalid(
            string uuid,
            string nama,
            int nilai,
            string message,
            string mode)
        {
            Result? result = null;

            if (mode == "created")
            {
                var command = new CreateKewajaranTahapanTargetCommand(nama, nilai);
                result = await Sender.Send(command);
            }
            else if (mode == "updated")
            {
                var command = new UpdateKewajaranTahapanTargetCommand(uuid, nama, nilai);
                result = await Sender.Send(command);
            }
            else
            {
                var command = new DeleteKewajaranTahapanTargetCommand(uuid);
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
        public async Task CreateUpdateDelete_ShouldBeExecute_WhenValid(
            object[] beforeData,
            object[]? afterData,
            string mode)
        {
            var namaBefore = (string)beforeData[0];
            var nilaiBefore = (int)beforeData[1];

            // --- CREATE ---
            var createCommand = new CreateKewajaranTahapanTargetCommand(namaBefore, nilaiBefore);
            var createResult = await Sender.Send(createCommand);

            Assert.True(createResult.IsSuccess);
            var dataCreate = DBContext.KewajaranTahapanTarget.FirstOrDefault(p => p.Uuid == createResult!.Value);

            Assert.NotNull(dataCreate);
            Assert.Equal(namaBefore, dataCreate.Nama);
            Assert.Equal(nilaiBefore, dataCreate.Nilai);

            using (var scope = Factory.Services.CreateScope())
            {
                var handler = scope.ServiceProvider
                    .GetService<IRequestHandler<CreateKewajaranTahapanTargetCommand, Result<Guid>>>();

                Assert.NotNull(handler);
                Assert.IsType<CreateKewajaranTahapanTargetCommandHandler>(handler);
            }

            var newUuid = createResult.Value.ToString();

            // --- UPDATE / DELETE ---
            if (mode == "updated")
            {
                var namaAfter = (string)afterData![0];
                var nilaiAfter = (int)afterData[1];

                var updateCommand = new UpdateKewajaranTahapanTargetCommand(newUuid, namaAfter, nilaiAfter);
                var updateResult = await Sender.Send(updateCommand);

                Assert.True(updateResult.IsSuccess);
                var dataUpdate = DBContext.KewajaranTahapanTarget.FirstOrDefault(p => p.Uuid == createResult!.Value);

                Assert.NotNull(dataUpdate);
                Assert.Equal(namaAfter, dataUpdate.Nama);
                Assert.Equal(nilaiAfter, dataUpdate.Nilai);

                using (var scope = Factory.Services.CreateScope())
                {
                    var handler = scope.ServiceProvider
                        .GetService<IRequestHandler<UpdateKewajaranTahapanTargetCommand, Result>>();

                    Assert.NotNull(handler);
                    Assert.IsType<UpdateKewajaranTahapanTargetCommandHandler>(handler);
                }
            }
            else if (mode == "deleted")
            {
                var deleteCommand = new DeleteKewajaranTahapanTargetCommand(newUuid);
                var deleteResult = await Sender.Send(deleteCommand);

                Assert.True(deleteResult.IsSuccess);

                using (var scope = Factory.Services.CreateScope())
                {
                    var handler = scope.ServiceProvider
                        .GetService<IRequestHandler<DeleteKewajaranTahapanTargetCommand, Result>>();

                    Assert.NotNull(handler);
                    Assert.IsType<DeleteKewajaranTahapanTargetCommandHandler>(handler);
                }
            }
        }
        [Fact]
        public async Task Create_ShouldThrow_WhenInvalidRuleDomain()
        {
            var nama = "tes";
            var nilai = int.MaxValue; // contoh melanggar aturan domain

            var command = new CreateKewajaranTahapanTargetCommand(nama, nilai);
            var result = await Sender.Send(command);

            Assert.True(result.IsFailure);
            Assert.Equal("KewajaranTahapanTarget.InvalidValueNilai", result.Error.Code);
        }

        [Fact]
        public async Task Update_ShouldThrow_WhenNotExist()
        {
            var guid = Guid.NewGuid().ToString();
            var nama = "tes";
            var nilai = 10;

            var command = new UpdateKewajaranTahapanTargetCommand(guid, nama, nilai);
            var result = await Sender.Send(command);

            Assert.True(result.IsFailure);
            Assert.Equal("KewajaranTahapanTarget.NotFound", result.Error.Code);
        }

        [Fact]
        public async Task Update_ShouldThrow_WhenInvalidRuleDomain()
        {
            // --- CREATE ---
            var namaBefore = "tes";
            var nilaiBefore = 10;

            var createCommand = new CreateKewajaranTahapanTargetCommand(namaBefore, nilaiBefore);
            var createResult = await Sender.Send(createCommand);

            Assert.True(createResult.IsSuccess);
            var dataCreate = DBContext.KewajaranTahapanTarget.FirstOrDefault(p => p.Uuid == createResult!.Value);

            Assert.NotNull(dataCreate);
            Assert.Equal(namaBefore, dataCreate.Nama);
            Assert.Equal(nilaiBefore, dataCreate.Nilai);

            var newUuid = createResult.Value.ToString();

            // --- UPDATE (melanggar aturan domain)
            var namaAfter = "tes2";
            var nilaiAfter = int.MaxValue;

            var updateCommand = new UpdateKewajaranTahapanTargetCommand(newUuid, namaAfter, nilaiAfter);
            var updateResult = await Sender.Send(updateCommand);

            Assert.True(updateResult.IsFailure);
            Assert.Equal("KewajaranTahapanTarget.InvalidValueNilai", updateResult.Error.Code);
        }

        [Fact]
        public async Task Delete_ShouldThrow_WhenNotExist()
        {
            var guid = Guid.NewGuid().ToString();

            var command = new DeleteKewajaranTahapanTargetCommand(guid);
            var result = await Sender.Send(command);

            Assert.True(result.IsFailure);
            Assert.Equal("KewajaranTahapanTarget.NotFound", result.Error.Code);
        }

        [Fact]
        public async Task GetAll_ReturnsList_WhenDataExists()
        {
            var mockFactory = new Mock<IDbConnectionFactory>();
            var mockConn = new Mock<DbConnection>();

            var fake = new List<KewajaranTahapanTargetResponse>
            {
                new() { Uuid = Guid.NewGuid().ToString(), Nama = "Data 1", Nilai = 10 }
            };

            mockConn.SetupDapperAsync(c =>
                c.QueryAsync<KewajaranTahapanTargetResponse>(It.IsAny<string>(), null, null, null, null)
            ).ReturnsAsync(fake);

            mockFactory.Setup(f => f.OpenConnectionAsync())
                .ReturnsAsync(mockConn.Object);

            var handler = new GetAllKewajaranTahapanTargetQueryHandler(mockFactory.Object);
            var result = await handler.Handle(new GetAllKewajaranTahapanTargetQuery(), CancellationToken.None);

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
                c.QueryAsync<KewajaranTahapanTargetResponse>(It.IsAny<string>(), null, null, null, null)
            ).ReturnsAsync(new List<KewajaranTahapanTargetResponse>());

            mockFactory.Setup(f => f.OpenConnectionAsync())
                .ReturnsAsync(mockConn.Object);

            var handler = new GetAllKewajaranTahapanTargetQueryHandler(mockFactory.Object);
            var result = await handler.Handle(new GetAllKewajaranTahapanTargetQuery(), CancellationToken.None);

            Assert.False(result.IsSuccess);
            Assert.Equal(KewajaranTahapanTargetErrors.EmptyData().Code, result.Error.Code);
        }

        [Fact]
        public async Task GetBobot_ReturnsSuccess_WhenSingleValueFound()
        {
            var mockFactory = new Mock<IDbConnectionFactory>();
            var mockConn = new Mock<DbConnection>();

            mockConn.SetupDapperAsync(c =>
                c.QueryAsync<int>(It.IsAny<string>(), null, null, null, null)
            ).ReturnsAsync(new List<int> { 100 });

            mockFactory.Setup(f => f.OpenConnectionAsync())
                .ReturnsAsync(mockConn.Object);

            var handler = new GetBobotKewajaranTahapanTargetQueryHandler(mockFactory.Object);
            var result = await handler.Handle(new GetBobotKewajaranTahapanTargetQuery(), CancellationToken.None);

            Assert.True(result.IsSuccess);
            Assert.Equal(100, result.Value);
        }

        [Fact]
        public async Task GetBobot_ReturnsFailure_WhenEmpty()
        {
            var mockFactory = new Mock<IDbConnectionFactory>();
            var mockConn = new Mock<DbConnection>();

            mockConn.SetupDapperAsync(c =>
                c.QueryAsync<int>(It.IsAny<string>(), null, null, null, null)
            ).ReturnsAsync(new List<int>());

            mockFactory.Setup(f => f.OpenConnectionAsync())
                .ReturnsAsync(mockConn.Object);

            var handler = new GetBobotKewajaranTahapanTargetQueryHandler(mockFactory.Object);
            var result = await handler.Handle(new GetBobotKewajaranTahapanTargetQuery(), CancellationToken.None);

            Assert.False(result.IsSuccess);
            Assert.Equal(KewajaranTahapanTargetErrors.EmptyData().Code, result.Error.Code);
        }

        [Fact]
        public async Task GetBobot_ReturnsFailure_WhenMultipleValues()
        {
            var mockFactory = new Mock<IDbConnectionFactory>();
            var mockConn = new Mock<DbConnection>();

            mockConn.SetupDapperAsync(c =>
                c.QueryAsync<int>(It.IsAny<string>(), null, null, null, null)
            ).ReturnsAsync(new List<int> { 10, 20 });

            mockFactory.Setup(f => f.OpenConnectionAsync())
                .ReturnsAsync(mockConn.Object);

            var handler = new GetBobotKewajaranTahapanTargetQueryHandler(mockFactory.Object);
            var result = await handler.Handle(new GetBobotKewajaranTahapanTargetQuery(), CancellationToken.None);

            Assert.False(result.IsSuccess);
            Assert.Equal(KewajaranTahapanTargetErrors.NotSameValue().Code, result.Error.Code);
        }

        [Fact]
        public async Task GetDefault_ReturnsSuccess_WhenFound()
        {
            var mockFactory = new Mock<IDbConnectionFactory>();
            var mockConn = new Mock<DbConnection>();

            var uuid = Guid.NewGuid();
            var fake = new KewajaranTahapanTargetDefaultResponse
            {
                Id = "1",
                Uuid = uuid.ToString(),
                Nama = "Default",
                Nilai = 10
            };

            mockConn.SetupDapperAsync(c =>
                c.QuerySingleOrDefaultAsync<KewajaranTahapanTargetDefaultResponse?>(It.IsAny<string>(), It.IsAny<object>(), null, null, null)
            ).ReturnsAsync(fake);

            mockFactory.Setup(f => f.OpenConnectionAsync())
                .ReturnsAsync(mockConn.Object);

            var handler = new GetKewajaranTahapanTargetDefaultQueryHandler(mockFactory.Object);
            var result = await handler.Handle(new GetKewajaranTahapanTargetDefaultQuery(uuid), CancellationToken.None);

            Assert.True(result.IsSuccess);
            Assert.Equal("Default", result.Value.Nama);
        }

        [Fact]
        public async Task GetDefault_ReturnsFailure_WhenNotFound()
        {
            var mockFactory = new Mock<IDbConnectionFactory>();
            var mockConn = new Mock<DbConnection>();

            mockConn.SetupDapperAsync(c =>
                c.QuerySingleOrDefaultAsync<KewajaranTahapanTargetDefaultResponse?>(It.IsAny<string>(), It.IsAny<object>(), null, null, null)
            ).ReturnsAsync((KewajaranTahapanTargetDefaultResponse?)null);

            mockFactory.Setup(f => f.OpenConnectionAsync())
                .ReturnsAsync(mockConn.Object);

            var uuid = Guid.NewGuid();
            var handler = new GetKewajaranTahapanTargetDefaultQueryHandler(mockFactory.Object);
            var result = await handler.Handle(new GetKewajaranTahapanTargetDefaultQuery(uuid), CancellationToken.None);

            Assert.False(result.IsSuccess);
            Assert.Equal(KewajaranTahapanTargetErrors.NotFound(uuid).Code, result.Error.Code);
        }

        [Fact]
        public async Task GetByUuid_ReturnsSuccess_WhenFound()
        {
            var mockFactory = new Mock<IDbConnectionFactory>();
            var mockConn = new Mock<DbConnection>();

            var uuid = Guid.NewGuid();
            var fake = new KewajaranTahapanTargetResponse
            {
                Uuid = uuid.ToString(),
                Nama = "Some Data",
                Nilai = 99
            };

            mockConn.SetupDapperAsync(c =>
                c.QuerySingleOrDefaultAsync<KewajaranTahapanTargetResponse?>(It.IsAny<string>(), It.IsAny<object>(), null, null, null)
            ).ReturnsAsync(fake);

            mockFactory.Setup(f => f.OpenConnectionAsync())
                .ReturnsAsync(mockConn.Object);

            var handler = new GetKewajaranTahapanTargetQueryHandler(mockFactory.Object);
            var result = await handler.Handle(new GetKewajaranTahapanTargetQuery(uuid), CancellationToken.None);

            Assert.True(result.IsSuccess);
            Assert.Equal("Some Data", result.Value.Nama);
        }

        [Fact]
        public async Task GetByUuid_ReturnsFailure_WhenNotFound()
        {
            var mockFactory = new Mock<IDbConnectionFactory>();
            var mockConn = new Mock<DbConnection>();

            mockConn.SetupDapperAsync(c =>
                c.QuerySingleOrDefaultAsync<KewajaranTahapanTargetResponse?>(It.IsAny<string>(), It.IsAny<object>(), null, null, null)
            ).ReturnsAsync((KewajaranTahapanTargetResponse?)null);

            mockFactory.Setup(f => f.OpenConnectionAsync())
                .ReturnsAsync(mockConn.Object);

            var uuid = Guid.NewGuid();
            var handler = new GetKewajaranTahapanTargetQueryHandler(mockFactory.Object);
            var result = await handler.Handle(new GetKewajaranTahapanTargetQuery(uuid), CancellationToken.None);

            Assert.False(result.IsSuccess);
            Assert.Equal(KewajaranTahapanTargetErrors.NotFound(uuid).Code, result.Error.Code);
        }
    }
}
