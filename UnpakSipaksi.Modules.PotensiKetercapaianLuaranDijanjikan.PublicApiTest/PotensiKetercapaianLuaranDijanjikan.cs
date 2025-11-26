using MediatR;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.PotensiKetercapaianLuaranDijanjikan.Application.GetBobotPotensiKetercapaianLuaranDijanjikan;
using UnpakSipaksi.Modules.PotensiKetercapaianLuaranDijanjikan.Application.GetPotensiKetercapaianLuaranDijanjikan;
using UnpakSipaksi.Modules.PotensiKetercapaianLuaranDijanjikan.Domain.PotensiKetercapaianLuaranDijanjikan;
using UnpakSipaksi.Modules.PotensiKetercapaianLuaranDijanjikan.Infrastructure.PublicApi;
using Xunit;

namespace UnpakSipaksi.Modules.PotensiKetercapaianLuaranDijanjikan.PublicApiTest
{
    public sealed class PotensiKetercapaianLuaranDijanjikanApiTests
    {
        private readonly Mock<ISender> _senderMock;
        private readonly PotensiKetercapaianLuaranDijanjikanApi _api;

        public PotensiKetercapaianLuaranDijanjikanApiTests()
        {
            _senderMock = new Mock<ISender>();
            _api = new PotensiKetercapaianLuaranDijanjikanApi(_senderMock.Object);
        }

        [Fact]
        public async Task GetAsync_WhenSuccess_ReturnsResponse()
        {
            // Arrange
            Guid uuid = Guid.NewGuid();
            var response = new PotensiKetercapaianLuaranDijanjikanDefaultResponse
            {
                Id = "1",
                Uuid = uuid.ToString(),
                Nama = "Potensi Tinggi",
                BobotPDP = 10,
                BobotTerapan = 20,
                BobotKerjasama = 30,
                BobotPenelitianDasar = 40,
                Skor = 50
            };

            _senderMock.Setup(s => s.Send(
                It.Is<GetPotensiKetercapaianLuaranDijanjikanDefaultQuery>(
                    q => q.PotensiKetercapaianLuaranDijanjikanUuid == uuid),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Success(response));

            // Act
            var result = await _api.GetAsync(uuid, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("1", result.Id);
            Assert.Equal(uuid.ToString(), result.Uuid);
            Assert.Equal("Potensi Tinggi", result.Nama);
            Assert.Equal(10, result.BobotPDP);
            Assert.Equal(20, result.BobotTerapan);
            Assert.Equal(30, result.BobotKerjasama);
            Assert.Equal(40, result.BobotPenelitianDasar);
            Assert.Equal(50, result.Skor);
        }

        [Fact]
        public async Task GetAsync_WhenFailure_ReturnsNull()
        {
            // Arrange
            Guid uuid = Guid.NewGuid();

            _senderMock.Setup(s => s.Send(
                    It.Is<GetPotensiKetercapaianLuaranDijanjikanDefaultQuery>(
                        q => q.PotensiKetercapaianLuaranDijanjikanUuid == uuid),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(Result.Failure<PotensiKetercapaianLuaranDijanjikanDefaultResponse>(PotensiKetercapaianLuaranDijanjikanErrors.NotFound(Guid.NewGuid())));

            // Act
            var result = await _api.GetAsync(uuid, CancellationToken.None);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetBobotWithoutTargetAsync_WhenSuccess_ReturnsValue()
        {
            // Arrange
            string kategori = "PDP";

            _senderMock.Setup(s => s.Send(
                    It.Is<GetBobotPotensiKetercapaianLuaranDijanjikanQuery>(q => q.KategoriSkema == kategori),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(Result.Success<int?>(25));

            // Act
            var result = await _api.GetBobotWithoutTargetAsync(kategori, CancellationToken.None);

            // Assert
            Assert.Equal(25, result);
        }

        [Fact]
        public async Task GetBobotWithoutTargetAsync_WhenFailure_ReturnsNull()
        {
            // Arrange
            string kategori = "PDP";

            _senderMock.Setup(s => s.Send(
                    It.Is<GetBobotPotensiKetercapaianLuaranDijanjikanQuery>(q => q.KategoriSkema == kategori),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(Result.Failure<int?>(PotensiKetercapaianLuaranDijanjikanErrors.UnknownKategoriSkema()));

            // Act
            var result = await _api.GetBobotWithoutTargetAsync(kategori, CancellationToken.None);

            // Assert
            Assert.Null(result);
        }
    }
}
