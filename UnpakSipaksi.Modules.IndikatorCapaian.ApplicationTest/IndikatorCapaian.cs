using Docker.DotNet.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.IndikatorCapaian.Application.CreateIndikatorCapaian;
using UnpakSipaksi.Modules.IndikatorCapaian.Application.DeleteIndikatorCapaian;
using UnpakSipaksi.Modules.IndikatorCapaian.Application.UpdateIndikatorCapaian;
using UnpakSipaksi.Modules.IndikatorCapaian.ApplicationTest;
using UnpakSipaksi.Modules.IndikatorCapaian.Infrastructure.Database;
using UnpakSipaksi.Modules.JenisLuaran.PublicApi;
using Xunit;

namespace Application.Integration.Tests
{
    public class IndikatorCapaianTest : BaseIntegrationTest
    {
        public IndikatorCapaianTest(IntegrationTestWebAppFactory factory) : base(factory) { }

        public static IEnumerable<object?[]> ValidData()
        {
            yield return new object?[] { new object[] { Guid.NewGuid().ToString(), "Luaran Tes", "aktif" }, null, "created" };
            yield return new object?[] { new object[] { Guid.NewGuid().ToString(), "Luaran Tes", "aktif" }, new object[] { "Luaran Baru", "nonaktif" }, "updated" };
            yield return new object?[] { new object[] { Guid.NewGuid().ToString(), "Luaran Tes", "aktif" }, null, "deleted" };
        }

        public static IEnumerable<object[]> InvalidData()
        {
            var validUuid = Guid.NewGuid().ToString();
            var validJenisLuaranUuid = Guid.NewGuid().ToString();
            var empty = "";
            var guidEmpty = Guid.Empty.ToString();

            yield return new object[] { empty, validJenisLuaranUuid, "", "aktif", "'Nama' tidak boleh kosong.", "created" };
            yield return new object[] { empty, validJenisLuaranUuid, "Luaran Tes", "", "'Status' tidak boleh kosong.", "created" };
            yield return new object[] { empty, guidEmpty, "Luaran Tes", "aktif", "'JenisLuaran' harus dalam format UUID v4 yang valid.", "created" };

            yield return new object[] { "", validJenisLuaranUuid, "Luaran Tes", "aktif", "'Uuid' tidak boleh kosong.", "updated" };
            yield return new object[] { "no-guid", validJenisLuaranUuid, "Luaran Tes", "aktif", "'Uuid' harus dalam format UUID v4 yang valid.", "updated" };
            yield return new object[] { validUuid, validJenisLuaranUuid, "", "aktif", "'Nama' tidak boleh kosong.", "updated" };

            yield return new object[] { "", "", "", "", "'Uuid' tidak boleh kosong.", "deleted" };
            yield return new object[] { "no-guid", "", "", "", "'Uuid' harus dalam format UUID v4 yang valid.", "deleted" };
        }

        // ==============================================================
        // ✅ TEST: Valid Data (Create / Update / Delete)
        // ==============================================================

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

            // --- 1️⃣ Setup mock JenisLuaranApi ---
            var jenisLuaranApiMock = new Mock<IJenisLuaranApi>();
            jenisLuaranApiMock
                .Setup(api => api.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new JenisLuaranResponse("1", jenisLuaranId, "Luaran Tes"));

            // --- 2️⃣ Override DI container agar pakai mock ini ---
            var factoryWithMock = Factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    var descriptor = services.SingleOrDefault(
                        d => d.ServiceType == typeof(IJenisLuaranApi)
                    );
                    if (descriptor != null)
                        services.Remove(descriptor);

                    services.AddSingleton<IJenisLuaranApi>(jenisLuaranApiMock.Object);
                });
            });

            // --- 3️⃣ Ambil scope service baru ---
            await using var scope = factoryWithMock.Services.CreateAsyncScope();
            var sender = scope.ServiceProvider.GetRequiredService<ISender>();
            var dbContext = scope.ServiceProvider.GetRequiredService<IndikatorCapaianDbContext>();

            // --- 4️⃣ CREATE ---
            var createCommand = new CreateIndikatorCapaianCommand(jenisLuaranId, namaBefore, statusBefore);
            var createResult = await sender.Send(createCommand);

            Assert.True(createResult.IsSuccess);
            var dataCreate = dbContext.IndikatorCapaian.FirstOrDefault(p => p.Uuid == createResult!.Value);
            Assert.NotNull(dataCreate);
            Assert.Equal(namaBefore, dataCreate.Nama);
            Assert.Equal(statusBefore, dataCreate.Status);

            var newUuid = createResult.Value.ToString();

            // --- 5️⃣ UPDATE ---
            if (mode == "updated")
            {
                var namaAfter = (string)afterData![0];
                var statusAfter = (string)afterData![1];

                var updateCommand = new UpdateIndikatorCapaianCommand(newUuid, jenisLuaranId, namaAfter, statusAfter);
                var updateResult = await sender.Send(updateCommand);

                Assert.True(updateResult.IsSuccess);

                var dataUpdate = dbContext.IndikatorCapaian.FirstOrDefault(p => p.Uuid.ToString() == newUuid);
                Assert.NotNull(dataUpdate);
                Assert.Equal(namaAfter, dataUpdate.Nama);
                Assert.Equal(statusAfter, dataUpdate.Status);
            }

            // --- 6️⃣ DELETE ---
            if (mode == "deleted")
            {
                var deleteCommand = new DeleteIndikatorCapaianCommand(newUuid);
                var deleteResult = await sender.Send(deleteCommand);

                Assert.True(deleteResult.IsSuccess);

                var deletedData = dbContext.IndikatorCapaian.FirstOrDefault(p => p.Uuid.ToString() == newUuid);
                Assert.Null(deletedData);
            }
        }

        // ==============================================================
        // ❌ TEST: Invalid Data (FluentValidation)
        // ==============================================================

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
                result = await Sender.Send(new CreateIndikatorCapaianCommand(jenisLuaranId, nama, status));
            else if (mode == "updated")
                result = await Sender.Send(new UpdateIndikatorCapaianCommand(uuid, jenisLuaranId, nama, status));
            else
                result = await Sender.Send(new DeleteIndikatorCapaianCommand(uuid));

            Assert.True(result.IsFailure);
            if (result.Error is ValidationError validationError)
                Assert.Contains(validationError.Errors, e => e.Description == message);
            else
                Assert.Equal(message, result.Error.Description);
        }

        // ==============================================================
        // ❌ TEST: Domain Rule Violations
        // ==============================================================

        [Fact]
        public async Task Create_ShouldThrow_WhenInvalidRuleDomain()
        {
            var jenisLuaranApiMock = new Mock<IJenisLuaranApi>();
            var jenisLuaranValid = Guid.NewGuid().ToString();
            var jenisLuaranInvalid = Guid.NewGuid().ToString();

            // Setup mock untuk jenisLuaranValid → return data valid
            jenisLuaranApiMock
                .Setup(api => api.GetAsync(It.Is<Guid>(id => id.ToString() == jenisLuaranValid), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new JenisLuaranResponse("1", jenisLuaranValid, "Luaran Tes"));

            var factoryWithMock = Factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    var descriptor = services.SingleOrDefault(
                        d => d.ServiceType == typeof(IJenisLuaranApi)
                    );
                    if (descriptor != null)
                        services.Remove(descriptor);

                    services.AddSingleton<IJenisLuaranApi>(jenisLuaranApiMock.Object);
                });
            });

            await using var scope = factoryWithMock.Services.CreateAsyncScope();
            var sender = scope.ServiceProvider.GetRequiredService<ISender>();

            var createCommand = new CreateIndikatorCapaianCommand(jenisLuaranInvalid, "Tes Awal", "aktif");
            var createResult = await sender.Send(createCommand);

            Assert.True(createResult.IsFailure);
            Assert.Equal("IndikatorCapaian.UnknownJenisLuaran", createResult.Error.Code);
        }

        [Fact]
        public async Task Update_ShouldThrow_WhenNotExist()
        {
            var updateCommand = new UpdateIndikatorCapaianCommand(
                Guid.NewGuid().ToString(),
                Guid.NewGuid().ToString(),
                "tes",
                "ok"
            );

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

            var factoryWithMock = Factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    var descriptor = services.SingleOrDefault(
                        d => d.ServiceType == typeof(IJenisLuaranApi)
                    );
                    if (descriptor != null)
                        services.Remove(descriptor);

                    services.AddSingleton<IJenisLuaranApi>(jenisLuaranApiMock.Object);
                });
            });

            await using var scope = factoryWithMock.Services.CreateAsyncScope();
            var sender = scope.ServiceProvider.GetRequiredService<ISender>();
            var dbContext = scope.ServiceProvider.GetRequiredService<IndikatorCapaianDbContext>();

            // CREATE valid
            var createCommand = new CreateIndikatorCapaianCommand(jenisLuaranValid, "Tes Awal", "aktif");
            var createResult = await sender.Send(createCommand);
            Assert.True(createResult.IsSuccess);

            var dataCreate = dbContext.IndikatorCapaian.FirstOrDefault(p => p.Uuid == createResult!.Value);
            Assert.NotNull(dataCreate);
            var newUuid = createResult.Value.ToString();

            // UPDATE invalid
            var updateCommand = new UpdateIndikatorCapaianCommand(newUuid, jenisLuaranInvalid, "Tes Ubah", "aktif");
            var updateResult = await sender.Send(updateCommand);

            Assert.True(updateResult.IsFailure);
            Assert.Equal("IndikatorCapaian.UnknownJenisLuaran", updateResult.Error.Code);
        }

        [Fact]
        public async Task Delete_ShouldThrow_WhenNotExist()
        {
            var deleteCommand = new DeleteIndikatorCapaianCommand(Guid.NewGuid().ToString());
            var deleteResult = await Sender.Send(deleteCommand);

            Assert.True(deleteResult.IsFailure);
            Assert.Equal("IndikatorCapaian.NotFound", deleteResult.Error.Code);
        }
    }
}
