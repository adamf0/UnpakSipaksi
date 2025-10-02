using Docker.DotNet.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System.Reflection;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.Kategori.PublicApi;
using UnpakSipaksi.Modules.KategoriLuaran.Application.Abstractions.Data;
using UnpakSipaksi.Modules.KategoriLuaran.Application.CreateKategoriLuaran;
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

        public static IEnumerable<object[]> InvalidData()
        {
            var validUuid = Guid.NewGuid().ToString();
            var empty = "";

            // Nama kosong
            yield return new object[] { validUuid, "", "aktif", "'Nama' tidak boleh kosong." };
            // Status kosong
            yield return new object[] { validUuid, "Kategori Tes", "", "'Status' tidak boleh kosong." };
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

        [Fact]
        public async Task Create_ShouldAdd_WhenValid_AndCallHandler()
        {
            var uuidKategori = Guid.NewGuid().ToString();

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

                // Assert
                Assert.True(result.IsSuccess);

                var data = DBContext.KategoriLuaran.FirstOrDefault(p => p.Uuid == result.Value);
                Assert.NotNull(data);
                Assert.Equal("Kategori Tes", data.Nama);
                Assert.Equal("aktif", data.Status);

                // Pastikan mock terpanggil
                kategoriApiMock.Verify(api => api.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Once);
            }
        }
    }
}
