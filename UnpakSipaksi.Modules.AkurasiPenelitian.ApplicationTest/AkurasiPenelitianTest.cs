using MediatR;
using Microsoft.Extensions.DependencyInjection;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.AkurasiPenelitian.Application.CreateAkurasiPenelitian;
using UnpakSipaksi.Modules.AkurasiPenelitian.Application.DeleteAkurasiPenelitian;
using UnpakSipaksi.Modules.AkurasiPenelitian.Application.GetAkurasiPenelitian;
using UnpakSipaksi.Modules.AkurasiPenelitian.Application.GetAllAkurasiPenelitian;
using UnpakSipaksi.Modules.AkurasiPenelitian.Application.UpdateAkurasiPenelitian;
using Xunit;

namespace Application.Integration.Tests
{
    public class AkurasiPenelitianTest : BaseIntegrationTest
    {
        public AkurasiPenelitianTest(IntegrationTestWebAppFactory factory) : base(factory)
        {

        }

        public static IEnumerable<object[]> InvalidData()
        {
            var valid = Guid.NewGuid().ToString();
            var empty = "";

            yield return new object[] { empty, "", 10, "'Nama' tidak boleh kosong.", "created" };
            yield return new object[] { empty, "tes", -10, "'Skor' tidak boleh negative.", "created" };

            yield return new object[] { valid, "", 10, "'Nama' tidak boleh kosong.", "updated" };
            yield return new object[] { valid, "tes", -10, "'Skor' tidak boleh negative.", "updated" };
            yield return new object[] { empty, "tes", 10, "'Uuid' tidak boleh kosong.", "updated" };
            yield return new object[] { "no-guid", "tes", 10, "'Uuid' harus dalam format UUID v4 yang valid.", "updated" };

            yield return new object[] { empty, "tes", 10, "'Uuid' tidak boleh kosong.", "deleted" };
            yield return new object[] { "no-guid", "tes", 10, "'Uuid' harus dalam format UUID v4 yang valid.", "deleted" };

            yield return new object[] { empty, "", 0, "'Uuid' tidak boleh kosong.", "get" };
            yield return new object[] { "no-guid", "", 0, "'Uuid' harus dalam format UUID v4 yang valid.", "get" };
        }

        public static IEnumerable<object?[]> ValidData()
        {
            yield return new object?[] { new object[] { "tes", 10 }, null, "created" };
            yield return new object?[] { new object[] { "tes", 10 }, new object[] { "tes2", 11 }, "updated" };
            yield return new object?[] { new object[] { "tes", 10 }, null, "deleted" };
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
                var command = new CreateAkurasiPenelitianCommand(nama, skor);
                result = await Sender.Send(command);
            }
            else if (mode == "updated")
            {
                var command = new UpdateAkurasiPenelitianCommand(uuid, nama, skor);
                result = await Sender.Send(command);
            }
            else if (mode == "deleted")
            {
                var command = new DeleteAkurasiPenelitianCommand(uuid);
                result = await Sender.Send(command);
            }
            else {
                var command = new GetAkurasiPenelitianQuery(uuid);
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

            var createCommand = new CreateAkurasiPenelitianCommand(namaBefore, skorBefore);
            var createResult = await Sender.Send(createCommand);

            Assert.True(createResult.IsSuccess);
            var dataCreate = DBContext.AkurasiPenelitian.FirstOrDefault(p => p.Uuid == createResult!.Value);

            Assert.NotNull(dataCreate);
            Assert.Equal(namaBefore, dataCreate.Nama);
            Assert.Equal(skorBefore, dataCreate.Skor);

            using (var scope = Factory.Services.CreateScope())
            {
                var handler = scope.ServiceProvider
                    .GetService<IRequestHandler<CreateAkurasiPenelitianCommand, Result<Guid>>>();

                Assert.NotNull(handler);
                Assert.IsType<CreateAkurasiPenelitianCommandHandler>(handler);
            }

            var newUuid = createResult.Value.ToString();

            // --- UPDATE / DELETE ---
            if (mode == "updated")
            {
                var namaAfter = (string)afterData![0];
                var skorAfter = (int)afterData[1];

                var updateCommand = new UpdateAkurasiPenelitianCommand(newUuid, namaAfter, skorAfter);
                var updateResult = await Sender.Send(updateCommand);

                Assert.True(updateResult.IsSuccess);
                var dataUpdate = DBContext.AkurasiPenelitian.FirstOrDefault(p => p.Uuid == createResult!.Value);

                Assert.NotNull(dataUpdate);
                Assert.Equal(namaAfter, dataUpdate.Nama);
                Assert.Equal(skorAfter, dataUpdate.Skor);

                using (var scope = Factory.Services.CreateScope())
                {
                    var handler = scope.ServiceProvider
                        .GetService<IRequestHandler<UpdateAkurasiPenelitianCommand, Result>>();

                    Assert.NotNull(handler);
                    Assert.IsType<UpdateAkurasiPenelitianCommandHandler>(handler);
                }
            }
            else if (mode == "deleted")
            {
                var deleteCommand = new DeleteAkurasiPenelitianCommand(newUuid);
                var deleteResult = await Sender.Send(deleteCommand);

                Assert.True(deleteResult.IsSuccess);
            }
        }

        [Fact]
        public async Task Create_ShouldThrow_WhenInvalidRuleDomain()
        {
            var namaBefore = "tes";
            var skorBefore = int.MaxValue;

            var createCommand = new CreateAkurasiPenelitianCommand(namaBefore, skorBefore);
            var createResult = await Sender.Send(createCommand);

            Assert.True(createResult.IsFailure);
            Assert.Equal("AkurasiPenelitian.InvalidSkor", createResult.Error.Code);
        }

        [Fact]
        public async Task Update_ShouldThrow_WhenNotExist()
        {
            var guid = Guid.NewGuid().ToString();
            var namaBefore = "tes";
            var skorBefore = 1;

            var updateCommand = new UpdateAkurasiPenelitianCommand(guid, namaBefore, skorBefore);
            var updateResult = await Sender.Send(updateCommand);

            Assert.True(updateResult.IsFailure);
            Assert.Equal("AkurasiPenelitian.NotFound", updateResult.Error.Code);
        }

        [Fact]
        public async Task Update_ShouldThrow_WhenInvalidRuleDomain()
        {
            // --- CREATE ---
            var namaBefore = "tes";
            var skorBefore = 1;

            var createCommand = new CreateAkurasiPenelitianCommand(namaBefore, skorBefore);
            var createResult = await Sender.Send(createCommand);

            Assert.True(createResult.IsSuccess);
            var dataCreate = DBContext.AkurasiPenelitian.FirstOrDefault(p => p.Uuid == createResult!.Value);

            Assert.NotNull(dataCreate);
            Assert.Equal(namaBefore, dataCreate.Nama);
            Assert.Equal(skorBefore, dataCreate.Skor);

            var newUuid = createResult.Value.ToString();

            // --- UPDATE ---
            //if (mode == "updated")
            //{
                var namaAfter = "tes2";
                var skorAfter = int.MaxValue;

                var updateCommand = new UpdateAkurasiPenelitianCommand(newUuid, namaAfter, skorAfter);
                var updateResult = await Sender.Send(updateCommand);

                Assert.True(updateResult.IsFailure);
                Assert.Equal("AkurasiPenelitian.InvalidSkor", updateResult.Error.Code);
            //}
        }

        [Fact]
        public async Task Delete_ShouldThrow_WhenNotExist()
        {
            var guid = Guid.NewGuid().ToString();

            var deleteCommand = new DeleteAkurasiPenelitianCommand(guid);
            var deleteResult = await Sender.Send(deleteCommand);

            Assert.True(deleteResult.IsFailure);
            Assert.Equal("AkurasiPenelitian.NotFound", deleteResult.Error.Code);
        }

        [Fact]
        public async Task Get_ShouldThrow_WhenNotExist()
        {
            var guid = Guid.NewGuid().ToString();
            var query = new GetAkurasiPenelitianQuery(guid);

            var result = await Sender.Send(query);

            Assert.True(result.IsFailure);
            Assert.Equal("AkurasiPenelitian.NotFound", result.Error.Code);
        }

        [Fact]
        public async Task GetDefault_ShouldThrow_WhenNotExist()
        {
            var guid = Guid.NewGuid();
            var query = new GetAkurasiPenelitianDefaultQuery(guid);

            var result = await Sender.Send(query);

            Assert.True(result.IsFailure);
            Assert.Equal("AkurasiPenelitian.NotFound", result.Error.Code);
        }

        [Fact]
        public async Task Get_ShouldReturnData_WhenExist()
        {
            // CREATE first
            var create = new CreateAkurasiPenelitianCommand("tes", 10);
            var resCreate = await Sender.Send(create);

            Assert.True(resCreate.IsSuccess);

            var uuid = resCreate.Value.ToString();

            // GET
            var query = new GetAkurasiPenelitianQuery(uuid);
            var result = await Sender.Send(query);

            Assert.True(result.IsSuccess);
            Assert.Equal(uuid, result.Value.Uuid.ToString());
            Assert.Equal("tes", result.Value.Nama);
            Assert.Equal(10, result.Value.Skor);

            // Ensure correct handler
            using var scope = Factory.Services.CreateScope();
            var handler = scope.ServiceProvider.GetService<IRequestHandler<GetAkurasiPenelitianQuery, Result<AkurasiPenelitianResponse>>>();
            Assert.NotNull(handler);
            Assert.IsType<GetAkurasiPenelitianQueryHandler>(handler);
        }

        [Fact]
        public async Task GetDefault_ShouldReturnData_WhenExist()
        {
            // CREATE first
            var create = new CreateAkurasiPenelitianCommand("tes", 10);
            var resCreate = await Sender.Send(create);

            Assert.True(resCreate.IsSuccess);

            var uuid = resCreate.Value;

            // GET DEFAULT
            var query = new GetAkurasiPenelitianDefaultQuery(uuid);
            var result = await Sender.Send(query);

            Assert.True(result.IsSuccess);
            Assert.Equal(uuid.ToString(), result.Value.Uuid.ToString());
            Assert.Equal("tes", result.Value.Nama);
            Assert.Equal(10, result.Value.Skor);

            // Ensure correct handler
            using var scope = Factory.Services.CreateScope();
            var handler = scope.ServiceProvider.GetService<IRequestHandler<GetAkurasiPenelitianDefaultQuery, Result<AkurasiPenelitianDefaultResponse>>>();
            Assert.NotNull(handler);
            Assert.IsType<GetAkurasiPenelitianDefaultQueryHandler>(handler);
        }

        [Fact]
        public async Task GetAll_ShouldThrow_WhenEmptyData()
        {
            // Pastikan tabel kosong
            DBContext.AkurasiPenelitian.RemoveRange(DBContext.AkurasiPenelitian);
            DBContext.SaveChanges();

            var query = new GetAllAkurasiPenelitianQuery();
            var result = await Sender.Send(query);

            Assert.True(result.IsFailure);
            Assert.Equal("AkurasiPenelitian.EmptyData", result.Error.Code);
        }

        [Fact]
        public async Task GetAll_ShouldReturnList_WhenDataExists()
        {
            // Clean dulu
            DBContext.AkurasiPenelitian.RemoveRange(DBContext.AkurasiPenelitian);
            DBContext.SaveChanges();

            // Create beberapa data
            var cmd1 = new CreateAkurasiPenelitianCommand("tes1", 10);
            var r1 = await Sender.Send(cmd1);
            Assert.True(r1.IsSuccess);

            var cmd2 = new CreateAkurasiPenelitianCommand("tes2", 20);
            var r2 = await Sender.Send(cmd2);
            Assert.True(r2.IsSuccess);

            var query = new GetAllAkurasiPenelitianQuery();
            var result = await Sender.Send(query);

            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Value);
            Assert.True(result.Value.Count >= 2);

            // Validasi mapping Dapper
            var item1 = result.Value.FirstOrDefault(a => a.Uuid == r1.Value.ToString());
            var item2 = result.Value.FirstOrDefault(a => a.Uuid == r2.Value.ToString());

            Assert.NotNull(item1);
            Assert.Equal("tes1", item1.Nama);
            Assert.Equal(10, item1.Skor);

            Assert.NotNull(item2);
            Assert.Equal("tes2", item2.Nama);
            Assert.Equal(20, item2.Skor);

            // Pastikan handler benar
            using var scope = Factory.Services.CreateScope();
            var handler = scope.ServiceProvider.GetService<
                IRequestHandler<GetAllAkurasiPenelitianQuery, Result<List<AkurasiPenelitianResponse>>>>
            ();

            Assert.NotNull(handler);
            Assert.IsType<GetAllAkurasiPenelitianQueryHandler>(handler);
        }
    }
}
