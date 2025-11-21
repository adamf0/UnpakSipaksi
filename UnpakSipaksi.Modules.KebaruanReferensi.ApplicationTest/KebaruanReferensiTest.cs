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
        public async Task GetAllKebaruanReferensi_ShouldReturnSuccess_WhenDataExists()
        {
            var factory = new Mock<IDbConnectionFactory>();
            var connection = new Mock<DbConnection>();

            var fakeData = new List<KebaruanReferensiResponse>
            {
                new() { Uuid = Guid.NewGuid().ToString(), Nama = "Referensi A", Skor = "5" }
            };

            connection.SetupDapperAsync(c =>
                c.QueryAsync<KebaruanReferensiResponse>(
                    It.IsAny<string>(),
                    It.IsAny<object>(),
                    null, null, null
                )
            ).ReturnsAsync(fakeData);

            var handler = new GetAllKebaruanReferensiQueryHandler(factory.Object);

            var result = await handler.Handle(new GetAllKebaruanReferensiQuery(), CancellationToken.None);

            Assert.True(result.IsSuccess);
            Assert.Single(result.Value);
        }

        [Fact]
        public async Task GetAllKebaruanReferensi_ShouldReturnFailure_WhenDataIsEmpty()
        {
            var factory = new Mock<IDbConnectionFactory>();
            var connection = new Mock<DbConnection>();

            connection.SetupDapperAsync(c =>
                c.QueryAsync<KebaruanReferensiResponse>(
                    It.IsAny<string>(),
                    It.IsAny<object>(),
                    null, null, null
                )
            ).ReturnsAsync(new List<KebaruanReferensiResponse>());

            var handler = new GetAllKebaruanReferensiQueryHandler(factory.Object);

            var result = await handler.Handle(new GetAllKebaruanReferensiQuery(), CancellationToken.None);

            Assert.True(result.IsFailure);
        }


        [Fact]
        public async Task GetKebaruanReferensiDefault_ShouldReturnSuccess_WhenRecordExists()
        {
            var factory = new Mock<IDbConnectionFactory>();
            var connection = new Mock<DbConnection>();
            var uuid = Guid.NewGuid();

            var fakeData = new KebaruanReferensiDefaultResponse
            {
                Id = "1",
                Uuid = uuid.ToString(),
                Nama = "Referensi",
                Skor = 10
            };

            connection.SetupDapperAsync(c =>
                c.QuerySingleOrDefaultAsync<KebaruanReferensiDefaultResponse?>(
                    It.IsAny<string>(),
                    It.IsAny<object>(),
                    null, null, null
                )
            ).ReturnsAsync(fakeData);

            var handler = new GetKebaruanReferensiDefaultQueryHandler(factory.Object);

            var result = await handler.Handle(new GetKebaruanReferensiDefaultQuery(uuid), CancellationToken.None);

            Assert.True(result.IsSuccess);
            Assert.Equal(uuid.ToString(), result.Value.Uuid);
        }

        [Fact]
        public async Task GetKebaruanReferensiDefault_ShouldReturnFailure_WhenRecordNotFound()
        {
            var factory = new Mock<IDbConnectionFactory>();
            var connection = new Mock<DbConnection>();

            connection.SetupDapperAsync(c =>
                c.QuerySingleOrDefaultAsync<KebaruanReferensiDefaultResponse?>(
                    It.IsAny<string>(),
                    It.IsAny<object>(),
                    null, null, null
                )
            ).ReturnsAsync((KebaruanReferensiDefaultResponse)null);

            var handler = new GetKebaruanReferensiDefaultQueryHandler(factory.Object);

            var result = await handler.Handle(new GetKebaruanReferensiDefaultQuery(Guid.NewGuid()), CancellationToken.None);

            Assert.True(result.IsFailure);
        }


        //[Fact]
        //public async Task GetKebaruanReferensi_ShouldReturnSuccess_WhenRecordExists()
        //{
        //    var factory = new Mock<IDbConnectionFactory>();
        //    var connection = new Mock<DbConnection>();
        //    var uuid = Guid.NewGuid();

        //    var data = new KebaruanReferensiResponse
        //    {
        //        Uuid = uuid.ToString(),
        //        Nama = "Ref A",
        //        Skor = "30"
        //    };

        //    factory.Setup(f => f.OpenConnectionAsync()).ReturnsAsync(connection.Object);
        //    connection.SetupQuerySingleOrDefaultAsync(data);

        //    var handler = new GetKebaruanReferensiQueryHandler(factory.Object);

        //    var result = await handler.Handle(new GetKebaruanReferensiQuery(uuid), CancellationToken.None);

        //    Assert.True(result.IsSuccess);
        //    Assert.Equal("Ref A", result.Value.Nama);
        //}

        [Fact]
        public async Task GetKebaruanReferensi_ShouldReturnFailure_WhenRecordNotFound()
        {
            var factory = new Mock<IDbConnectionFactory>();
            var connection = new Mock<DbConnection>();

            connection.SetupDapperAsync(c =>
                c.QuerySingleOrDefaultAsync<KebaruanReferensiDefaultResponse?>(
                    It.IsAny<string>(),
                    It.IsAny<object>(),
                    null, null, null
                )
            ).ReturnsAsync((KebaruanReferensiDefaultResponse)null);

            var handler = new GetKebaruanReferensiQueryHandler(factory.Object);

            var result = await handler.Handle(new GetKebaruanReferensiQuery(Guid.NewGuid()), CancellationToken.None);

            Assert.True(result.IsFailure);
        }


        //[Fact]
        //public async Task GetBobotKebaruanReferensi_ShouldReturnSuccess_WhenSingleValueExists()
        //{
        //    var factory = new Mock<IDbConnectionFactory>();
        //    var connection = new Mock<DbConnection>();

        //    factory.Setup(f => f.OpenConnectionAsync()).ReturnsAsync(connection.Object);
        //    connection.SetupQueryAsync(new List<int> { 10 });

        //    var handler = new GetBobotKebaruanReferensiQueryHandler(factory.Object);

        //    var result = await handler.Handle(
        //        new GetBobotKebaruanReferensiQuery("Penelitian Dasar"),
        //        CancellationToken.None);

        //    Assert.True(result.IsSuccess);
        //    Assert.Equal(10, result.Value);
        //}

        [Fact]
        public async Task GetBobotKebaruanReferensi_ShouldReturnFailure_WhenKategoriUnknown()
        {
            var handler = new GetBobotKebaruanReferensiQueryHandler(new Mock<IDbConnectionFactory>().Object);

            var result = await handler.Handle(
                new GetBobotKebaruanReferensiQuery("Kategori Tidak Ada"),
                CancellationToken.None);

            Assert.True(result.IsFailure);
        }

        //[Fact]
        //public async Task GetBobotKebaruanReferensi_ShouldReturnFailure_WhenMultipleValuesFound()
        //{
        //    var factory = new Mock<IDbConnectionFactory>();
        //    var connection = new Mock<DbConnection>();

        //    connection.SetupDapperAsync(c => c.QueryAsync< new List<int>> (
        //    It.IsAny<string>(), null, null, null, null))
        //    .ReturnsAsync(new List<int> { 10, 20 });
        //    connection.SetupDapperAsync();

        //    var handler = new GetBobotKebaruanReferensiQueryHandler(factory.Object);

        //    var result = await handler.Handle(
        //        new GetBobotKebaruanReferensiQuery("Penelitian Dasar"),
        //        CancellationToken.None);

        //    Assert.True(result.IsFailure);
        //}
    }
}
