using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.InovasiPemecahanMasalah.Application.CreateInovasiPemecahanMasalah;
using UnpakSipaksi.Modules.InovasiPemecahanMasalah.Application.DeleteInovasiPemecahanMasalah;
using UnpakSipaksi.Modules.InovasiPemecahanMasalah.Application.UpdateInovasiPemecahanMasalah;
using UnpakSipaksi.Modules.InovasiPemecahanMasalah.ApplicationTest;
using Xunit;

namespace Application.Integration.Tests
{
    public class InovasiPemecahanMasalahTest : BaseIntegrationTest
    {
        public InovasiPemecahanMasalahTest(IntegrationTestWebAppFactory factory) : base(factory)
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
        }

        public static IEnumerable<object?[]> ValidData()
        {
            yield return new object?[] { new object[] { "tes", 10 }, null, "created" };
            yield return new object?[] { new object[] { "tes", 10 }, new object[] { "tes2", 20 }, "updated" };
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
            Result? result = null;
            if (mode == "created")
            {
                var command = new CreateInovasiPemecahanMasalahCommand(nama, skor);
                result = await Sender.Send(command);
            }
            else if (mode == "updated")
            {
                var command = new UpdateInovasiPemecahanMasalahCommand(uuid, nama, skor);
                result = await Sender.Send(command);
            }
            else
            {
                var command = new DeleteInovasiPemecahanMasalahCommand(uuid);
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
            var skorBefore = (int)beforeData[1];

            // --- CREATE ---
            var createCommand = new CreateInovasiPemecahanMasalahCommand(namaBefore, skorBefore);
            var createResult = await Sender.Send(createCommand);

            Assert.True(createResult.IsSuccess);
            var dataCreate = DBContext.InovasiPemecahanMasalah.FirstOrDefault(p => p.Uuid == createResult!.Value);
            Assert.NotNull(dataCreate);
            Assert.Equal(namaBefore, dataCreate.Nama);
            Assert.Equal(skorBefore, dataCreate.Skor);

            using (var scope = Factory.Services.CreateScope())
            {
                var handler = scope.ServiceProvider.GetService<IRequestHandler<CreateInovasiPemecahanMasalahCommand, Result<Guid>>>();
                Assert.NotNull(handler);
                Assert.IsType<CreateInovasiPemecahanMasalahCommandHandler>(handler);
            }

            var newUuid = createResult.Value.ToString();

            // --- UPDATE / DELETE ---
            if (mode == "updated")
            {
                var namaAfter = (string)afterData![0];
                var skorAfter = (int)afterData[1];

                var updateCommand = new UpdateInovasiPemecahanMasalahCommand(newUuid, namaAfter, skorAfter);
                var updateResult = await Sender.Send(updateCommand);

                Assert.True(updateResult.IsSuccess);
                var dataUpdate = DBContext.InovasiPemecahanMasalah.FirstOrDefault(p => p.Uuid == createResult!.Value);
                Assert.NotNull(dataUpdate);
                Assert.Equal(namaAfter, dataUpdate.Nama);
                Assert.Equal(skorAfter, dataUpdate.Skor);

                using (var scope = Factory.Services.CreateScope())
                {
                    var handler = scope.ServiceProvider.GetService<IRequestHandler<UpdateInovasiPemecahanMasalahCommand, Result>>();
                    Assert.NotNull(handler);
                    Assert.IsType<UpdateInovasiPemecahanMasalahCommandHandler>(handler);
                }
            }
            else if (mode == "deleted")
            {
                var deleteCommand = new DeleteInovasiPemecahanMasalahCommand(newUuid);
                var deleteResult = await Sender.Send(deleteCommand);
                Assert.True(deleteResult.IsSuccess);

                var deleted = DBContext.InovasiPemecahanMasalah.FirstOrDefault(p => p.Uuid == Guid.Parse(newUuid));
                Assert.Null(deleted);

                using (var scope = Factory.Services.CreateScope())
                {
                    var handler = scope.ServiceProvider.GetService<IRequestHandler<DeleteInovasiPemecahanMasalahCommand, Result>>();
                    Assert.NotNull(handler);
                    Assert.IsType<DeleteInovasiPemecahanMasalahCommandHandler>(handler);
                }
            }
        }

        [Fact]
        public async Task Create_ShouldThrow_WhenInvalidRuleDomain()
        {
            var namaBefore = "tes";
            var skorBefore = int.MaxValue;

            var createCommand = new CreateInovasiPemecahanMasalahCommand(namaBefore, skorBefore);
            var createResult = await Sender.Send(createCommand);

            Assert.True(createResult.IsFailure);
            Assert.Equal("InovasiPemecahanMasalah.InvalidSkor", createResult.Error.Code);
        }

        [Fact]
        public async Task Update_ShouldThrow_WhenNotExist()
        {
            var guid = Guid.NewGuid().ToString();
            var namaBefore = "tes";
            var skorBefore = 1;

            var updateCommand = new UpdateInovasiPemecahanMasalahCommand(guid, namaBefore, skorBefore);
            var updateResult = await Sender.Send(updateCommand);

            Assert.True(updateResult.IsFailure);
            Assert.Equal("InovasiPemecahanMasalah.NotFound", updateResult.Error.Code);
        }

        [Fact]
        public async Task Update_ShouldThrow_WhenInvalidRuleDomain()
        {
            // --- CREATE ---
            var namaBefore = "tes";
            var skorBefore = 1;

            var createCommand = new CreateInovasiPemecahanMasalahCommand(namaBefore, skorBefore);
            var createResult = await Sender.Send(createCommand);

            Assert.True(createResult.IsSuccess);
            var dataCreate = DBContext.InovasiPemecahanMasalah.FirstOrDefault(p => p.Uuid == createResult!.Value);
            Assert.NotNull(dataCreate);

            var newUuid = createResult.Value.ToString();

            // --- UPDATE ---
            var namaAfter = "tes2";
            var skorAfter = int.MaxValue;

            var updateCommand = new UpdateInovasiPemecahanMasalahCommand(newUuid, namaAfter, skorAfter);
            var updateResult = await Sender.Send(updateCommand);

            Assert.True(updateResult.IsFailure);
            Assert.Equal("InovasiPemecahanMasalah.InvalidSkor", updateResult.Error.Code);
        }

        [Fact]
        public async Task Delete_ShouldThrow_WhenNotExist()
        {
            var guid = Guid.NewGuid().ToString();

            var deleteCommand = new DeleteInovasiPemecahanMasalahCommand(guid);
            var deleteResult = await Sender.Send(deleteCommand);

            Assert.True(deleteResult.IsFailure);
            Assert.Equal("InovasiPemecahanMasalah.NotFound", deleteResult.Error.Code);
        }
    }
}
