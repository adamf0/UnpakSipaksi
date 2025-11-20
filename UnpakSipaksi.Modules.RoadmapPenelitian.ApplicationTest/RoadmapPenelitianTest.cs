using Docker.DotNet.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.RoadmapPenelitian.Application.CreateRoadmapPenelitian;
using UnpakSipaksi.Modules.RoadmapPenelitian.Application.DeleteRoadmapPenelitian;
using UnpakSipaksi.Modules.RoadmapPenelitian.Application.UpdateRoadmapPenelitian;
using UnpakSipaksi.Modules.RoadmapPenelitian.ApplicationTest;
using Xunit;

namespace Application.Integration.Tests
{
    public class RoadmapPenelitianTest : BaseIntegrationTest
    {
        public RoadmapPenelitianTest(IntegrationTestWebAppFactory factory) : base(factory)
        {
        }

        public static IEnumerable<object[]> InvalidData()
        {
            var empty = "";
            var validUuid = Guid.NewGuid().ToString();

            // CREATE
            yield return new object[] { empty, "", 100, "'Nama' tidak boleh kosong.", "created" };
            yield return new object[] { empty, "tes", -100, "'Skor' tidak boleh negative.", "created" };

            // UPDATE
            yield return new object[] { validUuid, "", 100, "'Nama' tidak boleh kosong.", "updated" };
            yield return new object[] { validUuid, "tes", -100, "'Skor' tidak boleh negative.", "updated" };
            yield return new object[] { empty, "tes", 100, "'Uuid' tidak boleh kosong.", "updated" };
            yield return new object[] { "no-guid", "tes", 100, "'Uuid' harus dalam format UUID v4 yang valid.", "updated" };

            // DELETE
            yield return new object[] { empty, "tes", 100, "'Uuid' tidak boleh kosong.", "deleted" };
            yield return new object[] { "no-guid", "tes", 100, "'Uuid' harus dalam format UUID v4 yang valid.", "deleted" };
        }

        public static IEnumerable<object?[]> ValidData()
        {
            yield return new object?[] { new object[] { "tes", 100 }, null, "created" };
            yield return new object?[] { new object[] { "tes", 100 }, new object[] { "tes-update", 200 }, "updated" };
            yield return new object?[] { new object[] { "tes", 100 }, null, "deleted" };
        }

        [Theory]
        [MemberData(nameof(InvalidData))]
        public async Task CreateUpdateDelete_ShouldThrow_WhenInvalid(
            string uuid,
            string nama,
            int skor,
            string message,
            string mode)
        {
            Result? result = null;

            if (mode == "created")
            {
                var command = new CreateRoadmapPenelitianCommand(nama, skor);
                result = await Sender.Send(command);
            }
            else if (mode == "updated")
            {
                var command = new UpdateRoadmapPenelitianCommand(uuid, nama, skor);
                result = await Sender.Send(command);
            }
            else if (mode == "deleted")
            {
                var command = new DeleteRoadmapPenelitianCommand(uuid);
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
        public async Task CreateUpdateDelete_ShouldExecute_WhenValid(
            object[] beforeData,
            object[]? afterData,
            string mode)
        {
            var namaBefore = (string)beforeData[0];
            var skorBefore = (int)beforeData[1];

            // --- CREATE ---
            var createCommand = new CreateRoadmapPenelitianCommand(namaBefore, skorBefore);
            var createResult = await Sender.Send(createCommand);

            Assert.True(createResult.IsSuccess);

            var dataCreate = DBContext.RoadmapPenelitian.FirstOrDefault(p => p.Uuid == createResult!.Value);
            Assert.NotNull(dataCreate);
            Assert.Equal(namaBefore, dataCreate.Nama);

            using (var scope = Factory.Services.CreateScope())
            {
                var handler = scope.ServiceProvider
                    .GetService<IRequestHandler<CreateRoadmapPenelitianCommand, Result<Guid>>>();

                Assert.NotNull(handler);
                Assert.IsType<CreateRoadmapPenelitianCommandHandler>(handler);
            }

            var newUuid = createResult.Value.ToString();

            // --- UPDATE / DELETE ---
            if (mode == "updated")
            {
                var namaAfter = (string)afterData![0];
                var skorAfter = (int)afterData[1];

                var updateCommand = new UpdateRoadmapPenelitianCommand(newUuid, namaAfter, skorAfter);
                var updateResult = await Sender.Send(updateCommand);

                Assert.True(updateResult.IsSuccess);

                var dataUpdate = DBContext.RoadmapPenelitian.FirstOrDefault(p => p.Uuid == createResult!.Value);
                Assert.NotNull(dataUpdate);
                Assert.Equal(namaAfter, dataUpdate.Nama);
                Assert.Equal(skorAfter, dataUpdate.Skor);

                using (var scope = Factory.Services.CreateScope())
                {
                    var handler = scope.ServiceProvider
                        .GetService<IRequestHandler<UpdateRoadmapPenelitianCommand, Result>>();

                    Assert.NotNull(handler);
                    Assert.IsType<UpdateRoadmapPenelitianCommandHandler>(handler);
                }
            }
            else if (mode == "deleted")
            {
                var deleteCommand = new DeleteRoadmapPenelitianCommand(newUuid);
                var deleteResult = await Sender.Send(deleteCommand);

                Assert.True(deleteResult.IsSuccess);
            }
        }
        [Fact]
        public async Task Create_ShouldThrow_WhenInvalidRuleDomain()
        {
            var nama = "tes";
            var skor = int.MaxValue; // contoh melanggar aturan domain

            var command = new CreateRoadmapPenelitianCommand(nama, skor);
            var result = await Sender.Send(command);

            Assert.True(result.IsFailure);
            Assert.Equal("RoadmapPenelitian.InvalidValueSkor", result.Error.Code);
        }

        [Fact]
        public async Task Update_ShouldThrow_WhenNotExist()
        {
            var guid = Guid.NewGuid().ToString();
            var nama = "tes";
            var skor = 10;

            var command = new UpdateRoadmapPenelitianCommand(guid, nama, skor);
            var result = await Sender.Send(command);

            Assert.True(result.IsFailure);
            Assert.Equal("RoadmapPenelitian.NotFound", result.Error.Code);
        }

        [Fact]
        public async Task Update_ShouldThrow_WhenInvalidRuleDomain()
        {
            // --- CREATE ---
            var namaBefore = "tes";
            var skorBefore = 10;

            var createCommand = new CreateRoadmapPenelitianCommand(namaBefore, skorBefore);
            var createResult = await Sender.Send(createCommand);

            Assert.True(createResult.IsSuccess);
            var dataCreate = DBContext.RoadmapPenelitian.FirstOrDefault(p => p.Uuid == createResult!.Value);

            Assert.NotNull(dataCreate);
            Assert.Equal(namaBefore, dataCreate.Nama);
            Assert.Equal(skorBefore, dataCreate.Skor);

            var newUuid = createResult.Value.ToString();

            // --- UPDATE (melanggar aturan domain)
            var namaAfter = "tes2";
            var skorAfter = int.MaxValue;

            var updateCommand = new UpdateRoadmapPenelitianCommand(newUuid, namaAfter, skorAfter);
            var updateResult = await Sender.Send(updateCommand);

            Assert.True(updateResult.IsFailure);
            Assert.Equal("RoadmapPenelitian.InvalidValueSkor", updateResult.Error.Code);
        }

        [Fact]
        public async Task Delete_ShouldThrow_WhenNotExist()
        {
            var guid = Guid.NewGuid().ToString();

            var command = new DeleteRoadmapPenelitianCommand(guid);
            var result = await Sender.Send(command);

            Assert.True(result.IsFailure);
            Assert.Equal("RoadmapPenelitian.NotFound", result.Error.Code);
        }
    }
}
