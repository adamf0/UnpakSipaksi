using MediatR;
using Microsoft.Extensions.DependencyInjection;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KategoriProgramPengabdian.Application.CreateKategoriProgramPengabdian;
using UnpakSipaksi.Modules.KategoriProgramPengabdian.Application.UpdateKategoriProgramPengabdian;
using UnpakSipaksi.Modules.KategoriProgramPengabdian.Application.DeleteKategoriProgramPengabdian;
using Xunit;

namespace UnpakSipaksi.Modules.KategoriProgramPengabdian.ApplicationTest
{
    public class KategoriProgramPengabdianTest : BaseIntegrationTest
    {
        public KategoriProgramPengabdianTest(IntegrationTestWebAppFactory factory) : base(factory)
        {
        }

        public static IEnumerable<object[]> InvalidData()
        {
            var validUuid = Guid.NewGuid().ToString();
            var empty = "";

            // CREATE
            yield return new object[] { validUuid, "", "[]", "'Nama' tidak boleh kosong.", "created" };
            yield return new object[] { validUuid, "nama", "", "'Rule' tidak boleh kosong.", "created" };

            // UPDATE
            yield return new object[] { validUuid, "", "[]", "'Nama' tidak boleh kosong.", "updated" };
            yield return new object[] { validUuid, "nama", "", "'Rule' tidak boleh kosong.", "updated" };
            yield return new object[] { empty, "tes", "[]", "'Uuid' tidak boleh kosong.", "updated" };
            yield return new object[] { "no-guid", "tes", "[]", "'Uuid' harus dalam format UUID v4 yang valid.", "updated" };

            // DELETE
            yield return new object[] { empty, "tes", "[]", "'Uuid' tidak boleh kosong.", "deleted" };
            yield return new object[] { "no-guid", "tes", "[]", "'Uuid' harus dalam format UUID v4 yang valid.", "deleted" };
        }

        public static IEnumerable<object?[]> ValidData()
        {
            var validUuid = Guid.NewGuid().ToString();
            yield return new object?[] { new object[] { "tes", "[]" }, null, "created" };
            yield return new object?[] { new object[] { "tes", "[]" }, new object[] { "tes updated", "[]" }, "updated" };
            yield return new object?[] { new object[] { "tes", "[]" }, null, "deleted" };
        }

        [Theory]
        [MemberData(nameof(InvalidData))]
        public async Task CreateUpdateDelete_ShouldThrow_WhenInvalidFluentValidation(
            string uuid,
            string nama,
            string rule,
            string message,
            string mode)
        {
            Result? result = null;

            if (mode == "created")
            {
                var command = new CreateKategoriProgramPengabdianCommand(nama, rule);
                result = await Sender.Send(command);
            }
            else if (mode == "updated")
            {
                var command = new UpdateKategoriProgramPengabdianCommand(uuid, nama, rule);
                result = await Sender.Send(command);
            }
            else
            {
                var command = new DeleteKategoriProgramPengabdianCommand(uuid);
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
            var ruleBefore = (string)beforeData[1];

            // --- CREATE ---
            var createCommand = new CreateKategoriProgramPengabdianCommand(namaBefore, ruleBefore);
            var createResult = await Sender.Send(createCommand);

            Assert.True(createResult.IsSuccess);
            var dataCreate = DBContext.KategoriProgramPengabdian.FirstOrDefault(p => p.Uuid == createResult!.Value);
            Assert.NotNull(dataCreate);
            Assert.Equal(namaBefore, dataCreate.Nama);
            Assert.Equal(ruleBefore, dataCreate.Rule);

            using (var scope = Factory.Services.CreateScope())
            {
                var handler = scope.ServiceProvider
                    .GetService<IRequestHandler<CreateKategoriProgramPengabdianCommand, Result<Guid>>>();
                Assert.NotNull(handler);
                Assert.IsType<CreateKategoriProgramPengabdianCommandHandler>(handler);
            }

            var uuid = createResult.Value.ToString();

            // --- UPDATE ---
            if (mode == "updated")
            {
                var namaAfter = (string)afterData![0];
                var ruleAfter = (string)afterData[1];
                var updateCommand = new UpdateKategoriProgramPengabdianCommand(uuid, namaAfter, ruleAfter);
                var updateResult = await Sender.Send(updateCommand);

                Assert.True(updateResult.IsSuccess);
                var dataUpdate = DBContext.KategoriProgramPengabdian.FirstOrDefault(p => p.Uuid.ToString() == uuid);
                Assert.NotNull(dataUpdate);
                Assert.Equal(namaAfter, dataUpdate.Nama);
                Assert.Equal(ruleAfter, dataUpdate.Rule);

                using (var scope = Factory.Services.CreateScope())
                {
                    var handler = scope.ServiceProvider
                        .GetService<IRequestHandler<UpdateKategoriProgramPengabdianCommand, Result>>();
                    Assert.NotNull(handler);
                    Assert.IsType<UpdateKategoriProgramPengabdianCommandHandler>(handler);
                }
            }

            // --- DELETE ---
            if (mode == "deleted")
            {
                var deleteCommand = new DeleteKategoriProgramPengabdianCommand(uuid);
                var deleteResult = await Sender.Send(deleteCommand);

                Assert.True(deleteResult.IsSuccess);
                var dataDeleted = DBContext.KategoriProgramPengabdian.FirstOrDefault(p => p.Uuid.ToString() == uuid);
                Assert.Null(dataDeleted);

                using (var scope = Factory.Services.CreateScope())
                {
                    var handler = scope.ServiceProvider
                        .GetService<IRequestHandler<DeleteKategoriProgramPengabdianCommand, Result>>();
                    Assert.NotNull(handler);
                    Assert.IsType<DeleteKategoriProgramPengabdianCommandHandler>(handler);
                }
            }
        }
    }
}
