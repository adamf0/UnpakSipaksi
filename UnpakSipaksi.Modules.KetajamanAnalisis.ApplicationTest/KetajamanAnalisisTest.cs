using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KetajamanAnalisis.Application.CreateKetajamanAnalisis;
using UnpakSipaksi.Modules.KetajamanAnalisis.Application.DeleteKetajamanAnalisis;
using UnpakSipaksi.Modules.KetajamanAnalisis.Application.UpdateKetajamanAnalisis;
using Xunit;

namespace UnpakSipaksi.Modules.KetajamanAnalisis.ApplicationTest
{
    public class KetajamanAnalisisTest : BaseIntegrationTest
    {
        public KetajamanAnalisisTest(IntegrationTestWebAppFactory factory) : base(factory)
        {
        }

        public static IEnumerable<object[]> InvalidData()
        {
            var validUuid = Guid.NewGuid().ToString();
            var empty = "";

            // CREATE
            yield return new object[] { validUuid, "", 10, "'Nama' tidak boleh kosong.", "created" };
            yield return new object[] { validUuid, "Tes", -10, "'Nilai' tidak boleh negative.", "created" };

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
        public async Task CreateUpdateDelete_ShouldThrow_WhenInvalidFluentValidation(
            string uuid,
            string nama,
            int nilai,
            string message,
            string mode)
        {
            Result? result = null;

            if (mode == "created")
            {
                var command = new CreateKetajamanAnalisisCommand(nama, nilai);
                result = await Sender.Send(command);
            }
            else if (mode == "updated")
            {
                var command = new UpdateKetajamanAnalisisCommand(uuid, nama, nilai);
                result = await Sender.Send(command);
            }
            else if (mode == "deleted")
            {
                var command = new DeleteKetajamanAnalisisCommand(uuid);
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
            var nilaiBefore = (int)beforeData[2];

            // CREATE
            var createCommand = new CreateKetajamanAnalisisCommand(namaBefore, nilaiBefore);
            var createResult = await Sender.Send(createCommand);

            Assert.True(createResult.IsSuccess);
            var dataCreate = DBContext.KetajamanAnalisis.FirstOrDefault(p => p.Uuid == createResult!.Value);

            Assert.NotNull(dataCreate);
            Assert.Equal(namaBefore, dataCreate.Nama);
            Assert.Equal(nilaiBefore, dataCreate.Nilai);

            using (var scope = Factory.Services.CreateScope())
            {
                var handler = scope.ServiceProvider
                    .GetService<IRequestHandler<CreateKetajamanAnalisisCommand, Result<Guid>>>();

                Assert.NotNull(handler);
                Assert.IsType<CreateKetajamanAnalisisCommandHandler>(handler);
            }

            var newUuid = createResult.Value.ToString();

            // UPDATE / DELETE
            if (mode == "updated")
            {
                var namaAfter = (string)afterData![0];
                var nilaiAfter = (int)afterData[1];

                var updateCommand = new UpdateKetajamanAnalisisCommand(newUuid, namaAfter, nilaiAfter);
                var updateResult = await Sender.Send(updateCommand);

                Assert.True(updateResult.IsSuccess);
                var dataUpdate = DBContext.KetajamanAnalisis.FirstOrDefault(p => p.Uuid == createResult!.Value);

                Assert.NotNull(dataUpdate);
                Assert.Equal(namaAfter, dataUpdate.Nama);
                Assert.Equal(nilaiAfter, dataUpdate.Nilai);

                using (var scope = Factory.Services.CreateScope())
                {
                    var handler = scope.ServiceProvider
                        .GetService<IRequestHandler<UpdateKetajamanAnalisisCommand, Result>>();

                    Assert.NotNull(handler);
                    Assert.IsType<UpdateKetajamanAnalisisCommandHandler>(handler);
                }
            }
            else if (mode == "deleted")
            {
                var deleteCommand = new DeleteKetajamanAnalisisCommand(newUuid);
                var deleteResult = await Sender.Send(deleteCommand);

                Assert.True(deleteResult.IsSuccess);
            }
        }
    }
}
