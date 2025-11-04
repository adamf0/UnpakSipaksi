using Docker.DotNet.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.JenisPublikasi.Application.CreateJenisPublikasi;
using UnpakSipaksi.Modules.JenisPublikasi.Application.DeleteJenisPublikasi;
using UnpakSipaksi.Modules.JenisPublikasi.Application.UpdateJenisPublikasi;
using UnpakSipaksi.Modules.JenisPublikasi.ApplicationtTest;
using Xunit;

namespace Application.Integration.Tests
{
    public class JenisPublikasiTest : BaseIntegrationTest
    {
        public JenisPublikasiTest(IntegrationTestWebAppFactory factory) : base(factory)
        {

        }

        public static IEnumerable<object[]> InvalidData()
        {
            var valid = Guid.NewGuid().ToString();
            var empty = "";

            // CREATE
            yield return new object[] { empty, "", 10, "'Nama' tidak boleh kosong.", "created" };
            yield return new object[] { empty, "tes", -10, "'Sbu' tidak boleh negative.", "created" };

            // UPDATE
            yield return new object[] { valid, "", 10, "'Nama' tidak boleh kosong.", "updated" };
            yield return new object[] { valid, "tes", -10, "'Sbu' tidak boleh negative.", "updated" };
            yield return new object[] { empty, "tes", 10, "'Uuid' tidak boleh kosong.", "updated" };
            yield return new object[] { "no-guid", "tes", 10, "'Uuid' harus dalam format UUID v4 yang valid.", "updated" };

            // DELETE
            yield return new object[] { empty, "tes", 10, "'Uuid' tidak boleh kosong.", "deleted" };
            yield return new object[] { "no-guid", "tes", 10, "'Uuid' harus dalam format UUID v4 yang valid.", "deleted" };
        }

        public static IEnumerable<object?[]> ValidData()
        {
            yield return new object?[] { new object[] { "tes", 1000 }, null, "created" };
            yield return new object?[] { new object[] { "tes", 1000 }, new object[] { "tes2", 2000 }, "updated" };
            yield return new object?[] { new object[] { "tes", 1000 }, null, "deleted" };
        }

        [Theory]
        [MemberData(nameof(InvalidData))]
        public async Task CreateUpdateDelete_ShouldThrow_WhenInvalidFluentValidation(
            string uuid,
            string nama,
            int sbu,
            string message,
            string mode)
        {
            Result? result = null;

            if (mode == "created")
            {
                var command = new CreateJenisPublikasiCommand(nama, sbu);
                result = await Sender.Send(command);
            }
            else if (mode == "updated")
            {
                var command = new UpdateJenisPublikasiCommand(uuid, nama, sbu);
                result = await Sender.Send(command);
            }
            else
            {
                var command = new DeleteJenisPublikasiCommand(uuid);
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
            // --- CREATE ---
            var namaBefore = (string)beforeData[0];
            var sbuBefore = (int)beforeData[1];

            var createCommand = new CreateJenisPublikasiCommand(namaBefore, sbuBefore);
            var createResult = await Sender.Send(createCommand);

            Assert.True(createResult.IsSuccess);
            var dataCreate = DBContext.JenisPublikasi.FirstOrDefault(p => p.Uuid == createResult!.Value);

            Assert.NotNull(dataCreate);
            Assert.Equal(namaBefore, dataCreate.Nama);
            Assert.Equal(sbuBefore, dataCreate.Sbu);

            using (var scope = Factory.Services.CreateScope())
            {
                var handler = scope.ServiceProvider
                    .GetService<IRequestHandler<CreateJenisPublikasiCommand, Result<Guid>>>();

                Assert.NotNull(handler);
                Assert.IsType<CreateJenisPublikasiCommandHandler>(handler);
            }

            var newUuid = createResult.Value.ToString();

            // --- UPDATE / DELETE ---
            if (mode == "updated")
            {
                var namaAfter = (string)afterData![0];
                var sbuAfter = (int)afterData[1];

                var updateCommand = new UpdateJenisPublikasiCommand(newUuid, namaAfter, sbuAfter);
                var updateResult = await Sender.Send(updateCommand);

                Assert.True(updateResult.IsSuccess);
                var dataUpdate = DBContext.JenisPublikasi.FirstOrDefault(p => p.Uuid == createResult!.Value);

                Assert.NotNull(dataUpdate);
                Assert.Equal(namaAfter, dataUpdate.Nama);
                Assert.Equal(sbuAfter, dataUpdate.Sbu);

                using (var scope = Factory.Services.CreateScope())
                {
                    var handler = scope.ServiceProvider
                        .GetService<IRequestHandler<UpdateJenisPublikasiCommand, Result>>();

                    Assert.NotNull(handler);
                    Assert.IsType<UpdateJenisPublikasiCommandHandler>(handler);
                }
            }
            else if (mode == "deleted")
            {
                var deleteCommand = new DeleteJenisPublikasiCommand(newUuid);
                var deleteResult = await Sender.Send(deleteCommand);

                Assert.True(deleteResult.IsSuccess);
            }
        }

        [Fact]
        public async Task Create_ShouldThrow_WhenInvalidRuleDomain()
        {
            var nama = "tes";
            var sbu = int.MaxValue; // misal terlalu besar, melanggar aturan domain

            var command = new CreateJenisPublikasiCommand(nama, sbu);
            var result = await Sender.Send(command);

            Assert.True(result.IsFailure);
            Assert.Equal("JenisPublikasi.InvalidSbu", result.Error.Code);
        }

        [Fact]
        public async Task Update_ShouldThrow_WhenNotExist()
        {
            var guid = Guid.NewGuid().ToString();
            var nama = "tes";
            var sbu = 1;

            var command = new UpdateJenisPublikasiCommand(guid, nama, sbu);
            var result = await Sender.Send(command);

            Assert.True(result.IsFailure);
            Assert.Equal("JenisPublikasi.NotFound", result.Error.Code);
        }

        [Fact]
        public async Task Update_ShouldThrow_WhenInvalidRuleDomain()
        {
            // --- CREATE ---
            var namaBefore = "tes";
            var sbuBefore = 10;

            var createCommand = new CreateJenisPublikasiCommand(namaBefore, sbuBefore);
            var createResult = await Sender.Send(createCommand);

            Assert.True(createResult.IsSuccess);
            var dataCreate = DBContext.JenisPublikasi.FirstOrDefault(p => p.Uuid == createResult!.Value);

            Assert.NotNull(dataCreate);
            Assert.Equal(namaBefore, dataCreate.Nama);
            Assert.Equal(sbuBefore, dataCreate.Sbu);

            var newUuid = createResult.Value.ToString();

            // --- UPDATE invalid ---
            var namaAfter = "tes2";
            var sbuAfter = int.MaxValue; // nilai melanggar aturan domain

            var updateCommand = new UpdateJenisPublikasiCommand(newUuid, namaAfter, sbuAfter);
            var updateResult = await Sender.Send(updateCommand);

            Assert.True(updateResult.IsFailure);
            Assert.Equal("JenisPublikasi.InvalidSbu", updateResult.Error.Code);
        }

        [Fact]
        public async Task Delete_ShouldThrow_WhenNotExist()
        {
            var guid = Guid.NewGuid().ToString();

            var command = new DeleteJenisPublikasiCommand(guid);
            var result = await Sender.Send(command);

            Assert.True(result.IsFailure);
            Assert.Equal("JenisPublikasi.NotFound", result.Error.Code);
        }
    }
}
