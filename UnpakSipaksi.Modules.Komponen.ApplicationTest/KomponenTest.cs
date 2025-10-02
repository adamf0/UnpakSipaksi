using Docker.DotNet.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.Komponen.Application.CreateKomponen;
using UnpakSipaksi.Modules.Komponen.Application.DeleteKomponen;
using UnpakSipaksi.Modules.Komponen.Application.UpdateKomponen;
using UnpakSipaksi.Modules.Komponen.ApplicationTest;
using Xunit;

namespace Application.Integration.Tests
{
    public class KomponenTest : BaseIntegrationTest
    {
        public KomponenTest(IntegrationTestWebAppFactory factory) : base(factory)
        {
        }

        public static IEnumerable<object[]> InvalidData()
        {
            var valid = Guid.NewGuid().ToString();
            var empty = "";

            // CREATE
            yield return new object[] { empty, "", 10, "'Nama' tidak boleh kosong.", "created" };
            yield return new object[] { empty, "Tes", -100, "'MaxBiaya' tidak boleh kurang dari 0.", "created" };

            // UPDATE
            yield return new object[] { valid, "", 10, "'Nama' tidak boleh kosong.", "updated" };
            yield return new object[] { empty, "Tes", 10, "'Uuid' tidak boleh kosong.", "updated" };
            yield return new object[] { "no-guid", "Tes", 10, "'Uuid' harus dalam format UUID v4 yang valid.", "updated" };

            // DELETE
            yield return new object[] { empty, "Tes", 10, "'Uuid' tidak boleh kosong.", "deleted" };
            yield return new object[] { "no-guid", "Tes", 10, "'Uuid' harus dalam format UUID v4 yang valid.", "deleted" };
        }

        public static IEnumerable<object?[]> ValidData()
        {
            yield return new object?[] { new object[] { "Tes", 1000 }, null, "created" };
            yield return new object?[] { new object[] { "Tes", 1000 }, new object[] { "Tes Updated", 2000 }, "updated" };
            yield return new object?[] { new object[] { "Tes", 1000 }, null, "deleted" };
        }

        [Theory]
        [MemberData(nameof(InvalidData))]
        public async Task CreateUpdateDelete_ShouldThrow_WhenInvalid(
            string uuid,
            string nama,
            int maxBiaya,
            string message,
            string mode)
        {
            Result? result = null;

            if (mode == "created")
            {
                var command = new CreateKomponenCommand(nama, maxBiaya);
                result = await Sender.Send(command);
            }
            else if (mode == "updated")
            {
                var command = new UpdateKomponenCommand(uuid, nama, maxBiaya);
                result = await Sender.Send(command);
            }
            else
            {
                var command = new DeleteKomponenCommand(uuid);
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
        public async Task CreateUpdateDelete_ShouldBeExecute_WhenValid(
            object[] beforeData,
            object[]? afterData,
            string mode)
        {
            var namaBefore = (string)beforeData[0];
            var maxBiayaBefore = (int)beforeData[1];

            // --- CREATE ---
            var createCommand = new CreateKomponenCommand(namaBefore, maxBiayaBefore);
            var createResult = await Sender.Send(createCommand);

            Assert.True(createResult.IsSuccess);
            var dataCreate = DBContext.Komponen.FirstOrDefault(p => p.Uuid == createResult!.Value);

            Assert.NotNull(dataCreate);
            Assert.Equal(namaBefore, dataCreate.Nama);
            Assert.Equal(maxBiayaBefore, dataCreate.MaxBiaya);

            using (var scope = Factory.Services.CreateScope())
            {
                var handler = scope.ServiceProvider
                    .GetService<IRequestHandler<CreateKomponenCommand, Result<Guid>>>();

                Assert.NotNull(handler);
                Assert.IsType<CreateKomponenCommandHandler>(handler);
            }

            var newUuid = createResult.Value.ToString();

            // --- UPDATE / DELETE ---
            if (mode == "updated")
            {
                var namaAfter = (string)afterData![0];
                var maxBiayaAfter = (int)afterData[1];

                var updateCommand = new UpdateKomponenCommand(newUuid, namaAfter, maxBiayaAfter);
                var updateResult = await Sender.Send(updateCommand);

                Assert.True(updateResult.IsSuccess);
                var dataUpdate = DBContext.Komponen.FirstOrDefault(p => p.Uuid == createResult!.Value);

                Assert.NotNull(dataUpdate);
                Assert.Equal(namaAfter, dataUpdate.Nama);
                Assert.Equal(maxBiayaAfter, dataUpdate.MaxBiaya);

                using (var scope = Factory.Services.CreateScope())
                {
                    var handler = scope.ServiceProvider
                        .GetService<IRequestHandler<UpdateKomponenCommand, Result>>();

                    Assert.NotNull(handler);
                    Assert.IsType<UpdateKomponenCommandHandler>(handler);
                }
            }
            else if (mode == "deleted")
            {
                var deleteCommand = new DeleteKomponenCommand(newUuid);
                var deleteResult = await Sender.Send(deleteCommand);

                Assert.True(deleteResult.IsSuccess);

                using (var scope = Factory.Services.CreateScope())
                {
                    var handler = scope.ServiceProvider
                        .GetService<IRequestHandler<DeleteKomponenCommand, Result>>();

                    Assert.NotNull(handler);
                    Assert.IsType<DeleteKomponenCommandHandler>(handler);
                }
            }
        }
    }
}
