using Docker.DotNet.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System.Reflection;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.IndikatorCapaian.Application.Abstractions.Data;
using UnpakSipaksi.Modules.IndikatorCapaian.Application.CreateIndikatorCapaian;
using UnpakSipaksi.Modules.IndikatorCapaian.Application.DeleteIndikatorCapaian;
using UnpakSipaksi.Modules.IndikatorCapaian.Application.UpdateIndikatorCapaian;
using UnpakSipaksi.Modules.IndikatorCapaian.Domain;
using UnpakSipaksi.Modules.JenisLuaran.PublicApi;
using Xunit;

namespace UnpakSipaksi.Modules.IndikatorCapaian.ApplicationTest
{
    public class IndikatorCapaianTest : BaseIntegrationTest
    {
        public IndikatorCapaianTest(IntegrationTestWebAppFactory factory) : base(factory) { }

        public static IEnumerable<object[]> InvalidData()
        {
            var validUuid = Guid.NewGuid().ToString();
            var empty = "";
            var invalidGuid = "no-guid";

            // CREATE invalid (mengacu pada UuidJenisLuaran, Nama, Status)
            yield return new object[] { empty, validUuid, "Luaran Tes", "aktif", "'JenisLuaran' tidak boleh kosong.", "created" };
            yield return new object[] { invalidGuid, validUuid, "Luaran Tes", "aktif", "'JenisLuaran' harus dalam format UUID v4 yang valid.", "created" };
            yield return new object[] { validUuid, validUuid, "", "aktif", "'Nama' tidak boleh kosong.", "created" };
            yield return new object[] { validUuid, validUuid, "Luaran Tes", "", "'Status' tidak boleh kosong.", "created" };

            // UPDATE invalid (mengacu pada Uuid, UuidJenisLuaran, Nama, Status)
            yield return new object[] { empty, validUuid, "Luaran Tes", "aktif", "'Uuid' tidak boleh kosong.", "updated" };
            yield return new object[] { invalidGuid, validUuid, "Luaran Tes", "aktif", "'Uuid' harus dalam format UUID v4 yang valid.", "updated" };
            yield return new object[] { validUuid, empty, "Luaran Tes", "aktif", "'JenisLuaran' tidak boleh kosong.", "updated" };
            yield return new object[] { validUuid, invalidGuid, "Luaran Tes", "aktif", "'JenisLuaran' harus dalam format UUID v4 yang valid.", "updated" };
            yield return new object[] { validUuid, validUuid, "", "aktif", "'Nama' tidak boleh kosong.", "updated" };
            yield return new object[] { validUuid, validUuid, "Luaran Tes", "", "'Status' tidak boleh kosong.", "updated" };

            // DELETE invalid (hanya mengacu pada Uuid)
            yield return new object[] { empty, "", "", "aktif", "'Uuid' tidak boleh kosong.", "deleted" };
            yield return new object[] { invalidGuid, "", "", "aktif", "'Uuid' harus dalam format UUID v4 yang valid.", "deleted" };
        }


        public static IEnumerable<object?[]> ValidData()
        {
            var jenisLuaranId = Guid.NewGuid().ToString();
            // create: belum ada data awal, ingin buat baru
            yield return new object?[]
            {
            new object[] { jenisLuaranId, "Luaran Tes", "aktif" },
            null,
            "created"
            };

            // update: data lama ada, ingin ubah ke baru
            yield return new object?[]
            {
            new object[] { jenisLuaranId, "Luaran Tes", "aktif"},
            new object[] { jenisLuaranId, "Luaran Tes", "non-aktif"},
            "updated"
            };

            // delete: data lama ada, ingin hapus
            yield return new object?[]
            {
            new object[] { jenisLuaranId, "Luaran Tes", "aktif" },
            null,
            "deleted"
            };
        }

        //[Theory]
        //[MemberData(nameof(InvalidData))]
        //public async Task CreateUpdateDelete_ShouldThrow_WhenInvalid(
        //    string uuid,
        //    string jenisLuaranId,
        //    string nama,
        //    string status,
        //    string message,
        //    string mode)
        //{
        //    Result? result = null;

        //    using var scope = Factory.Services.CreateScope();
        //    var services = scope.ServiceProvider;

        //    var jenisLuaranApiMock = new Mock<IJenisLuaranApi>();
        //    jenisLuaranApiMock
        //        .Setup(api => api.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
        //        .ReturnsAsync(new JenisLuaranResponse("1", jenisLuaranId, "Luaran Tes"));

        //    switch (mode)
        //    {
        //        case "created":
        //            var createHandler = new CreateIndikatorCapaianCommandHandler(
        //                jenisLuaranApiMock.Object,
        //                services.GetRequiredService<IIndikatorCapaianRepository>(),
        //                services.GetRequiredService<IUnitOfWork>()
        //            );
        //            var createCommand = new CreateIndikatorCapaianCommand(jenisLuaranId, nama, status);
        //            result = await Sender.Send(createCommand);
        //            break;

        //        case "updated":
        //            var updateHandler = new UpdateIndikatorCapaianCommandHandler(
        //                jenisLuaranApiMock.Object,
        //                services.GetRequiredService<IIndikatorCapaianRepository>(),
        //                services.GetRequiredService<IUnitOfWork>()
        //            );
        //            var updateCommand = new UpdateIndikatorCapaianCommand(uuid, jenisLuaranId, nama, status);
        //            result = await updateHandler.Handle(updateCommand, CancellationToken.None);
        //            break;

        //        case "deleted":
        //            var deleteHandler = new DeleteIndikatorCapaianCommandHandler(
        //                services.GetRequiredService<IIndikatorCapaianRepository>(),
        //                services.GetRequiredService<IUnitOfWork>()
        //            );
        //            var deleteCommand = new DeleteIndikatorCapaianCommand(uuid);
        //            result = await deleteHandler.Handle(deleteCommand, CancellationToken.None);
        //            break;
        //    }

        //    Assert.True(result!.IsFailure);
        //    if (result.Error is ValidationError validationError)
        //    {
        //        Assert.Contains(validationError.Errors, e => e.Description == message);
        //    }
        //    else
        //    {
        //        Assert.Equal(message, result.Error.Description);
        //    }
        //}

        [Theory]
        [MemberData(nameof(ValidData))]
        public async Task CreateUpdateDelete_ShouldExecute_WhenValid(object[] initial, object?[] updated, string action)
        {
            using var scope = Factory.Services.CreateScope();
            var services = scope.ServiceProvider;

            var jenisLuaranApiMock = new Mock<IJenisLuaranApi>();
            jenisLuaranApiMock
                .Setup(api => api.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new JenisLuaranResponse("1", initial[0]!.ToString()!, "Luaran Tes"));

            var createHandler = new CreateIndikatorCapaianCommandHandler(
                jenisLuaranApiMock.Object,
                services.GetRequiredService<IIndikatorCapaianRepository>(),
                services.GetRequiredService<IUnitOfWork>()
            );

            var createCommand = new CreateIndikatorCapaianCommand(
                initial[0]!.ToString()!,
                initial[1]!.ToString()!,
                initial[2]!.ToString()!
            );

            var createResult = await createHandler.Handle(createCommand, CancellationToken.None);
            Assert.True(createResult.IsSuccess);
            var newUuid = createResult.Value.ToString();

            switch (action)
            {
                case "updated":
                    var updateHandler = new UpdateIndikatorCapaianCommandHandler(
                        jenisLuaranApiMock.Object,
                        services.GetRequiredService<IIndikatorCapaianRepository>(),
                        services.GetRequiredService<IUnitOfWork>()
                    );

                    var updateCommand = new UpdateIndikatorCapaianCommand(
                        newUuid,
                        updated![0]!.ToString()!,
                        updated![1]!.ToString()!,
                        updated![2]!.ToString()!
                    );

                    var updateResult = await updateHandler.Handle(updateCommand, CancellationToken.None);
                    Assert.True(updateResult.IsSuccess);
                    break;

                case "deleted":
                    var deleteHandler = new DeleteIndikatorCapaianCommandHandler(
                        services.GetRequiredService<IIndikatorCapaianRepository>(),
                        services.GetRequiredService<IUnitOfWork>()
                    );

                    var deleteCommand = new DeleteIndikatorCapaianCommand(newUuid);
                    var deleteResult = await deleteHandler.Handle(deleteCommand, CancellationToken.None);
                    Assert.True(deleteResult.IsSuccess);
                    break;
            }
        }

        [Fact]
        public async Task Update_ShouldThrow_WhenNotExist()
        {
            var jenisLuaranId = Guid.NewGuid().ToString();
            var invalidUuid = Guid.NewGuid().ToString();

            var jenisLuaranApiMock = new Mock<IJenisLuaranApi>();
            jenisLuaranApiMock
                .Setup(api => api.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new JenisLuaranResponse("1", jenisLuaranId, "Luaran Tes"));

            using var scope = Factory.Services.CreateScope();
            var services = scope.ServiceProvider;

            // Coba update uuid yang tidak ada
            var updateHandler = new UpdateIndikatorCapaianCommandHandler(
                jenisLuaranApiMock.Object,
                services.GetRequiredService<IIndikatorCapaianRepository>(),
                services.GetRequiredService<IUnitOfWork>()
            );

            var commandUpdate = new UpdateIndikatorCapaianCommand(
                invalidUuid,
                jenisLuaranId,
                "Luaran Tes",
                "aktif"
            );

            var updateResult = await updateHandler.Handle(commandUpdate, CancellationToken.None);

            Assert.True(updateResult.IsFailure);
            Assert.Equal("IndikatorCapaian.NotFound", updateResult.Error.Code);
        }

        [Fact]
        public async Task Update_ShouldThrow_WhenInvalidRuleDomain()
        {
            var jenisLuaranId = Guid.NewGuid().ToString();
            var invalidJenisLuaranId = Guid.NewGuid().ToString();

            // Mock valid
            var jenisLuaranApiMockValid = new Mock<IJenisLuaranApi>();
            jenisLuaranApiMockValid
                .Setup(api => api.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new JenisLuaranResponse("1", jenisLuaranId, "Luaran Tes"));

            // Mock invalid (foreign key tidak ada)
            var jenisLuaranApiMockInvalid = new Mock<IJenisLuaranApi>();
            jenisLuaranApiMockInvalid
                .Setup(api => api.GetAsync(It.Is<Guid>(id => id != Guid.Parse(jenisLuaranId)), It.IsAny<CancellationToken>()))
                .ReturnsAsync((JenisLuaranResponse?)null);

            using var scope = Factory.Services.CreateScope();
            var services = scope.ServiceProvider;

            // Buat record awal
            var createHandler = new CreateIndikatorCapaianCommandHandler(
                jenisLuaranApiMockValid.Object,
                services.GetRequiredService<IIndikatorCapaianRepository>(),
                services.GetRequiredService<IUnitOfWork>()
            );

            var createCommand = new CreateIndikatorCapaianCommand(jenisLuaranId, "Luaran Tes", "aktif");
            var createResult = await createHandler.Handle(createCommand, CancellationToken.None);
            Assert.True(createResult.IsSuccess);

            var newUuid = createResult.Value.ToString();

            // Update ke jenisLuaranId yang tidak valid
            var updateHandler = new UpdateIndikatorCapaianCommandHandler(
                jenisLuaranApiMockInvalid.Object,
                services.GetRequiredService<IIndikatorCapaianRepository>(),
                services.GetRequiredService<IUnitOfWork>()
            );

            var commandUpdate = new UpdateIndikatorCapaianCommand(
                newUuid,
                invalidJenisLuaranId,
                "Luaran Tes",
                "aktif"
            );

            var updateResult = await updateHandler.Handle(commandUpdate, CancellationToken.None);
            Assert.True(updateResult.IsFailure);
            Assert.Equal("IndikatorCapaian.UnknownJenisLuaran", updateResult.Error.Code);
        }

        [Fact]
        public async Task Delete_ShouldThrow_WhenNotExist()
        {
            var invalidUuid = Guid.NewGuid().ToString();

            using var scope = Factory.Services.CreateScope();
            var services = scope.ServiceProvider;

            var deleteHandler = new DeleteIndikatorCapaianCommandHandler(
                services.GetRequiredService<IIndikatorCapaianRepository>(),
                services.GetRequiredService<IUnitOfWork>()
            );

            var deleteCommand = new DeleteIndikatorCapaianCommand(invalidUuid);
            var deleteResult = await deleteHandler.Handle(deleteCommand, CancellationToken.None);

            Assert.True(deleteResult.IsFailure);
            Assert.Equal("IndikatorCapaian.NotFound", deleteResult.Error.Code);
        }

    }
}
