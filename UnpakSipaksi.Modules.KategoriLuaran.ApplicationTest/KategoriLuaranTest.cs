using Docker.DotNet.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System.Reflection;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.Kategori.PublicApi;
using UnpakSipaksi.Modules.KategoriLuaran.Application.Abstractions.Data;
using UnpakSipaksi.Modules.KategoriLuaran.Application.CreateKategoriLuaran;
using UnpakSipaksi.Modules.KategoriLuaran.Application.DeleteKategoriLuaran;
using UnpakSipaksi.Modules.KategoriLuaran.Application.UpdateKategoriLuaran;
using UnpakSipaksi.Modules.KategoriLuaran.ApplicationTest;
using UnpakSipaksi.Modules.KategoriLuaran.Domain.Kategori;
using Xunit;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Application.Integration.Tests
{
    public class KategoriLuaranTest : BaseIntegrationTest
    {
        public KategoriLuaranTest(IntegrationTestWebAppFactory factory) : base(factory)
        {
        }

        public static IEnumerable<object[]> InvalidData()
        {
            var validUuid = Guid.NewGuid().ToString();
            var empty = "";

            // Nama kosong
            yield return new object[] { validUuid, "", "aktif", "'Nama' tidak boleh kosong." };
            // Status kosong
            yield return new object[] { validUuid, "Kategori Tes", "", "'Status' tidak boleh kosong." };
        }

        public static IEnumerable<object?[]> ValidData()
        {
            var uuidKategori = Guid.NewGuid().ToString();

            // create: belum ada data awal, ingin buat baru
            yield return new object?[]
            {
            new object?[] { uuidKategori, "Kategori Tes", "aktif" },
            null,
            "created"
            };

            // update: data lama ada, ingin ubah ke baru
            yield return new object?[]
            {
            new object?[] { uuidKategori, "Kategori Lama", "aktif" },
            new object?[] { uuidKategori, "Kategori Baru", "nonaktif" },
            "updated"
            };

            // delete: data lama ada, ingin hapus
            yield return new object?[]
            {
            new object?[] { uuidKategori, "Kategori Hapus", "aktif" },
            null,
            "deleted"
            };
        }

        [Theory]
        [MemberData(nameof(InvalidData))]
        public async Task Create_ShouldThrow_WhenInvalid(
            string uuidKategori,
            string nama,
            string status,
            string message)
        {
            var command = new CreateKategoriLuaranCommand(uuidKategori, nama, status);
            var result = await Sender.Send(command);

            Assert.True(result.IsFailure);
            var validationError = Assert.IsType<ValidationError>(result.Error);
            Assert.Contains(validationError.Errors, e => e.Description == message);
        }

        [Theory]
        [MemberData(nameof(ValidData))]
        public async Task CreateUpdateDelete_ShouldBeExecute_WhenValidData(object[] initial, object[] updated, string action)
        {
            var uuidKategori = initial[0].ToString();

            var kategoriApiMock = new Mock<IKategoriApi>();
            kategoriApiMock
                .Setup(api => api.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new KategoriResponse("1", uuidKategori!, "Kategori Tes"));

            using var scope = Factory.Services.CreateScope();
            var services = scope.ServiceProvider;

            var handler = new CreateKategoriLuaranCommandHandler(
                    kategoriApiMock.Object,
                    services.GetRequiredService<IKategoriLuaranRepository>(),
                    services.GetRequiredService<IUnitOfWork>()
            );

            var command = new CreateKategoriLuaranCommand(
                uuidKategori!,
                initial[1].ToString()!,
                initial[2].ToString()!
            );

            var result = await handler.Handle(command, CancellationToken.None);
            Assert.True(result.IsSuccess);
            var newUuid = result.Value.ToString();

            if (action == "updated")
            {
                var handlerUpdate = new UpdateKategoriLuaranCommandHandler(
                    kategoriApiMock.Object,
                    services.GetRequiredService<IKategoriLuaranRepository>(),
                    services.GetRequiredService<IUnitOfWork>()
                );

                var commandUpdate = new UpdateKategoriLuaranCommand(
                    newUuid,
                    uuidKategori!,
                    updated[1].ToString()!,
                    updated[2].ToString()!
                );

                var resultUpdate = await handlerUpdate.Handle(commandUpdate, CancellationToken.None);
                Assert.True(resultUpdate.IsSuccess);
            }
            else if (action == "deleted")
            {
                var handlerDelete = new DeleteKategoriLuaranCommandHandler(
                    services.GetRequiredService<IKategoriLuaranRepository>(),
                    services.GetRequiredService<IUnitOfWork>()
                );

                var commandDelete = new DeleteKategoriLuaranCommand(newUuid);

                var resultDelete = await handlerDelete.Handle(commandDelete, CancellationToken.None);
                Assert.True(resultDelete.IsSuccess);
            }
        }


        [Fact]
        public async Task Update_ShouldThrow_WhenNotExist()
        {
            // --- CREATE ---
            var uuidKategori = Guid.NewGuid().ToString();
            var newUuidInvalid = Guid.NewGuid().ToString();

            // Mock IKategoriApi
            var kategoriApiMock = new Mock<IKategoriApi>();
            kategoriApiMock
                .Setup(api => api.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new KategoriResponse("1", uuidKategori, "Kategori Tes"));

            using (var scope = Factory.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                var handler = new CreateKategoriLuaranCommandHandler(
                    kategoriApiMock.Object,
                    services.GetRequiredService<IKategoriLuaranRepository>(),
                    services.GetRequiredService<IUnitOfWork>()
                );

                var command = new CreateKategoriLuaranCommand(
                    uuidKategori,
                    "Kategori Tes",
                    "aktif"
                );

                // Act
                var result = await handler.Handle(command, CancellationToken.None);
                var newUuid = result.Value.ToString();

                // Assert
                Assert.True(result.IsSuccess);

                var data = DBContext.KategoriLuaran.FirstOrDefault(p => p.Uuid == result.Value);
                Assert.NotNull(data);
                Assert.Equal("Kategori Tes", data.Nama);
                Assert.Equal("aktif", data.Status);

                // --- UPDATE ---
                //if (mode == "updated")
                //{
                var handlerUpdate = new UpdateKategoriLuaranCommandHandler(
                    kategoriApiMock.Object,
                    services.GetRequiredService<IKategoriLuaranRepository>(),
                    services.GetRequiredService<IUnitOfWork>()
                );

                var commandUpdate = new UpdateKategoriLuaranCommand(
                    newUuidInvalid,
                    uuidKategori,
                    "Kategori Tes",
                    "aktif"
                );
                var updateResult = await handlerUpdate.Handle(commandUpdate, CancellationToken.None);

                Assert.True(updateResult.IsFailure);
                Assert.Equal("KategoriLuaran.NotFound", updateResult.Error.Code);
                //}
            }
        }

        [Fact]
        public async Task Update_ShouldThrow_WhenInvalidRuleDomain()
        {
            // --- CREATE ---
            var uuidKategori = Guid.NewGuid().ToString();
            var uuidKategoriInvalid = Guid.NewGuid().ToString();

            // Mock IKategoriApi
            var kategoriApiMock = new Mock<IKategoriApi>();
            kategoriApiMock
                .Setup(api => api.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new KategoriResponse("1", uuidKategori, "Kategori Tes"));

            var kategoriApiMockInvalid = new Mock<IKategoriApi>();
            kategoriApiMockInvalid
                .Setup(api => api.GetAsync(It.Is<Guid>(id => id != Guid.Parse(uuidKategori)), It.IsAny<CancellationToken>()))
                .ReturnsAsync((KategoriResponse?)null);

            using (var scope = Factory.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                var handler = new CreateKategoriLuaranCommandHandler(
                    kategoriApiMock.Object,
                    services.GetRequiredService<IKategoriLuaranRepository>(),
                    services.GetRequiredService<IUnitOfWork>()
                );

                var command = new CreateKategoriLuaranCommand(
                    uuidKategori,
                    "Kategori Tes",
                    "aktif"
                );

                // Act
                var result = await handler.Handle(command, CancellationToken.None);
                var newUuid = result.Value.ToString();

                // Assert
                Assert.True(result.IsSuccess);

                var data = DBContext.KategoriLuaran.FirstOrDefault(p => p.Uuid == result.Value);
                Assert.NotNull(data);
                Assert.Equal("Kategori Tes", data.Nama);
                Assert.Equal("aktif", data.Status);

                // --- UPDATE ---
                //if (mode == "updated")
                //{
                var handlerUpdate = new UpdateKategoriLuaranCommandHandler(
                    kategoriApiMockInvalid.Object,
                    services.GetRequiredService<IKategoriLuaranRepository>(),
                    services.GetRequiredService<IUnitOfWork>()
                );

                var commandUpdate = new UpdateKategoriLuaranCommand(
                    newUuid,
                    uuidKategoriInvalid,
                    "Kategori Tes",
                    "aktif"
                );
                var updateResult = await handlerUpdate.Handle(commandUpdate, CancellationToken.None);

                Assert.True(updateResult.IsFailure);
                Assert.Equal("KategoriLuaran.KategoriNotFound", updateResult.Error.Code);
                //}
            }
        }

        [Fact]
        public async Task Delete_ShouldThrow_WhenNotExist()
        {
            var guid = Guid.NewGuid().ToString();

            var deleteCommand = new DeleteKategoriLuaranCommand(guid);
            var deleteResult = await Sender.Send(deleteCommand);

            Assert.True(deleteResult.IsFailure);
            Assert.Equal("KategoriLuaran.NotFound", deleteResult.Error.Code);
        }
    }
}
