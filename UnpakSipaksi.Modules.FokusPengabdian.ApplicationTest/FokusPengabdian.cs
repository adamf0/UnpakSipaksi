using MediatR;
using Microsoft.Extensions.DependencyInjection;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.FokusPengabdian.Application.CreateFokusPengabdian;
using UnpakSipaksi.Modules.FokusPengabdian.Application.DeleteFokusPengabdian;
using UnpakSipaksi.Modules.FokusPengabdian.Application.UpdateFokusPengabdian;
using Xunit;

namespace UnpakSipaksi.Modules.FokusPengabdian.ApplicationTest
{
    public class FokusPengabdianTest : BaseIntegrationTest
    {
        public FokusPengabdianTest(IntegrationTestWebAppFactory factory) : base(factory) { }

        public static IEnumerable<object[]> InvalidData()
        {
            var valid = Guid.NewGuid().ToString();
            var empty = "";

            // CREATE
            yield return new object[] { empty, "", "'Nama' tidak boleh kosong.", "created" };

            // UPDATE
            yield return new object[] { valid, "", "'Nama' tidak boleh kosong.", "updated" };
            yield return new object[] { empty, "tes", "'Uuid' tidak boleh kosong.", "updated" };
            yield return new object[] { "no-guid", "tes", "'Uuid' harus dalam format UUID v4 yang valid.", "updated" };

            // DELETE
            yield return new object[] { empty, "tes", "'Uuid' tidak boleh kosong.", "deleted" };
            yield return new object[] { "no-guid", "tes", "'Uuid' harus dalam format UUID v4 yang valid.", "deleted" };
        }

        public static IEnumerable<object?[]> ValidData()
        {
            yield return new object?[] { new object[] { "tes" }, null, "created" };
            yield return new object?[] { new object[] { "tes" }, new object[] { "tes2" }, "updated" };
            yield return new object?[] { new object[] { "tes" }, null, "deleted" };
        }

        [Theory]
        [MemberData(nameof(InvalidData))]
        public async Task CreateUpdateDelete_ShouldThrow_WhenInvalidFluentValidation(
            string uuid,
            string nama,
            string message,
            string mode)
        {
            Result? result = null;
            if (mode == "created")
            {
                var command = new CreateFokusPengabdianCommand(nama);
                result = await Sender.Send(command);
            }
            else if (mode == "updated")
            {
                var command = new UpdateFokusPengabdianCommand(uuid, nama);
                result = await Sender.Send(command);
            }
            else
            {
                var command = new DeleteFokusPengabdianCommand(uuid);
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

            var createCommand = new CreateFokusPengabdianCommand(namaBefore);
            var createResult = await Sender.Send(createCommand);

            Assert.True(createResult.IsSuccess);
            var dataCreate = DBContext.FokusPengabdian.FirstOrDefault(p => p.Uuid == createResult!.Value);

            Assert.NotNull(dataCreate);
            Assert.Equal(namaBefore, dataCreate.Nama);

            using (var scope = Factory.Services.CreateScope())
            {
                var handler = scope.ServiceProvider
                    .GetService<IRequestHandler<CreateFokusPengabdianCommand, Result<Guid>>>();

                Assert.NotNull(handler);
                Assert.IsType<CreateFokusPengabdianCommandHandler>(handler);
            }

            var newUuid = createResult.Value.ToString();

            // --- UPDATE / DELETE ---
            if (mode == "updated")
            {
                var namaAfter = (string)afterData![0];

                var updateCommand = new UpdateFokusPengabdianCommand(newUuid, namaAfter);
                var updateResult = await Sender.Send(updateCommand);

                Assert.True(updateResult.IsSuccess);
                var dataUpdate = DBContext.FokusPengabdian.FirstOrDefault(p => p.Uuid.ToString() == createResult!.Value.ToString());

                Assert.NotNull(dataUpdate);
                Assert.Equal(namaAfter, dataUpdate.Nama);

                using (var scope = Factory.Services.CreateScope())
                {
                    var handler = scope.ServiceProvider
                        .GetService<IRequestHandler<UpdateFokusPengabdianCommand, Result>>();

                    Assert.NotNull(handler);
                    Assert.IsType<UpdateFokusPengabdianCommandHandler>(handler);
                }
            }
            else if (mode == "deleted")
            {
                var deleteCommand = new DeleteFokusPengabdianCommand(newUuid);
                var deleteResult = await Sender.Send(deleteCommand);

                Assert.True(deleteResult.IsSuccess);
            }
        }

        [Fact]
        public async Task Update_ShouldThrow_WhenNotExist()
        {
            var guid = Guid.NewGuid().ToString();
            var namaBefore = "tes";

            var updateCommand = new UpdateFokusPengabdianCommand(guid, namaBefore);
            var updateResult = await Sender.Send(updateCommand);

            Assert.True(updateResult.IsFailure);
            Assert.Equal("FokusPengabdian.NotFound", updateResult.Error.Code);
        }

        [Fact]
        public async Task Delete_ShouldThrow_WhenNotExist()
        {
            var guid = Guid.NewGuid().ToString();

            var deleteCommand = new DeleteFokusPengabdianCommand(guid);
            var deleteResult = await Sender.Send(deleteCommand);

            Assert.True(deleteResult.IsFailure);
            Assert.Equal("FokusPengabdian.NotFound", deleteResult.Error.Code);
        }
    }
}
