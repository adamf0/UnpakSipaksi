using Dapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Moq.Dapper;
using System.Data;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KebaruanReferensi.Application.CreateKebaruanReferensi;
using UnpakSipaksi.Modules.KebaruanReferensi.Application.DeleteKebaruanReferensi;
using UnpakSipaksi.Modules.KebaruanReferensi.Application.GetAllKebaruanReferensi;
using UnpakSipaksi.Modules.KebaruanReferensi.Application.GetBobotKebaruanReferensi;
using UnpakSipaksi.Modules.KebaruanReferensi.Application.GetKebaruanReferensi;
using UnpakSipaksi.Modules.KebaruanReferensi.Application.UpdateKebaruanReferensi;
using Xunit;

namespace UnpakSipaksi.Modules.KebaruanReferensi.ApplicationTest
{
    public class KebaruanReferensiTest : BaseIntegrationTest 
    {
        public KebaruanReferensiTest(IntegrationTestWebAppFactory factory) : base(factory)
        {
        }

        public static IEnumerable<object[]> InvalidData()
        {
            var valid = Guid.NewGuid().ToString();
            var empty = "";

            // CREATE
            yield return new object[] { empty, "", 10, "'Nama' tidak boleh kosong.", "created" };
            yield return new object[] { empty, "tes", -10, "'Skor' tidak boleh negative.", "created" };

            // UPDATE
            yield return new object[] { valid, "", 10, "'Nama' tidak boleh kosong.", "updated" };
            yield return new object[] { valid, "tes", -10, "'Skor' tidak boleh negative.", "updated" };
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
                var command = new CreateKebaruanReferensiCommand(nama, skor);
                result = await Sender.Send(command);
            }
            else if (mode == "updated")
            {
                var command = new UpdateKebaruanReferensiCommand(uuid, nama, skor);
                result = await Sender.Send(command);
            }
            else
            {
                var command = new DeleteKebaruanReferensiCommand(uuid);
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

            var createCommand = new CreateKebaruanReferensiCommand(namaBefore, skorBefore);
            var createResult = await Sender.Send(createCommand);

            Assert.True(createResult.IsSuccess);
            var dataCreate = DBContext.KebaruanReferensi.FirstOrDefault(p => p.Uuid == createResult!.Value);

            Assert.NotNull(dataCreate);
            Assert.Equal(namaBefore, dataCreate.Nama);
            Assert.Equal(skorBefore, dataCreate.Skor);

            using (var scope = Factory.Services.CreateScope())
            {
                var handler = scope.ServiceProvider
                    .GetService<IRequestHandler<CreateKebaruanReferensiCommand, Result<Guid>>>();

                Assert.NotNull(handler);
                Assert.IsType<CreateKebaruanReferensiCommandHandler>(handler);
            }

            var newUuid = createResult.Value.ToString();

            // --- UPDATE / DELETE ---
            if (mode == "updated")
            {
                var namaAfter = (string)afterData![0];
                var skorAfter = (int)afterData[1];

                var updateCommand = new UpdateKebaruanReferensiCommand(newUuid, namaAfter, skorAfter);
                var updateResult = await Sender.Send(updateCommand);

                Assert.True(updateResult.IsSuccess);
                var dataUpdate = DBContext.KebaruanReferensi.FirstOrDefault(p => p.Uuid == createResult!.Value);

                Assert.NotNull(dataUpdate);
                Assert.Equal(namaAfter, dataUpdate.Nama);
                Assert.Equal(skorAfter, dataUpdate.Skor);

                using (var scope = Factory.Services.CreateScope())
                {
                    var handler = scope.ServiceProvider
                        .GetService<IRequestHandler<UpdateKebaruanReferensiCommand, Result>>();

                    Assert.NotNull(handler);
                    Assert.IsType<UpdateKebaruanReferensiCommandHandler>(handler);
                }
            }
            else if (mode == "deleted")
            {
                var deleteCommand = new DeleteKebaruanReferensiCommand(newUuid);
                var deleteResult = await Sender.Send(deleteCommand);

                Assert.True(deleteResult.IsSuccess);

                using (var scope = Factory.Services.CreateScope())
                {
                    var handler = scope.ServiceProvider
                        .GetService<IRequestHandler<DeleteKebaruanReferensiCommand, Result>>();

                    Assert.NotNull(handler);
                    Assert.IsType<DeleteKebaruanReferensiCommandHandler>(handler);
                }
            }
        }

        [Fact]
        public async Task Update_ShouldThrow_WhenNotExist()
        {
            var guid = Guid.NewGuid().ToString();
            var nama = "tes";
            var skor = 10;

            var command = new UpdateKebaruanReferensiCommand(guid, nama, skor);
            var result = await Sender.Send(command);

            Assert.True(result.IsFailure);
            Assert.Equal("KebaruanReferensi.NotFound", result.Error.Code);
        }

        [Fact]
        public async Task Update_ShouldThrow_WhenInvalidRuleDomain()
        {
            // --- CREATE ---
            var namaBefore = "tes";
            var skorBefore = 10;

            var createCommand = new CreateKebaruanReferensiCommand(namaBefore, skorBefore);
            var createResult = await Sender.Send(createCommand);

            Assert.True(createResult.IsSuccess);
            var dataCreate = DBContext.KebaruanReferensi.FirstOrDefault(p => p.Uuid == createResult!.Value);

            Assert.NotNull(dataCreate);
            Assert.Equal(namaBefore, dataCreate.Nama);
            Assert.Equal(skorBefore, dataCreate.Skor);

            var newUuid = createResult.Value.ToString();

            // --- UPDATE (melanggar aturan domain)
            var namaAfter = "tes2";
            var skorAfter = int.MaxValue;

            var updateCommand = new UpdateKebaruanReferensiCommand(newUuid, namaAfter, skorAfter);
            var updateResult = await Sender.Send(updateCommand);

            Assert.True(updateResult.IsFailure);
            Assert.Equal("KebaruanReferensi.InvalidValueSkor", updateResult.Error.Code);
        }

        [Fact]
        public async Task Delete_ShouldThrow_WhenNotExist()
        {
            var guid = Guid.NewGuid().ToString();

            var command = new DeleteKebaruanReferensiCommand(guid);
            var result = await Sender.Send(command);

            Assert.True(result.IsFailure);
            Assert.Equal("KebaruanReferensi.NotFound", result.Error.Code);
        }

        [Fact]
        public async Task Handle_All_ReturnsSuccess_WhenDataExists()
        {
            var mockFactory = new Mock<IDbConnectionFactory>();
            var mockConn = new Mock<DbConnection>();

            var fakeList = new List<KebaruanReferensiResponse>
            {
                new() { Uuid = Guid.NewGuid().ToString(), Nama = "Ref 1", Skor = "10" }
            };

            mockConn.SetupDapperAsync(c =>
                c.QueryAsync<KebaruanReferensiResponse>(
                    It.IsAny<string>(),
                    null,
                    null,
                    null,
                    null
                )
            ).ReturnsAsync(fakeList);

            mockFactory.Setup(f => f.OpenConnectionAsync())
                .ReturnsAsync(mockConn.Object);

            var handler = new GetAllKebaruanReferensiQueryHandler(mockFactory.Object);
            var query = new GetAllKebaruanReferensiQuery();

            var result = await handler.Handle(query, CancellationToken.None);

            Assert.True(result.IsSuccess);
            Assert.NotEmpty(result.Value);
            Assert.Equal(fakeList[0].Nama, result.Value[0].Nama);
        }

        [Fact]
        public async Task Handle_All_ReturnsFailure_WhenNoData()
        {
            var mockFactory = new Mock<IDbConnectionFactory>();
            var mockConn = new Mock<DbConnection>();

            mockConn.SetupDapperAsync(c =>
                c.QueryAsync<KebaruanReferensiResponse>(
                    It.IsAny<string>(),
                    null, null, null, null
                )
            ).ReturnsAsync(new List<KebaruanReferensiResponse>());

            mockFactory.Setup(f => f.OpenConnectionAsync())
                .ReturnsAsync(mockConn.Object);

            var handler = new GetAllKebaruanReferensiQueryHandler(mockFactory.Object);
            var query = new GetAllKebaruanReferensiQuery();

            var result = await handler.Handle(query, CancellationToken.None);

            Assert.False(result.IsSuccess);
        }

        [Fact]
        public async Task Handle_Default_ReturnsSuccess_WhenDataExists()
        {
            var mockFactory = new Mock<IDbConnectionFactory>();
            var mockConn = new Mock<DbConnection>();
            var uuid = Guid.NewGuid().ToString();

            var fakeData = new KebaruanReferensiDefaultResponse
            {
                Id = "1",
                Uuid = uuid,
                Nama = "Ref Default",
                BobotPDP = 1,
                BobotTerapan = 2,
                BobotKerjasama = 3,
                BobotPenelitianDasar = 4,
                Skor = 10
            };

            mockConn.SetupDapperAsync(c =>
                c.QuerySingleOrDefaultAsync<KebaruanReferensiDefaultResponse>(
                    It.IsAny<string>(),
                    It.IsAny<object>(),
                    null, null, null
                )
            ).ReturnsAsync(fakeData);

            mockFactory.Setup(f => f.OpenConnectionAsync())
                .ReturnsAsync(mockConn.Object);

            var handler = new GetKebaruanReferensiDefaultQueryHandler(mockFactory.Object);
            var query = new GetKebaruanReferensiDefaultQuery(Guid.Parse(uuid));

            var result = await handler.Handle(query, CancellationToken.None);

            Assert.True(result.IsSuccess);
            Assert.Equal(fakeData.Nama, result.Value.Nama);
        }

        [Fact]
        public async Task Handle_Default_ReturnsFailure_WhenNotFound()
        {
            var mockFactory = new Mock<IDbConnectionFactory>();
            var mockConn = new Mock<DbConnection>();

            mockConn.SetupDapperAsync(c =>
                c.QuerySingleOrDefaultAsync<KebaruanReferensiDefaultResponse>(
                    It.IsAny<string>(),
                    It.IsAny<object>(),
                    null, null, null
                )
            ).ReturnsAsync((KebaruanReferensiDefaultResponse?)null);

            mockFactory.Setup(f => f.OpenConnectionAsync())
                .ReturnsAsync(mockConn.Object);

            var handler = new GetKebaruanReferensiDefaultQueryHandler(mockFactory.Object);

            var result = await handler.Handle(
                new GetKebaruanReferensiDefaultQuery(Guid.NewGuid()),
                CancellationToken.None
            );

            Assert.False(result.IsSuccess);
        }

        [Fact]
        public async Task Handle_ReturnsSuccess_WhenDataExists()
        {
            var mockFactory = new Mock<IDbConnectionFactory>();
            var mockConn = new Mock<DbConnection>();
            var uuid = Guid.NewGuid().ToString();

            var fakeData = new KebaruanReferensiResponse
            {
                Uuid = uuid,
                Nama = "Ref 1",
                Skor = "5"
            };

            mockConn.SetupDapperAsync(c =>
                c.QuerySingleOrDefaultAsync<KebaruanReferensiResponse>(
                    It.IsAny<string>(),
                    It.IsAny<object>(),
                    null, null, null
                )
            ).ReturnsAsync(fakeData);

            mockFactory.Setup(f => f.OpenConnectionAsync())
                .ReturnsAsync(mockConn.Object);

            var handler = new GetKebaruanReferensiQueryHandler(mockFactory.Object);
            var query = new GetKebaruanReferensiQuery(Guid.Parse(uuid));

            var result = await handler.Handle(query, CancellationToken.None);

            Assert.True(result.IsSuccess);
            Assert.Equal(fakeData.Nama, result.Value.Nama);
        }

        [Fact]
        public async Task Handle_ReturnsFailure_WhenNotFound()
        {
            var mockFactory = new Mock<IDbConnectionFactory>();
            var mockConn = new Mock<DbConnection>();

            mockConn.SetupDapperAsync(c =>
                c.QuerySingleOrDefaultAsync<KebaruanReferensiResponse>(
                    It.IsAny<string>(),
                    It.IsAny<object>(),
                    null, null, null
                )
            ).ReturnsAsync((KebaruanReferensiResponse?)null);

            mockFactory.Setup(f => f.OpenConnectionAsync())
                .ReturnsAsync(mockConn.Object);

            var handler = new GetKebaruanReferensiQueryHandler(mockFactory.Object);

            var result = await handler.Handle(
                new GetKebaruanReferensiQuery(Guid.NewGuid()),
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
            var mockFactory = new Mock<IDbConnectionFactory>();
            var mockConn = new Mock<DbConnection>();

            mockConn.SetupDapperAsync(c =>
                c.QueryAsync<int>(
                    It.IsAny<string>(),
                    null, null, null, null
                )
            ).ReturnsAsync(new List<int> { expected });

            mockFactory.Setup(f => f.OpenConnectionAsync())
                .ReturnsAsync(mockConn.Object);

            var handler = new GetBobotKebaruanReferensiQueryHandler(mockFactory.Object);
            var query = new GetBobotKebaruanReferensiQuery(kategori);

            var result = await handler.Handle(query, CancellationToken.None);

            Assert.True(result.IsSuccess);
            Assert.Equal(expected, result.Value);
        }

        [Fact]
        public async Task Handle_ReturnsFailure_WhenEmptyData()
        {
            var mockFactory = new Mock<IDbConnectionFactory>();
            var mockConn = new Mock<DbConnection>();

            mockConn.SetupDapperAsync(c =>
                c.QueryAsync<int>(It.IsAny<string>(), null, null, null, null)
            ).ReturnsAsync(new List<int>());

            mockFactory.Setup(f => f.OpenConnectionAsync())
                .ReturnsAsync(mockConn.Object);

            var handler = new GetBobotKebaruanReferensiQueryHandler(mockFactory.Object);

            var result = await handler.Handle(
                new GetBobotKebaruanReferensiQuery("Penelitian Dasar"),
                CancellationToken.None
            );

            Assert.False(result.IsSuccess);
        }

        [Fact]
        public async Task Handle_ReturnsFailure_WhenMultipleValues()
        {
            var mockFactory = new Mock<IDbConnectionFactory>();
            var mockConn = new Mock<DbConnection>();

            mockConn.SetupDapperAsync(c =>
                c.QueryAsync<int>(
                    It.IsAny<string>(),
                    null, null, null, null
                )
            ).ReturnsAsync(new List<int> { 10, 20 });

            mockFactory.Setup(f => f.OpenConnectionAsync())
                .ReturnsAsync(mockConn.Object);

            var handler = new GetBobotKebaruanReferensiQueryHandler(mockFactory.Object);

            var result = await handler.Handle(
                new GetBobotKebaruanReferensiQuery("Penelitian Dasar"),
                CancellationToken.None
            );

            Assert.False(result.IsSuccess);
        }

        [Fact]
        public async Task Handle_ReturnsFailure_WhenUnknownKategori()
        {
            var mockFactory = new Mock<IDbConnectionFactory>();
            var handler = new GetBobotKebaruanReferensiQueryHandler(mockFactory.Object);

            var result = await handler.Handle(
                new GetBobotKebaruanReferensiQuery("Kategori Tidak Valid"),
                CancellationToken.None
            );

            Assert.False(result.IsSuccess);
        }
    }
}
