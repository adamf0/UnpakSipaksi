using Docker.DotNet.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.PotensiKetercapaianLuaranDijanjikan.Application.CreatePotensiKetercapaianLuaranDijanjikan;
using UnpakSipaksi.Modules.PotensiKetercapaianLuaranDijanjikan.Application.DeletePotensiKetercapaianLuaranDijanjikan;
using UnpakSipaksi.Modules.PotensiKetercapaianLuaranDijanjikan.Application.UpdatePotensiKetercapaianLuaranDijanjikan;
using UnpakSipaksi.Modules.PotensiKetercapaianLuaranDijanjikan.ApplicationTest;
using Xunit;

namespace Application.Integration.Tests
{
    public class PotensiKetercapaianLuaranDijanjikanTest : BaseIntegrationTest
    {
        public PotensiKetercapaianLuaranDijanjikanTest(IntegrationTestWebAppFactory factory) : base(factory)
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
            Result? result = null;

            if (mode == "created")
            {
                var command = new CreatePotensiKetercapaianLuaranDijanjikanCommand(nama, skor);
                result = await Sender.Send(command);
            }
            else if (mode == "updated")
            {
                var command = new UpdatePotensiKetercapaianLuaranDijanjikanCommand(uuid, nama, skor);
                result = await Sender.Send(command);
            }
            else
            {
                var command = new DeletePotensiKetercapaianLuaranDijanjikanCommand(uuid);
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
            var createCommand = new CreatePotensiKetercapaianLuaranDijanjikanCommand(namaBefore, skorBefore);
            var createResult = await Sender.Send(createCommand);

            Assert.True(createResult.IsSuccess);
            var dataCreate = DBContext.PotensiKetercapaianLuaranDijanjikan.FirstOrDefault(p => p.Uuid == createResult!.Value);

            Assert.NotNull(dataCreate);
            Assert.Equal(namaBefore, dataCreate.Nama);
            Assert.Equal(skorBefore, dataCreate.Skor);

            using (var scope = Factory.Services.CreateScope())
            {
                var handler = scope.ServiceProvider
                    .GetService<IRequestHandler<CreatePotensiKetercapaianLuaranDijanjikanCommand, Result<Guid>>>();

                Assert.NotNull(handler);
                Assert.IsType<CreatePotensiKetercapaianLuaranDijanjikanCommandHandler>(handler);
            }

            var newUuid = createResult.Value.ToString();

            // --- UPDATE / DELETE ---
            if (mode == "updated")
            {
                var namaAfter = (string)afterData![0];
                var skorAfter = (int)afterData[1];

                var updateCommand = new UpdatePotensiKetercapaianLuaranDijanjikanCommand(newUuid, namaAfter, skorAfter);
                var updateResult = await Sender.Send(updateCommand);

                Assert.True(updateResult.IsSuccess);
                var dataUpdate = DBContext.PotensiKetercapaianLuaranDijanjikan.FirstOrDefault(p => p.Uuid == createResult!.Value);

                Assert.NotNull(dataUpdate);
                Assert.Equal(namaAfter, dataUpdate.Nama);
                Assert.Equal(skorAfter, dataUpdate.Skor);

                using (var scope = Factory.Services.CreateScope())
                {
                    var handler = scope.ServiceProvider
                        .GetService<IRequestHandler<UpdatePotensiKetercapaianLuaranDijanjikanCommand, Result>>();

                    Assert.NotNull(handler);
                    Assert.IsType<UpdatePotensiKetercapaianLuaranDijanjikanCommandHandler>(handler);
                }
            }
            else if (mode == "deleted")
            {
                var deleteCommand = new DeletePotensiKetercapaianLuaranDijanjikanCommand(newUuid);
                var deleteResult = await Sender.Send(deleteCommand);

                Assert.True(deleteResult.IsSuccess);

                using (var scope = Factory.Services.CreateScope())
                {
                    var handler = scope.ServiceProvider
                        .GetService<IRequestHandler<DeletePotensiKetercapaianLuaranDijanjikanCommand, Result>>();

                    Assert.NotNull(handler);
                    Assert.IsType<DeletePotensiKetercapaianLuaranDijanjikanCommandHandler>(handler);
                }
            }
        }
        [Fact]
        public async Task Create_ShouldThrow_WhenInvalidRuleDomain()
        {
            var nama = "tes";
            var skor = int.MaxValue; // contoh melanggar aturan domain

            var command = new CreatePotensiKetercapaianLuaranDijanjikanCommand(nama, skor);
            var result = await Sender.Send(command);

            Assert.True(result.IsFailure);
            Assert.Equal("PotensiKetercapaianLuaranDijanjikan.InvalidValueSkor", result.Error.Code);
        }

        [Fact]
        public async Task Update_ShouldThrow_WhenNotExist()
        {
            var guid = Guid.NewGuid().ToString();
            var nama = "tes";
            var skor = 10;

            var command = new UpdatePotensiKetercapaianLuaranDijanjikanCommand(guid, nama, skor);
            var result = await Sender.Send(command);

            Assert.True(result.IsFailure);
            Assert.Equal("PotensiKetercapaianLuaranDijanjikan.NotFound", result.Error.Code);
        }

        [Fact]
        public async Task Update_ShouldThrow_WhenInvalidRuleDomain()
        {
            // --- CREATE ---
            var namaBefore = "tes";
            var skorBefore = 10;

            var createCommand = new CreatePotensiKetercapaianLuaranDijanjikanCommand(namaBefore, skorBefore);
            var createResult = await Sender.Send(createCommand);

            Assert.True(createResult.IsSuccess);
            var dataCreate = DBContext.PotensiKetercapaianLuaranDijanjikan.FirstOrDefault(p => p.Uuid == createResult!.Value);

            Assert.NotNull(dataCreate);
            Assert.Equal(namaBefore, dataCreate.Nama);
            Assert.Equal(skorBefore, dataCreate.Skor);

            var newUuid = createResult.Value.ToString();

            // --- UPDATE (melanggar aturan domain)
            var namaAfter = "tes2";
            var skorAfter = int.MaxValue;

            var updateCommand = new UpdatePotensiKetercapaianLuaranDijanjikanCommand(newUuid, namaAfter, skorAfter);
            var updateResult = await Sender.Send(updateCommand);

            Assert.True(updateResult.IsFailure);
            Assert.Equal("PotensiKetercapaianLuaranDijanjikan.InvalidValueSkor", updateResult.Error.Code);
        }

        [Fact]
        public async Task Delete_ShouldThrow_WhenNotExist()
        {
            var guid = Guid.NewGuid().ToString();

            var command = new DeletePotensiKetercapaianLuaranDijanjikanCommand(guid);
            var result = await Sender.Send(command);

            Assert.True(result.IsFailure);
            Assert.Equal("PotensiKetercapaianLuaranDijanjikan.NotFound", result.Error.Code);
        }
    }
}
