using Dapper;
using Docker.DotNet.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Moq;
using Moq.Dapper;
using System.Data;
using System.Data.Common;
using System.Reflection;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.Kategori.PublicApi;
using UnpakSipaksi.Modules.KategoriLuaran.Application.Abstractions.Data;
using UnpakSipaksi.Modules.KategoriLuaran.Application.CreateKategoriLuaran;
using UnpakSipaksi.Modules.KategoriLuaran.Application.DeleteKategoriLuaran;
using UnpakSipaksi.Modules.KategoriLuaran.Application.GetAllKategoriLuaran;
using UnpakSipaksi.Modules.KategoriLuaran.Application.GetKategoriLuaran;
using UnpakSipaksi.Modules.KategoriLuaran.Application.UpdateKategoriLuaran;
using UnpakSipaksi.Modules.KategoriLuaran.ApplicationTest;
using UnpakSipaksi.Modules.KategoriLuaran.Domain.Kategori;
using Xunit;

namespace Application.Integration.Tests
{
    public class KategoriLuaranTest : BaseIntegrationTest
    {
        public KategoriLuaranTest(IntegrationTestWebAppFactory factory) : base(factory)
        {
        }

        public static IEnumerable<object[]> InvalidDataKategoriLuaran()
        {
            var validUuid = Guid.NewGuid().ToString();
            var empty = "";

            // CREATE scenarios
            yield return new object[] { validUuid, empty, "Kategori Tes", "active", "'Kategori' tidak boleh kosong.", "created" };
            yield return new object[] { validUuid, "invalid-guid", "Kategori Tes", "active", "'Kategori' harus dalam format UUID v4 yang valid.", "created" };
            yield return new object[] { validUuid, validUuid, "", "active", "'Nama' tidak boleh kosong.", "created" };
            yield return new object[] { validUuid, validUuid, "Kategori Tes", "", "'Status' tidak boleh kosong.", "created" };

            // UPDATE scenarios
            yield return new object[] { empty, validUuid, "Kategori Tes", "active", "'Uuid' tidak boleh kosong.", "updated" };
            yield return new object[] { "invalid-guid", validUuid, "Kategori Tes", "active", "'Uuid' harus dalam format UUID v4 yang valid.", "updated" };
            yield return new object[] { validUuid, empty, "Kategori Tes", "active", "'Kategori' tidak boleh kosong.", "updated" };
            yield return new object[] { validUuid, "invalid-guid", "Kategori Tes", "active", "'Kategori' harus dalam format UUID v4 yang valid.", "updated" };
            yield return new object[] { validUuid, validUuid, "", "active", "'Nama' tidak boleh kosong.", "updated" };
            yield return new object[] { validUuid, validUuid, "Kategori Tes", "", "'Status' tidak boleh kosong.", "updated" };

            // DELETE scenarios (hanya Uuid)
            yield return new object[] { empty, "", "", "active", "'Uuid' tidak boleh kosong.", "deleted" };
            yield return new object[] { "invalid-guid", "", "", "active", "'Uuid' harus dalam format UUID v4 yang valid.", "deleted" };
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
            new object[] { uuidKategori, "Kategori Lama", "aktif" },
            new object[] { uuidKategori, "Kategori Baru", "nonaktif" },
            "updated"
            };

            // delete: data lama ada, ingin hapus
            yield return new object?[]
            {
            new object[] { uuidKategori, "Kategori Hapus", "aktif" },
            null,
            "deleted"
            };
        }

        //[Theory]
        //[MemberData(nameof(InvalidDataKategoriLuaran))]
        //public async Task CreateUpdateDeleteKategoriLuaran_ShouldThrow_WhenInvalid(
        //    string uuid,
        //    string uuidKategori,
        //    string nama,
        //    string status,
        //    string message,
        //    string mode
        //)
        //{
        //    // === Arrange ===
        //    var kategoriApiMock = new Mock<IKategoriApi>();
        //    kategoriApiMock
        //        .Setup(api => api.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
        //        .ReturnsAsync(new KategoriResponse("1", uuidKategori!, "Kategori Tes"));

        //    using var scope = Factory.Services.CreateScope();
        //    var services = scope.ServiceProvider;

        //    Result? result = null;

        //    // === Act ===
        //    switch (mode)
        //    {
        //        case "created":
        //            {
        //                var handler = new CreateKategoriLuaranCommandHandler(
        //                    kategoriApiMock.Object,
        //                    services.GetRequiredService<IKategoriLuaranRepository>(),
        //                    services.GetRequiredService<IUnitOfWork>()
        //                );

        //                var command = new CreateKategoriLuaranCommand(
        //                    uuidKategori!,
        //                    nama!,
        //                    status!
        //                );

        //                result = await handler.Handle(command, CancellationToken.None);
        //                break;
        //            }

        //        case "updated":
        //            {
        //                var handlerUpdate = new UpdateKategoriLuaranCommandHandler(
        //                    kategoriApiMock.Object,
        //                    services.GetRequiredService<IKategoriLuaranRepository>(),
        //                    services.GetRequiredService<IUnitOfWork>()
        //                );

        //                var commandUpdate = new UpdateKategoriLuaranCommand(
        //                    uuid!,
        //                    uuidKategori!,
        //                    nama!,
        //                    status!
        //                );

        //                result = await handlerUpdate.Handle(commandUpdate, CancellationToken.None);
        //                break;
        //            }

        //        case "deleted":
        //            {
        //                var handlerDelete = new DeleteKategoriLuaranCommandHandler(
        //                    services.GetRequiredService<IKategoriLuaranRepository>(),
        //                    services.GetRequiredService<IUnitOfWork>()
        //                );

        //                var commandDelete = new DeleteKategoriLuaranCommand(uuid!);
        //                result = await handlerDelete.Handle(commandDelete, CancellationToken.None);
        //                break;
        //            }
        //    }

        //    // === Assert ===
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
        public async Task CreateUpdateDelete_ShouldBeExecute_WhenValidData(object[] initial, object?[] updated, string action)
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
                    updated![1].ToString()!,
                    updated![2].ToString()!
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

        [Fact]
        public async Task Handle_All_ReturnsSuccess_WhenDataExists()
        {
            // Arrange
            var mockConnectionFactory = new Mock<IDbConnectionFactory>();
            var mockConnection = new Mock<DbConnection>();

            var fakeDataList = new List<KategoriLuaranResponse>
            {
                new() { Uuid = Guid.NewGuid().ToString(), uuidKategori = Guid.NewGuid().ToString(), Nama = "Luaran 1", Status = "ok" },
                new() { Uuid = Guid.NewGuid().ToString(), uuidKategori = Guid.NewGuid().ToString(), Nama = "Luaran 2", Status = "ok" }
            };

            mockConnection.SetupDapperAsync(c =>
                c.QueryAsync<KategoriLuaranResponse>(
                    It.IsAny<string>(),
                    It.IsAny<object>(),
                    It.IsAny<IDbTransaction>(),
                    It.IsAny<int?>(),
                    It.IsAny<System.Data.CommandType?>()
                )
            ).ReturnsAsync(fakeDataList);

            mockConnectionFactory.Setup(f => f.OpenConnectionAsync())
                .ReturnsAsync(mockConnection.Object);

            var handler = new GetAllKategoriLuaranQueryHandler(mockConnectionFactory.Object);
            var query = new GetAllKategoriLuaranQuery();

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotEmpty(result.Value);
            Assert.Equal(fakeDataList[0].Nama, result.Value[0].Nama);
        }

        [Fact]
        public async Task Handle_All_ReturnsFailure_WhenNoData()
        {
            // Arrange
            var mockConnectionFactory = new Mock<IDbConnectionFactory>();
            var mockConnection = new Mock<DbConnection>();

            mockConnection.SetupDapperAsync(c =>
                c.QueryAsync<KategoriLuaranResponse>(
                    It.IsAny<string>(),
                    It.IsAny<object>(),
                    It.IsAny<IDbTransaction>(),
                    It.IsAny<int?>(),
                    It.IsAny<System.Data.CommandType?>()
                )
            ).ReturnsAsync(new List<KategoriLuaranResponse>());

            mockConnectionFactory.Setup(f => f.OpenConnectionAsync())
                .ReturnsAsync(mockConnection.Object);

            var handler = new GetAllKategoriLuaranQueryHandler(mockConnectionFactory.Object);
            var query = new GetAllKategoriLuaranQuery();

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
        }

        [Fact]
        public async Task Handle_Default_ReturnsSuccess_WhenDataExists()
        {
            // Arrange
            var mockConnectionFactory = new Mock<IDbConnectionFactory>();
            var mockConnection = new Mock<DbConnection>();
            var uuid = Guid.NewGuid();

            var fakeData = new KategoriLuaranDefaultResponse
            {
                Id = "1",
                Uuid = uuid.ToString(),
                uuidKategori = Guid.NewGuid().ToString(),
                KategoriId = "10",
                Nama = "Luaran Default",
                Status = "ok"
            };

            mockConnection.SetupDapperAsync(c =>
                c.QuerySingleOrDefaultAsync<KategoriLuaranDefaultResponse>(
                    It.IsAny<string>(),
                    It.IsAny<object>(),
                    It.IsAny<IDbTransaction>(),
                    It.IsAny<int?>(),
                    It.IsAny<System.Data.CommandType?>()
                )
            ).ReturnsAsync(fakeData);

            mockConnectionFactory.Setup(f => f.OpenConnectionAsync())
                .ReturnsAsync(mockConnection.Object);

            var handler = new GetKategoriLuaranDefaultQueryHandler(mockConnectionFactory.Object);
            var query = new GetKategoriLuaranDefaultQuery(uuid);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(fakeData.Nama, result.Value.Nama);
        }

        [Fact]
        public async Task Handle_Default_ReturnsFailure_WhenDataNotFound()
        {
            // Arrange
            var mockConnectionFactory = new Mock<IDbConnectionFactory>();
            var mockConnection = new Mock<DbConnection>();

            mockConnection.SetupDapperAsync(c =>
                c.QuerySingleOrDefaultAsync<KategoriLuaranDefaultResponse>(
                    It.IsAny<string>(),
                    It.IsAny<object>(),
                    It.IsAny<IDbTransaction>(),
                    It.IsAny<int?>(),
                    It.IsAny<System.Data.CommandType?>()
                )
            ).ReturnsAsync((KategoriLuaranDefaultResponse?)null);

            mockConnectionFactory.Setup(f => f.OpenConnectionAsync())
                .ReturnsAsync(mockConnection.Object);

            var handler = new GetKategoriLuaranDefaultQueryHandler(mockConnectionFactory.Object);
            var query = new GetKategoriLuaranDefaultQuery(Guid.NewGuid());

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
        }

        [Fact]
        public async Task Handle_ReturnsSuccess_WhenDataExists()
        {
            // Arrange
            var mockConnectionFactory = new Mock<IDbConnectionFactory>();
            var mockConnection = new Mock<DbConnection>();
            var uuid = Guid.NewGuid();

            var fakeData = new KategoriLuaranResponse
            {
                Uuid = uuid.ToString(),
                uuidKategori = Guid.NewGuid().ToString(),
                Nama = "Luaran 1",
                Status = "ok"
            };

            mockConnection.SetupDapperAsync(c =>
                c.QuerySingleOrDefaultAsync<KategoriLuaranResponse>(
                    It.IsAny<string>(),
                    It.IsAny<object>(),
                    It.IsAny<IDbTransaction>(),
                    It.IsAny<int?>(),
                    It.IsAny<System.Data.CommandType?>()
                )
            ).ReturnsAsync(fakeData);

            mockConnectionFactory.Setup(f => f.OpenConnectionAsync())
                .ReturnsAsync(mockConnection.Object);

            var handler = new GetKategoriLuaranQueryHandler(mockConnectionFactory.Object);
            var query = new GetKategoriLuaranQuery(uuid.ToString());

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(fakeData.Nama, result.Value.Nama);
        }

        [Fact]
        public async Task Handle_ReturnsFailure_WhenDataNotFound()
        {
            // Arrange
            var mockConnectionFactory = new Mock<IDbConnectionFactory>();
            var mockConnection = new Mock<DbConnection>();

            mockConnection.SetupDapperAsync(c =>
                c.QuerySingleOrDefaultAsync<KategoriLuaranResponse>(
                    It.IsAny<string>(),
                    It.IsAny<object>(),
                    It.IsAny<IDbTransaction>(),
                    It.IsAny<int?>(),
                    It.IsAny<System.Data.CommandType?>()
                )
            ).ReturnsAsync((KategoriLuaranResponse?)null);

            mockConnectionFactory.Setup(f => f.OpenConnectionAsync())
                .ReturnsAsync(mockConnection.Object);

            var handler = new GetKategoriLuaranQueryHandler(mockConnectionFactory.Object);
            var query = new GetKategoriLuaranQuery(Guid.NewGuid().ToString());

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
        }
    }
}
