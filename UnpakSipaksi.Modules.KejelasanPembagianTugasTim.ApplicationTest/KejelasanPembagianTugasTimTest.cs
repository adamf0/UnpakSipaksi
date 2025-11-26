using Dapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Moq.Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KejelasanPembagianTugasTim.Application.CreateKejelasanPembagianTugasTim;
using UnpakSipaksi.Modules.KejelasanPembagianTugasTim.Application.DeleteKejelasanPembagianTugasTim;
using UnpakSipaksi.Modules.KejelasanPembagianTugasTim.Application.GetAllKejelasanPembagianTugasTim;
using UnpakSipaksi.Modules.KejelasanPembagianTugasTim.Application.GetBobotKejelasanPembagianTugasTim;
using UnpakSipaksi.Modules.KejelasanPembagianTugasTim.Application.GetKejelasanPembagianTugasTim;
using UnpakSipaksi.Modules.KejelasanPembagianTugasTim.Application.UpdateKejelasanPembagianTugasTim;
using Xunit;

namespace UnpakSipaksi.Modules.KejelasanPembagianTugasTim.ApplicationTest
{
    public class KejelasanPembagianTugasTimTest : BaseIntegrationTest
    {
        public KejelasanPembagianTugasTimTest(IntegrationTestWebAppFactory factory) : base(factory)
        {
        }

        public static IEnumerable<object[]> InvalidData()
        {
            var valid = Guid.NewGuid().ToString();
            var empty = "";

            // CREATE
            yield return new object[] { empty, "", 10, "'Nama' tidak boleh kosong.", "created" };
            yield return new object[] { empty, "tes", -1, "'Skor' tidak boleh negative.", "created" };

            // UPDATE
            yield return new object[] { valid, "", 10, "'Nama' tidak boleh kosong.", "updated" };
            yield return new object[] { valid, "tes", -1, "'Skor' tidak boleh negative.", "updated" };
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
            int skor,
            string message,
            string mode)
        {
            // Act
            Result? result = null;
            if (mode == "created")
            {
                var command = new CreateKejelasanPembagianTugasTimCommand(nama, skor);
                result = await Sender.Send(command);
            }
            else if (mode == "updated")
            {
                var command = new UpdateKejelasanPembagianTugasTimCommand(uuid, nama, skor);
                result = await Sender.Send(command);
            }
            else
            {
                var command = new DeleteKejelasanPembagianTugasTimCommand(uuid);
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
            var skorBefore = (int)beforeData[1];

            var createCommand = new CreateKejelasanPembagianTugasTimCommand(namaBefore, skorBefore);
            var createResult = await Sender.Send(createCommand);

            Assert.True(createResult.IsSuccess);
            var dataCreate = DBContext.KejelasanPembagianTugasTim.FirstOrDefault(p => p.Uuid == createResult!.Value);

            Assert.NotNull(dataCreate);
            Assert.Equal(namaBefore, dataCreate.Nama);
            Assert.Equal(skorBefore, dataCreate.Skor);

            using (var scope = Factory.Services.CreateScope())
            {
                var handler = scope.ServiceProvider
                    .GetService<IRequestHandler<CreateKejelasanPembagianTugasTimCommand, Result<Guid>>>();

                Assert.NotNull(handler);
                Assert.IsType<CreateKejelasanPembagianTugasTimCommandHandler>(handler);
            }

            var newUuid = createResult.Value.ToString();

            // --- UPDATE / DELETE ---
            if (mode == "updated")
            {
                var namaAfter = (string)afterData![0];
                var skorAfter = (int)afterData[1];

                var updateCommand = new UpdateKejelasanPembagianTugasTimCommand(newUuid, namaAfter, skorAfter);
                var updateResult = await Sender.Send(updateCommand);

                Assert.True(updateResult.IsSuccess);
                var dataUpdate = DBContext.KejelasanPembagianTugasTim.FirstOrDefault(p => p.Uuid == createResult!.Value);

                Assert.NotNull(dataUpdate);
                Assert.Equal(namaAfter, dataUpdate.Nama);
                Assert.Equal(skorAfter, dataUpdate.Skor);

                using (var scope = Factory.Services.CreateScope())
                {
                    var handler = scope.ServiceProvider
                        .GetService<IRequestHandler<UpdateKejelasanPembagianTugasTimCommand, Result>>();

                    Assert.NotNull(handler);
                    Assert.IsType<UpdateKejelasanPembagianTugasTimCommandHandler>(handler);
                }
            }
            else if (mode == "deleted")
            {
                var deleteCommand = new DeleteKejelasanPembagianTugasTimCommand(newUuid);
                var deleteResult = await Sender.Send(deleteCommand);

                Assert.True(deleteResult.IsSuccess);

                using (var scope = Factory.Services.CreateScope())
                {
                    var handler = scope.ServiceProvider
                        .GetService<IRequestHandler<DeleteKejelasanPembagianTugasTimCommand, Result>>();

                    Assert.NotNull(handler);
                    Assert.IsType<DeleteKejelasanPembagianTugasTimCommandHandler>(handler);
                }
            }
        }

        [Fact]
        public async Task Create_ShouldThrow_WhenInvalidRuleDomain()
        {
            var nama = "tes";
            var skor = int.MaxValue; // contoh melanggar aturan domain

            var command = new CreateKejelasanPembagianTugasTimCommand(nama, skor);
            var result = await Sender.Send(command);

            Assert.True(result.IsFailure);
            Assert.Equal("KejelasanPembagianTugasTim.InvalidValueSkor", result.Error.Code);
        }

        [Fact]
        public async Task Update_ShouldThrow_WhenNotExist()
        {
            var guid = Guid.NewGuid().ToString();
            var nama = "tes";
            var skor = 10;

            var command = new UpdateKejelasanPembagianTugasTimCommand(guid, nama, skor);
            var result = await Sender.Send(command);

            Assert.True(result.IsFailure);
            Assert.Equal("KejelasanPembagianTugasTim.NotFound", result.Error.Code);
        }

        [Fact]
        public async Task Update_ShouldThrow_WhenInvalidRuleDomain()
        {
            // --- CREATE ---
            var namaBefore = "tes";
            var skorBefore = 10;

            var createCommand = new CreateKejelasanPembagianTugasTimCommand(namaBefore, skorBefore);
            var createResult = await Sender.Send(createCommand);

            Assert.True(createResult.IsSuccess);
            var dataCreate = DBContext.KejelasanPembagianTugasTim.FirstOrDefault(p => p.Uuid == createResult!.Value);

            Assert.NotNull(dataCreate);
            Assert.Equal(namaBefore, dataCreate.Nama);
            Assert.Equal(skorBefore, dataCreate.Skor);

            var newUuid = createResult.Value.ToString();

            // --- UPDATE (melanggar aturan domain)
            var namaAfter = "tes2";
            var skorAfter = int.MaxValue;

            var updateCommand = new UpdateKejelasanPembagianTugasTimCommand(newUuid, namaAfter, skorAfter);
            var updateResult = await Sender.Send(updateCommand);

            Assert.True(updateResult.IsFailure);
            Assert.Equal("KejelasanPembagianTugasTim.InvalidValueSkor", updateResult.Error.Code);
        }

        [Fact]
        public async Task Delete_ShouldThrow_WhenNotExist()
        {
            var guid = Guid.NewGuid().ToString();

            var command = new DeleteKejelasanPembagianTugasTimCommand(guid);
            var result = await Sender.Send(command);

            Assert.True(result.IsFailure);
            Assert.Equal("KejelasanPembagianTugasTim.NotFound", result.Error.Code);
        }

        [Fact]
        public async Task Handle_All_ReturnsSuccess_WhenDataExists()
        {
            var mockConnectionFactory = new Mock<IDbConnectionFactory>();
            var mockConnection = new Mock<DbConnection>();

            var fakeData = new List<KejelasanPembagianTugasTimResponse>
            {
                new() { Uuid = Guid.NewGuid().ToString(), Nama = "Tim 1", Skor = "5" }
            };

            mockConnection.SetupDapperAsync(c =>
                c.QueryAsync<KejelasanPembagianTugasTimResponse>(
                    It.IsAny<string>(), null, null, null, null
                )
            ).ReturnsAsync(fakeData);

            mockConnectionFactory
                .Setup(f => f.OpenConnectionAsync())
                .Returns(new ValueTask<DbConnection>(mockConnection.Object));

            var handler = new GetAllKejelasanPembagianTugasTimQueryHandler(mockConnectionFactory.Object);

            var result = await handler.Handle(
                new GetAllKejelasanPembagianTugasTimQuery(),
                CancellationToken.None
            );

            Assert.True(result.IsSuccess);
            Assert.NotEmpty(result.Value);
        }

        [Fact]
        public async Task Handle_All_ReturnsFailure_WhenEmpty()
        {
            var mockConnectionFactory = new Mock<IDbConnectionFactory>();
            var mockConnection = new Mock<DbConnection>();

            mockConnection.SetupDapperAsync(c =>
                c.QueryAsync<KejelasanPembagianTugasTimResponse>(
                    It.IsAny<string>(), null, null, null, null
                )
            ).ReturnsAsync(new List<KejelasanPembagianTugasTimResponse>());

            mockConnectionFactory
                .Setup(f => f.OpenConnectionAsync())
                .Returns(new ValueTask<DbConnection>(mockConnection.Object));

            var handler = new GetAllKejelasanPembagianTugasTimQueryHandler(mockConnectionFactory.Object);

            var result = await handler.Handle(
                new GetAllKejelasanPembagianTugasTimQuery(),
                CancellationToken.None
            );

            Assert.False(result.IsSuccess);
        }

        [Fact]
        public async Task Handle_Default_ReturnsSuccess_WhenFound()
        {
            var mockConnectionFactory = new Mock<IDbConnectionFactory>();
            var mockConnection = new Mock<DbConnection>();
            var uuid = Guid.NewGuid();

            var fakeData = new KejelasanPembagianTugasTimDefaultResponse
            {
                Id = "1",
                Uuid = uuid.ToString(),
                Nama = "Default Tim",
                BobotPDP = 1,
                BobotTerapan = 2,
                BobotKerjasama = 3,
                BobotPenelitianDasar = 4,
                Skor = 10
            };

            mockConnection.SetupDapperAsync(c =>
                c.QuerySingleOrDefaultAsync<KejelasanPembagianTugasTimDefaultResponse>(
                    It.IsAny<string>(), It.IsAny<object>(), null, null, null
                )
            ).ReturnsAsync(fakeData);

            mockConnectionFactory
                .Setup(f => f.OpenConnectionAsync())
                .Returns(new ValueTask<DbConnection>(mockConnection.Object));

            var handler = new GetKejelasanPembagianTugasTimDefaultQueryHandler(mockConnectionFactory.Object);

            var result = await handler.Handle(
                new GetKejelasanPembagianTugasTimDefaultQuery(uuid),
                CancellationToken.None
            );

            Assert.True(result.IsSuccess);
            Assert.Equal(fakeData.Nama, result.Value.Nama);
        }

        [Fact]
        public async Task Handle_Default_ReturnsFailure_WhenNotFound()
        {
            var mockConnectionFactory = new Mock<IDbConnectionFactory>();
            var mockConnection = new Mock<DbConnection>();

            mockConnection.SetupDapperAsync(c =>
                c.QuerySingleOrDefaultAsync<KejelasanPembagianTugasTimDefaultResponse>(
                    It.IsAny<string>(), It.IsAny<object>(), null, null, null
                )
            ).ReturnsAsync((KejelasanPembagianTugasTimDefaultResponse?)null);

            mockConnectionFactory
                .Setup(f => f.OpenConnectionAsync())
                .Returns(new ValueTask<DbConnection>(mockConnection.Object));

            var handler = new GetKejelasanPembagianTugasTimDefaultQueryHandler(mockConnectionFactory.Object);

            var result = await handler.Handle(
                new GetKejelasanPembagianTugasTimDefaultQuery(Guid.NewGuid()),
                CancellationToken.None
            );

            Assert.False(result.IsSuccess);
        }

        [Fact]
        public async Task Handle_ReturnsSuccess_WhenFound()
        {
            var mockConnectionFactory = new Mock<IDbConnectionFactory>();
            var mockConnection = new Mock<DbConnection>();
            var uuid = Guid.NewGuid();

            var fakeData = new KejelasanPembagianTugasTimResponse
            {
                Uuid = uuid.ToString(),
                Nama = "Tim 1",
                Skor = "9"
            };

            mockConnection.SetupDapperAsync(c =>
                c.QuerySingleOrDefaultAsync<KejelasanPembagianTugasTimResponse>(
                    It.IsAny<string>(), It.IsAny<object>(), null, null, null
                )
            ).ReturnsAsync(fakeData);

            mockConnectionFactory
                .Setup(f => f.OpenConnectionAsync())
                .Returns(new ValueTask<DbConnection>(mockConnection.Object));

            var handler = new GetKejelasanPembagianTugasTimQueryHandler(mockConnectionFactory.Object);

            var result = await handler.Handle(
                new GetKejelasanPembagianTugasTimQuery(uuid),
                CancellationToken.None
            );

            Assert.True(result.IsSuccess);
            Assert.Equal(fakeData.Nama, result.Value.Nama);
        }

        [Fact]
        public async Task Handle_ReturnsFailure_WhenNotFound()
        {
            var mockConnectionFactory = new Mock<IDbConnectionFactory>();
            var mockConnection = new Mock<DbConnection>();

            mockConnection.SetupDapperAsync(c =>
                c.QuerySingleOrDefaultAsync<KejelasanPembagianTugasTimResponse>(
                    It.IsAny<string>(), It.IsAny<object>(), null, null, null
                )
            ).ReturnsAsync((KejelasanPembagianTugasTimResponse?)null);

            mockConnectionFactory
                .Setup(f => f.OpenConnectionAsync())
                .Returns(new ValueTask<DbConnection>(mockConnection.Object));

            var handler = new GetKejelasanPembagianTugasTimQueryHandler(mockConnectionFactory.Object);

            var result = await handler.Handle(
                new GetKejelasanPembagianTugasTimQuery(Guid.NewGuid()),
                CancellationToken.None
            );

            Assert.False(result.IsSuccess);
        }

        [Theory]
        [InlineData("Penelitian Dasar", 10)]
        [InlineData("Penelitian Terapan", 20)]
        [InlineData("Penelitian Kolaborasi", 30)]
        [InlineData("Penelitian Dosen Pemula (PDP)", 40)]
        public async Task Handle_ReturnsSuccess_WhenSingleValueExists(string kategori, int expected)
        {
            var mockConnectionFactory = new Mock<IDbConnectionFactory>();
            var mockConnection = new Mock<DbConnection>();

            var fakeData = new List<int> { expected };

            mockConnection.SetupDapperAsync(c =>
                c.QueryAsync<int>(It.IsAny<string>(), null, null, null, null)
            ).ReturnsAsync(fakeData);

            mockConnectionFactory
                .Setup(f => f.OpenConnectionAsync())
                .Returns(new ValueTask<DbConnection>(mockConnection.Object));

            var handler = new GetBobotKejelasanPembagianTugasTimQueryHandler(mockConnectionFactory.Object);

            var result = await handler.Handle(
                new GetBobotKejelasanPembagianTugasTimQuery(kategori),
                CancellationToken.None
            );

            Assert.True(result.IsSuccess);
            Assert.Equal(expected, result.Value);
        }

        [Fact]
        public async Task Handle_ReturnsFailure_WhenKategoriUnknown()
        {
            var handler = new GetBobotKejelasanPembagianTugasTimQueryHandler(new Mock<IDbConnectionFactory>().Object);

            var result = await handler.Handle(
                new GetBobotKejelasanPembagianTugasTimQuery("Kategori Tidak Ada"),
                CancellationToken.None
            );

            Assert.False(result.IsSuccess);
        }

        [Fact]
        public async Task Handle_ReturnsFailure_WhenEmptyData()
        {
            var mockConnectionFactory = new Mock<IDbConnectionFactory>();
            var mockConnection = new Mock<DbConnection>();

            mockConnection.SetupDapperAsync(c =>
                c.QueryAsync<int>(It.IsAny<string>(), null, null, null, null)
            ).ReturnsAsync(new List<int>());

            mockConnectionFactory
                .Setup(f => f.OpenConnectionAsync())
                .Returns(new ValueTask<DbConnection>(mockConnection.Object));

            var handler = new GetBobotKejelasanPembagianTugasTimQueryHandler(mockConnectionFactory.Object);

            var result = await handler.Handle(
                new GetBobotKejelasanPembagianTugasTimQuery("Penelitian Dasar"),
                CancellationToken.None
            );

            Assert.False(result.IsSuccess);
        }

        [Fact]
        public async Task Handle_ReturnsFailure_WhenMultipleValues()
        {
            var mockConnectionFactory = new Mock<IDbConnectionFactory>();
            var mockConnection = new Mock<DbConnection>();

            var fakeData = new List<int> { 10, 20 };

            mockConnection.SetupDapperAsync(c =>
                c.QueryAsync<int>(It.IsAny<string>(), null, null, null, null)
            ).ReturnsAsync(fakeData);

            mockConnectionFactory
                .Setup(f => f.OpenConnectionAsync())
                .Returns(new ValueTask<DbConnection>(mockConnection.Object));

            var handler = new GetBobotKejelasanPembagianTugasTimQueryHandler(mockConnectionFactory.Object);

            var result = await handler.Handle(
                new GetBobotKejelasanPembagianTugasTimQuery("Penelitian Dasar"),
                CancellationToken.None
            );

            Assert.False(result.IsSuccess);
        }
    }
}
