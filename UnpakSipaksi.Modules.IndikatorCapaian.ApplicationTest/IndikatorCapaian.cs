using Docker.DotNet.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System.Reflection;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.IndikatorCapaian.Application.CreateIndikatorCapaian;
using UnpakSipaksi.Modules.IndikatorCapaian.Application.DeleteIndikatorCapaian;
using UnpakSipaksi.Modules.IndikatorCapaian.Application.UpdateIndikatorCapaian;
using UnpakSipaksi.Modules.IndikatorCapaian.ApplicationTest;
using UnpakSipaksi.Modules.JenisLuaran.PublicApi;
using Xunit;

namespace Application.Integration.Tests
{
    public class IndikatorCapaianTest : BaseIntegrationTest
    {
        public IndikatorCapaianTest(IntegrationTestWebAppFactory factory) : base(factory) { }

        public static IEnumerable<object[]> InvalidData()
        {
            var validUuid = Guid.NewGuid().ToString();
            var validJenisLuaranUuid = Guid.NewGuid().ToString();
            var empty = "";
            var guidEmpty = Guid.Empty.ToString();

            // CREATE invalid
            yield return new object[] { empty, validJenisLuaranUuid, "", "aktif", "'Nama' tidak boleh kosong.", "created" };
            yield return new object[] { empty, validJenisLuaranUuid, "Luaran Tes", "", "'Status' tidak boleh kosong.", "created" };
            yield return new object[] { empty, guidEmpty, "Luaran Tes", "aktif", "'JenisLuaran' harus dalam format UUID v4 yang valid.", "created" };

            // UPDATE invalid
            yield return new object[] { "", validJenisLuaranUuid, "Luaran Tes", "aktif", "'Uuid' tidak boleh kosong.", "updated" };
            yield return new object[] { "no-guid", validJenisLuaranUuid, "Luaran Tes", "aktif", "'Uuid' harus dalam format UUID v4 yang valid.", "updated" };
            yield return new object[] { validUuid, validJenisLuaranUuid, "", "aktif", "'Nama' tidak boleh kosong.", "updated" };

            // DELETE invalid
            yield return new object[] { "", "", "", "", "'Uuid' tidak boleh kosong.", "deleted" };
            yield return new object[] { "no-guid", "", "", "", "'Uuid' harus dalam format UUID v4 yang valid.", "deleted" };
        }

        public static IEnumerable<object?[]> ValidData()
        {
            yield return new object?[] { new object[] { Guid.NewGuid().ToString(), "Luaran Tes", "aktif" }, null, "created" };
            yield return new object?[] { new object[] { Guid.NewGuid().ToString(), "Luaran Tes", "aktif" }, new object[] { "Luaran Baru", "nonaktif" }, "updated" };
            yield return new object?[] { new object[] { Guid.NewGuid().ToString(), "Luaran Tes", "aktif" }, null, "deleted" };
        }

        [Theory]
        [MemberData(nameof(InvalidData))]
        public async Task CreateUpdateDelete_ShouldThrow_WhenInvalidFluentValidation(
            string uuid,
            string jenisLuaranId,
            string nama,
            string status,
            string message,
            string mode)
        {
            Result? result = null;

            if (mode == "created")
            {
                var command = new CreateIndikatorCapaianCommand(jenisLuaranId, nama, status);
                result = await Sender.Send(command);
            }
            else if (mode == "updated")
            {
                var command = new UpdateIndikatorCapaianCommand(uuid, jenisLuaranId, nama, status);
                result = await Sender.Send(command);
            }
            else
            {
                var command = new DeleteIndikatorCapaianCommand(uuid);
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
            var jenisLuaranId = (string)beforeData[0];
            var namaBefore = (string)beforeData[1];
            var statusBefore = (string)beforeData[2];

            // --- Mock JenisLuaranApi ---
            var jenisLuaranApiMock = new Mock<IJenisLuaranApi>();
            jenisLuaranApiMock
                .Setup(api => api.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new JenisLuaranResponse("1", jenisLuaranId, "Luaran Tes"));

            // --- CREATE ---
            var createCommand = new CreateIndikatorCapaianCommand(jenisLuaranId, namaBefore, statusBefore);
            var createResult = await Sender.Send(createCommand);

            Assert.True(createResult.IsSuccess);
            var dataCreate = DBContext.IndikatorCapaian.FirstOrDefault(p => p.Uuid == createResult!.Value);
            Assert.NotNull(dataCreate);
            Assert.Equal(namaBefore, dataCreate.Nama);
            Assert.Equal(statusBefore, dataCreate.Status);

            using (var scope = Factory.Services.CreateScope())
            {
                var handler = scope.ServiceProvider.GetService<IRequestHandler<CreateIndikatorCapaianCommand, Result<Guid>>>();
                Assert.NotNull(handler);
                Assert.IsType<CreateIndikatorCapaianCommandHandler>(handler);
            }

            var newUuid = createResult.Value.ToString();

            // --- UPDATE / DELETE ---
            if (mode == "updated")
            {
                var namaAfter = (string)afterData![0];
                var statusAfter = (string)afterData![1];

                var updateCommand = new UpdateIndikatorCapaianCommand(newUuid, jenisLuaranId, namaAfter, statusAfter);
                var updateResult = await Sender.Send(updateCommand);

                Assert.True(updateResult.IsSuccess);

                var dataUpdate = DBContext.IndikatorCapaian.FirstOrDefault(p => p.Uuid.ToString() == newUuid);
                Assert.NotNull(dataUpdate);
                Assert.Equal(namaAfter, dataUpdate.Nama);
                Assert.Equal(statusAfter, dataUpdate.Status);

                using (var scope = Factory.Services.CreateScope())
                {
                    var handler = scope.ServiceProvider.GetService<IRequestHandler<UpdateIndikatorCapaianCommand, Result>>();
                    Assert.NotNull(handler);
                    Assert.IsType<UpdateIndikatorCapaianCommandHandler>(handler);
                }
            }
            else if (mode == "deleted")
            {
                var deleteCommand = new DeleteIndikatorCapaianCommand(newUuid);
                var deleteResult = await Sender.Send(deleteCommand);

                Assert.True(deleteResult.IsSuccess);

                var deletedData = DBContext.IndikatorCapaian.FirstOrDefault(p => p.Uuid.ToString() == newUuid);
                Assert.Null(deletedData);

                using (var scope = Factory.Services.CreateScope())
                {
                    var handler = scope.ServiceProvider.GetService<IRequestHandler<DeleteIndikatorCapaianCommand, Result>>();
                    Assert.NotNull(handler);
                    Assert.IsType<DeleteIndikatorCapaianCommandHandler>(handler);
                }
            }
        }

        [Fact]
        public async Task Create_ShouldThrow_WhenInvalidRuleDomain()
        {
            var uuidJenisLuaranBefore = Guid.NewGuid().ToString();
            var namaBefore = "tes";
            var statusBefore = "ok";

            var createCommand = new CreateIndikatorCapaianCommand(uuidJenisLuaranBefore, namaBefore, statusBefore);
            var createResult = await Sender.Send(createCommand);

            Assert.True(createResult.IsFailure);
            Assert.Equal("IndikatorCapaian.UnknownJenisLuaran", createResult.Error.Code);
        }

        [Fact]
        public async Task Update_ShouldThrow_WhenNotExist()
        {
            var guid = Guid.NewGuid().ToString();
            var uuidJenisLuaranBefore = Guid.NewGuid().ToString();
            var namaBefore = "tes";
            var statusBefore = "ok";

            var updateCommand = new UpdateIndikatorCapaianCommand(guid, uuidJenisLuaranBefore, namaBefore, statusBefore);
            var updateResult = await Sender.Send(updateCommand);

            Assert.True(updateResult.IsFailure);
            Assert.Equal("IndikatorCapaian.NotFound", updateResult.Error.Code);
        }

        [Fact]
        public async Task Update_ShouldThrow_WhenInvalidRuleDomain()
        {
            var jenisLuaranApiMock = new Mock<IJenisLuaranApi>();
            var jenisLuaranValid = Guid.NewGuid().ToString();
            var jenisLuaranInvalid = Guid.NewGuid().ToString();

            jenisLuaranApiMock
                .Setup(api => api.GetAsync(It.Is<Guid>(id => id.ToString() == jenisLuaranValid), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new JenisLuaranResponse("1", jenisLuaranValid, "Luaran Tes"));

            jenisLuaranApiMock
                .Setup(api => api.GetAsync(It.Is<Guid>(id => id.ToString() == jenisLuaranInvalid), It.IsAny<CancellationToken>()))
                .ReturnsAsync((JenisLuaranResponse?)null);

            // --- CREATE ---
            var namaBefore = "Tes Awal";
            var statusBefore = "aktif";

            var createCommand = new CreateIndikatorCapaianCommand(jenisLuaranValid, namaBefore, statusBefore);
            var createResult = await Sender.Send(createCommand);

            Assert.True(createResult.IsSuccess);

            var dataCreate = DBContext.IndikatorCapaian.FirstOrDefault(p => p.Uuid == createResult!.Value);
            Assert.NotNull(dataCreate);
            var newUuid = createResult.Value.ToString();

            // --- UPDATE ---
            var namaAfter = "Tes Ubah";
            var statusAfter = "aktif";

            var updateCommand = new UpdateIndikatorCapaianCommand(newUuid, jenisLuaranInvalid, namaAfter, statusAfter);
            var updateResult = await Sender.Send(updateCommand);

            Assert.True(updateResult.IsFailure);
            Assert.Equal("IndikatorCapaian.UnknownJenisLuaran", updateResult.Error.Code);
        }

        [Fact]
        public async Task Delete_ShouldThrow_WhenNotExist()
        {
            var guid = Guid.NewGuid().ToString();

            var deleteCommand = new DeleteIndikatorCapaianCommand(guid);
            var deleteResult = await Sender.Send(deleteCommand);

            Assert.True(deleteResult.IsFailure);
            Assert.Equal("IndikatorCapaian.NotFound", deleteResult.Error.Code);
        }
    }
}
