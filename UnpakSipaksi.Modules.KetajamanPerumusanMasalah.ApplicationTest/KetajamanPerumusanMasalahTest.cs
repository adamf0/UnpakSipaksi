using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KetajamanPerumusanMasalah.Application.CreateKetajamanPerumusanMasalah;
using UnpakSipaksi.Modules.KetajamanPerumusanMasalah.Application.DeleteKetajamanPerumusanMasalah;
using UnpakSipaksi.Modules.KetajamanPerumusanMasalah.Application.UpdateKetajamanPerumusanMasalah;
using Xunit;

namespace UnpakSipaksi.Modules.KetajamanPerumusanMasalah.ApplicationTest
{
    public class KetajamanPerumusanMasalahTest : BaseIntegrationTest
    {
        public KetajamanPerumusanMasalahTest(IntegrationTestWebAppFactory factory) : base(factory)
        {
        }

        public static IEnumerable<object[]> InvalidData()
        {
            var validUuid = Guid.NewGuid().ToString();
            var empty = "";

            // CREATE
            yield return new object[] { validUuid, "", 10, "'Nama' tidak boleh kosong.", "created" };
            yield return new object[] { validUuid, "Tes", -10, "'Skor' tidak boleh negative.", "created" };

            // UPDATE
            yield return new object[] { validUuid, "", 10, "'Nama' tidak boleh kosong.", "updated" };
            yield return new object[] { empty, "Tes", 10, "'Uuid' tidak boleh kosong.", "updated" };
            yield return new object[] { "no-guid", "Tes", 10, "'Uuid' harus dalam format UUID v4 yang valid.", "updated" };

            // DELETE
            yield return new object[] { empty, "Tes", 10, "'Uuid' tidak boleh kosong.", "deleted" };
            yield return new object[] { "no-guid", "Tes", 10, "'Uuid' harus dalam format UUID v4 yang valid.", "deleted" };
        }

        public static IEnumerable<object?[]> ValidData()
        {
            yield return new object?[] { new object[] { Guid.NewGuid().ToString(), "Tes", 100 }, null, "created" };
            yield return new object?[] { new object[] { Guid.NewGuid().ToString(), "Tes", 100 }, new object[] { "Tes Updated", 200 }, "updated" };
            yield return new object?[] { new object[] { Guid.NewGuid().ToString(), "Tes", 100 }, null, "deleted" };
        }

        [Theory]
        [MemberData(nameof(InvalidData))]
        public async Task CreateUpdateDelete_ShouldThrow_WhenInvalidData(
            string uuid,
            string nama,
            int skor,
            string message,
            string mode)
        {
            Result? result = null;

            if (mode == "created")
            {
                var command = new CreateKetajamanPerumusanMasalahCommand(nama, skor);
                result = await Sender.Send(command);
            }
            else if (mode == "updated")
            {
                var command = new UpdateKetajamanPerumusanMasalahCommand(uuid, nama, skor);
                result = await Sender.Send(command);
            }
            else if (mode == "deleted")
            {
                var command = new DeleteKetajamanPerumusanMasalahCommand(uuid);
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
            var uuid = (string)beforeData[0];
            var namaBefore = (string)beforeData[1];
            var skorBefore = (int)beforeData[2];

            // CREATE
            var createCommand = new CreateKetajamanPerumusanMasalahCommand(namaBefore, skorBefore);
            var createResult = await Sender.Send(createCommand);

            Assert.True(createResult.IsSuccess);
            var dataCreate = DBContext.KetajamanPerumusanMasalah.FirstOrDefault(p => p.Uuid == createResult!.Value);

            Assert.NotNull(dataCreate);
            Assert.Equal(namaBefore, dataCreate.Nama);
            Assert.Equal(skorBefore, dataCreate.Skor);

            using (var scope = Factory.Services.CreateScope())
            {
                var handler = scope.ServiceProvider
                    .GetService<IRequestHandler<CreateKetajamanPerumusanMasalahCommand, Result<Guid>>>();

                Assert.NotNull(handler);
                Assert.IsType<CreateKetajamanPerumusanMasalahCommandHandler>(handler);
            }

            var newUuid = createResult.Value.ToString();

            // UPDATE / DELETE
            if (mode == "updated")
            {
                var namaAfter = (string)afterData![0];
                var skorAfter = (int)afterData[1];

                var updateCommand = new UpdateKetajamanPerumusanMasalahCommand(newUuid, namaAfter, skorAfter);
                var updateResult = await Sender.Send(updateCommand);

                Assert.True(updateResult.IsSuccess);
                var dataUpdate = DBContext.KetajamanPerumusanMasalah.FirstOrDefault(p => p.Uuid == createResult!.Value);

                Assert.NotNull(dataUpdate);
                Assert.Equal(namaAfter, dataUpdate.Nama);
                Assert.Equal(skorAfter, dataUpdate.Skor);

                using (var scope = Factory.Services.CreateScope())
                {
                    var handler = scope.ServiceProvider
                        .GetService<IRequestHandler<UpdateKetajamanPerumusanMasalahCommand, Result>>();

                    Assert.NotNull(handler);
                    Assert.IsType<UpdateKetajamanPerumusanMasalahCommandHandler>(handler);
                }
            }
            else if (mode == "deleted")
            {
                var deleteCommand = new DeleteKetajamanPerumusanMasalahCommand(newUuid);
                var deleteResult = await Sender.Send(deleteCommand);

                Assert.True(deleteResult.IsSuccess);
            }
        }

        [Fact]
        public async Task Create_ShouldThrow_WhenInvalidRuleDomain()
        {
            var nama = "tes";
            var skor = int.MaxValue; // contoh melanggar aturan domain

            var command = new CreateKetajamanPerumusanMasalahCommand(nama, skor);
            var result = await Sender.Send(command);

            Assert.True(result.IsFailure);
            Assert.Equal("KetajamanPerumusanMasalah.InvalidValueSkor", result.Error.Code);
        }

        [Fact]
        public async Task Update_ShouldThrow_WhenNotExist()
        {
            var guid = Guid.NewGuid().ToString();
            var nama = "tes";
            var skor = 10;

            var command = new UpdateKetajamanPerumusanMasalahCommand(guid, nama, skor);
            var result = await Sender.Send(command);

            Assert.True(result.IsFailure);
            Assert.Equal("KetajamanPerumusanMasalah.NotFound", result.Error.Code);
        }

        [Fact]
        public async Task Update_ShouldThrow_WhenInvalidRuleDomain()
        {
            // --- CREATE ---
            var namaBefore = "tes";
            var skorBefore = 10;

            var createCommand = new CreateKetajamanPerumusanMasalahCommand(namaBefore, skorBefore);
            var createResult = await Sender.Send(createCommand);

            Assert.True(createResult.IsSuccess);
            var dataCreate = DBContext.KetajamanPerumusanMasalah.FirstOrDefault(p => p.Uuid == createResult!.Value);

            Assert.NotNull(dataCreate);
            Assert.Equal(namaBefore, dataCreate.Nama);
            Assert.Equal(skorBefore, dataCreate.Skor);

            var newUuid = createResult.Value.ToString();

            // --- UPDATE (melanggar aturan domain)
            var namaAfter = "tes2";
            var skorAfter = int.MaxValue;

            var updateCommand = new UpdateKetajamanPerumusanMasalahCommand(newUuid, namaAfter, skorAfter);
            var updateResult = await Sender.Send(updateCommand);

            Assert.True(updateResult.IsFailure);
            Assert.Equal("KetajamanPerumusanMasalah.InvalidValueSkor", updateResult.Error.Code);
        }

        [Fact]
        public async Task Delete_ShouldThrow_WhenNotExist()
        {
            var guid = Guid.NewGuid().ToString();

            var command = new DeleteKetajamanPerumusanMasalahCommand(guid);
            var result = await Sender.Send(command);

            Assert.True(result.IsFailure);
            Assert.Equal("KetajamanPerumusanMasalah.NotFound", result.Error.Code);
        }
    }
}
