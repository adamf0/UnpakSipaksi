using Docker.DotNet.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.JumlahKolaboratorPublikasBereputasi.Application.CreateJumlahKolaboratorPublikasBereputasi;
using UnpakSipaksi.Modules.JumlahKolaboratorPublikasBereputasi.Application.DeleteJumlahKolaboratorPublikasBereputasi;
using UnpakSipaksi.Modules.JumlahKolaboratorPublikasBereputasi.Application.UpdateJumlahKolaboratorPublikasBereputasi;
using UnpakSipaksi.Modules.KejelasanPembagianTugasTim.Application.CreateKejelasanPembagianTugasTim;
using UnpakSipaksi.Modules.KejelasanPembagianTugasTim.Application.DeleteKejelasanPembagianTugasTim;
using UnpakSipaksi.Modules.KejelasanPembagianTugasTim.Application.UpdateKejelasanPembagianTugasTim;
using UnpakSipaksi.Modules.KejelasanPembagianTugasTim.ApplicationTest;
using Xunit;

namespace Application.Integration.Tests
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
    }
}
