using Docker.DotNet.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.JumlahKolaboratorPublikasBereputasi.Application.CreateJumlahKolaboratorPublikasBereputasi;
using UnpakSipaksi.Modules.JumlahKolaboratorPublikasBereputasi.Application.DeleteJumlahKolaboratorPublikasBereputasi;
using UnpakSipaksi.Modules.JumlahKolaboratorPublikasBereputasi.Application.UpdateJumlahKolaboratorPublikasBereputasi;
using UnpakSipaksi.Modules.KesesuaianJadwal.Application.CreateKesesuaianJadwal;
using UnpakSipaksi.Modules.KesesuaianJadwal.Application.DeleteKesesuaianJadwal;
using UnpakSipaksi.Modules.KesesuaianJadwal.Application.UpdateKesesuaianJadwal;
using UnpakSipaksi.Modules.KesesuaianJadwal.ApplicationTest;
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
    }
}
