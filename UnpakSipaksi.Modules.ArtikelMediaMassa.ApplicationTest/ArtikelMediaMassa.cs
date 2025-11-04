using Docker.DotNet.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.AkurasiPenelitian.Application.CreateAkurasiPenelitian;
using UnpakSipaksi.Modules.AkurasiPenelitian.Application.DeleteAkurasiPenelitian;
using UnpakSipaksi.Modules.AkurasiPenelitian.Application.UpdateAkurasiPenelitian;
using UnpakSipaksi.Modules.ArtikelMediaMassa.Application.CreateArtikelMediaMassa;
using UnpakSipaksi.Modules.ArtikelMediaMassa.Application.DeleteArtikelMediaMassa;
using UnpakSipaksi.Modules.ArtikelMediaMassa.Application.UpdateArtikelMediaMassa;
using UnpakSipaksi.Modules.ArtikelMediaMassa.ApplicationTest;
using Xunit;

namespace Application.Integration.Tests
{
    public class ArtikelMediaMassaTest : BaseIntegrationTest
    {
        public ArtikelMediaMassaTest(IntegrationTestWebAppFactory factory) : base(factory)
        {

        }

        public static IEnumerable<object[]> InvalidData()
        {
            var valid = Guid.NewGuid().ToString();
            var empty = "";

            yield return new object[] { empty, "", 10, "'Nama' tidak boleh kosong.", "created" };
            yield return new object[] { empty, "tes", -10, "'Nilai' tidak boleh negative.", "created" };

            yield return new object[] { valid, "", 10, "'Nama' tidak boleh kosong.", "updated" };
            yield return new object[] { valid, "tes", -10, "'Nilai' tidak boleh negative.", "updated" };
            yield return new object[] { empty, "tes", 10, "'Uuid' tidak boleh kosong.", "updated" };
            yield return new object[] { "no-guid", "tes", 10, "'Uuid' harus dalam format UUID v4 yang valid.", "updated" };

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
            int nilai,
            string message,
            string mode)
        {
            // Act
            Result? result = null;
            if (mode == "created")
            {
                var command = new CreateArtikelMediaMassaCommand(nama, nilai);
                result = await Sender.Send(command);
            }
            else if (mode == "updated")
            {
                var command = new UpdateArtikelMediaMassaCommand(uuid, nama, nilai);
                result = await Sender.Send(command);
            }
            else
            {
                var command = new DeleteArtikelMediaMassaCommand(uuid);
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

            var createCommand = new CreateArtikelMediaMassaCommand(namaBefore, nilaiBefore);
            var createResult = await Sender.Send(createCommand);

            Assert.True(createResult.IsSuccess);
            var dataCreate = DBContext.ArtikelMediaMassa.FirstOrDefault(p => p.Uuid == createResult!.Value);

            Assert.NotNull(dataCreate);
            Assert.Equal(namaBefore, dataCreate.Nama);
            Assert.Equal(nilaiBefore, dataCreate.Nilai);

            using (var scope = Factory.Services.CreateScope())
            {
                var handler = scope.ServiceProvider
                    .GetService<IRequestHandler<CreateArtikelMediaMassaCommand, Result<Guid>>>();

                Assert.NotNull(handler);
                Assert.IsType<CreateArtikelMediaMassaCommandHandler>(handler);
            }

            var newUuid = createResult.Value.ToString();

            // --- UPDATE / DELETE ---
            if (mode == "updated")
            {
                var namaAfter = (string)afterData![0];
                var nilaiAfter = (int)afterData[1];

                var updateCommand = new UpdateArtikelMediaMassaCommand(newUuid, namaAfter, nilaiAfter);
                var updateResult = await Sender.Send(updateCommand);

                Assert.True(updateResult.IsSuccess);
                var dataUpdate = DBContext.ArtikelMediaMassa.FirstOrDefault(p => p.Uuid == createResult!.Value);

                Assert.NotNull(dataUpdate);
                Assert.Equal(namaAfter, dataUpdate.Nama);
                Assert.Equal(nilaiAfter, dataUpdate.Nilai);

                using (var scope = Factory.Services.CreateScope())
                {
                    var handler = scope.ServiceProvider
                        .GetService<IRequestHandler<UpdateArtikelMediaMassaCommand, Result>>();

                    Assert.NotNull(handler);
                    Assert.IsType<UpdateArtikelMediaMassaCommandHandler>(handler);
                }
            }
            else if (mode == "deleted")
            {
                var deleteCommand = new DeleteArtikelMediaMassaCommand(newUuid);
                var deleteResult = await Sender.Send(deleteCommand);

                Assert.True(deleteResult.IsSuccess);
            }
        }

        [Fact]
        public async Task Create_ShouldThrow_WhenInvalidRuleDomain()
        {
            var namaBefore = "tes";
            var nilaiBefore = int.MaxValue;

            var createCommand = new CreateArtikelMediaMassaCommand(namaBefore, nilaiBefore);
            var createResult = await Sender.Send(createCommand);

            Assert.True(createResult.IsFailure);
            Assert.Equal("ArtikelMediaMassa.InvalidNilai", createResult.Error.Code);
        }

        [Fact]
        public async Task Update_ShouldThrow_WhenNotExist()
        {
            var guid = Guid.NewGuid().ToString();
            var namaBefore = "tes";
            var nilaiBefore = 1;

            var updateCommand = new UpdateArtikelMediaMassaCommand(guid, namaBefore, nilaiBefore);
            var updateResult = await Sender.Send(updateCommand);

            Assert.True(updateResult.IsFailure);
            Assert.Equal("ArtikelMediaMassa.NotFound", updateResult.Error.Code);
        }

        [Fact]
        public async Task Update_ShouldThrow_WhenInvalidRuleDomain()
        {
            // --- CREATE ---
            var namaBefore = "tes";
            var nilaiBefore = 1;

            var createCommand = new CreateArtikelMediaMassaCommand(namaBefore, nilaiBefore);
            var createResult = await Sender.Send(createCommand);

            Assert.True(createResult.IsSuccess);
            var dataCreate = DBContext.ArtikelMediaMassa.FirstOrDefault(p => p.Uuid == createResult!.Value);

            Assert.NotNull(dataCreate);
            Assert.Equal(namaBefore, dataCreate.Nama);
            Assert.Equal(nilaiBefore, dataCreate.Nilai);

            var newUuid = createResult.Value.ToString();

            // --- UPDATE ---
            //if (mode == "updated")
            //{
            var namaAfter = "tes2";
            var nilaiAfter = int.MaxValue;

            var updateCommand = new UpdateAkurasiPenelitianCommand(newUuid, namaAfter, nilaiAfter);
            var updateResult = await Sender.Send(updateCommand);

            Assert.True(updateResult.IsFailure);
            Assert.Equal("ArtikelMediaMassa.InvalidNilai", updateResult.Error.Code);
            //}
        }

        [Fact]
        public async Task Delete_ShouldThrow_WhenNotExist()
        {
            var guid = Guid.NewGuid().ToString();

            var deleteCommand = new DeleteAkurasiPenelitianCommand(guid);
            var deleteResult = await Sender.Send(deleteCommand);

            Assert.True(deleteResult.IsFailure);
            Assert.Equal("ArtikelMediaMassa.NotFound", deleteResult.Error.Code);
        }
    }
}
