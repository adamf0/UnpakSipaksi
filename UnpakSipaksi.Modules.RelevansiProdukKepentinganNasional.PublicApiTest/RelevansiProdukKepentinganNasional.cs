using MediatR;
using Moq;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.RelevansiProdukKepentinganNasional.Application.GetBobotRelevansiProdukKepentinganNasional;
using UnpakSipaksi.Modules.RelevansiProdukKepentinganNasional.Application.GetRelevansiProdukKepentinganNasional;
using UnpakSipaksi.Modules.RelevansiProdukKepentinganNasional.Domain.RelevansiProdukKepentinganNasional;
using UnpakSipaksi.Modules.RelevansiProdukKepentinganNasional.Infrastructure.PublicApi;
using UnpakSipaksi.Modules.RelevansiProdukKepentinganNasional.PublicApi;
using Xunit;

namespace UnpakSipaksi.Modules.RelevansiProdukKepentinganNasional.PublicApiTest
{
    public class RelevansiProdukKepentinganNasionalApiTests
    {
        private readonly Mock<ISender> _senderMock;
        private readonly RelevansiProdukKepentinganNasionalApi _api;

        public RelevansiProdukKepentinganNasionalApiTests()
        {
            _senderMock = new Mock<ISender>();
            _api = new RelevansiProdukKepentinganNasionalApi(_senderMock.Object);
        }

        [Fact]
        public async Task GetAsync_ShouldReturnNull_WhenFailure()
        {
            // Arrange
            _senderMock
                .Setup(s => s.Send(
                    It.IsAny<GetRelevansiProdukKepentinganNasionalDefaultQuery>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(Result.Failure<RelevansiProdukKepentinganNasionalDefaultResponse>(RelevansiProdukKepentinganNasionalErrors.NotFound(Guid.NewGuid())));

            // Act
            var result = await _api.GetAsync(Guid.NewGuid());

            // Assert
            Assert.Null(result);
        }


        [Fact]
        public async Task GetAsync_ShouldReturnResponse_WhenSuccess()
        {
            // Arrange
            var uuid = Guid.NewGuid();

            var response = new RelevansiProdukKepentinganNasionalDefaultResponse
            {
                Id = "RLV-123",
                Uuid = uuid.ToString(),
                Nama = "Relevansi Test",
                BobotPDP = 10,
                BobotTerapan = 20,
                BobotKerjasama = 30,
                BobotPenelitianDasar = 40,
                Skor = 100
            };

            _senderMock
                .Setup(s => s.Send(
                    It.IsAny<GetRelevansiProdukKepentinganNasionalDefaultQuery>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(Result.Success(response));

            // Act
            var result = await _api.GetAsync(uuid);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("RLV-123", result.Id);
            Assert.Equal(uuid.ToString(), result.Uuid);
            Assert.Equal("Relevansi Test", result.Nama);
            Assert.Equal(10, result.BobotPDP);
            Assert.Equal(20, result.BobotTerapan);
            Assert.Equal(30, result.BobotKerjasama);
            Assert.Equal(40, result.BobotPenelitianDasar);
            Assert.Equal(100, result.Skor);
        }

        [Fact]
        public async Task GetBobotWithoutTargetAsync_ShouldReturnNull_WhenFailure()
        {
            // Arrange
            _senderMock
                .Setup(s => s.Send(
                    It.IsAny<GetBobotRelevansiProdukKepentinganNasionalQuery>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(Result.Failure<int?>(RelevansiProdukKepentinganNasionalErrors.UnknownKategoriSkema()));

            // Act
            var result = await _api.GetBobotWithoutTargetAsync("PDP");

            // Assert
            Assert.Null(result);
        }


        [Fact]
        public async Task GetBobotWithoutTargetAsync_ShouldReturnValue_WhenSuccess()
        {
            // Arrange
            _senderMock
                .Setup(s => s.Send(
                    It.IsAny<GetBobotRelevansiProdukKepentinganNasionalQuery>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(Result.Success<int?>(75));

            // Act
            var result = await _api.GetBobotWithoutTargetAsync("PDP");

            // Assert
            Assert.NotNull(result);
            Assert.Equal(75, result);
        }
    }
}
