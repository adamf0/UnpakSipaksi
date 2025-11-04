using Docker.DotNet.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System.Reflection;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.IndikatorCapaian.Application.Abstractions.Data;
using UnpakSipaksi.Modules.IndikatorCapaian.Application.CreateIndikatorCapaian;
using UnpakSipaksi.Modules.IndikatorCapaian.ApplicationTest;
using UnpakSipaksi.Modules.IndikatorCapaian.Domain;
using UnpakSipaksi.Modules.JenisLuaran.PublicApi;
using Xunit;

namespace Application.Integration.Tests
{
    public class IndikatorCapaianTest : BaseIntegrationTest
    {
        public IndikatorCapaianTest(IntegrationTestWebAppFactory factory) : base(factory)
        {
        }

        public static IEnumerable<object[]> InvalidData()
        {
            var validUuid = Guid.NewGuid().ToString();
            var empty = "";
            var guidEmpty = Guid.Empty.ToString();

            // CREATE invalid Nama
            yield return new object[] { validUuid, "", "aktif", "'Nama' tidak boleh kosong." };
            // CREATE invalid Status
            yield return new object[] { validUuid, "Luaran Tes", "", "'Status' tidak boleh kosong." };
            // CREATE invalid JenisLuaran
            yield return new object[] { guidEmpty, "Luaran Tes", "aktif", "'JenisLuaran' harus dalam format UUID v4 yang valid." };
        }

        [Theory]
        [MemberData(nameof(InvalidData))]
        public async Task Create_ShouldThrow_WhenInvalid(
            string jenisLuaranId,
            string nama,
            string status,
            string message)
        {
            var command = new CreateIndikatorCapaianCommand(jenisLuaranId, nama, status);
            var result = await Sender.Send(command);

            Assert.True(result.IsFailure);
            var validationError = Assert.IsType<ValidationError>(result.Error);
            Assert.Contains(validationError.Errors, e => e.Description == message);
        }

        [Fact]
        public async Task Create_ShouldAdd_WhenValid_AndCallHandler()
        {
            var jenisLuaranId = Guid.NewGuid().ToString();

            // Mock IJenisLuaranApi
            var jenisLuaranApiMock = new Mock<IJenisLuaranApi>();
            jenisLuaranApiMock
                .Setup(api => api.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new JenisLuaranResponse("1", jenisLuaranId, "Luaran Tes"));

            using (var scope = Factory.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                var handler = new CreateIndikatorCapaianCommandHandler(
                    jenisLuaranApiMock.Object,
                    services.GetRequiredService<IIndikatorCapaianRepository>(),
                    services.GetRequiredService<IUnitOfWork>()
                );

                var command = new CreateIndikatorCapaianCommand(
                    jenisLuaranId,
                    "Luaran Tes",
                    "aktif"
                );

                // Act
                var result = await handler.Handle(command, CancellationToken.None);

                // Assert
                Assert.True(result.IsSuccess);

                var data = DBContext.IndikatorCapaian.FirstOrDefault(p => p.Uuid == result.Value);
                Assert.NotNull(data);
                Assert.Equal("Luaran Tes", data.Nama);
                Assert.Equal("aktif", data.Status);

                // Pastikan mock terpanggil
                jenisLuaranApiMock.Verify(api => api.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Once);
            }
        }
    }
}