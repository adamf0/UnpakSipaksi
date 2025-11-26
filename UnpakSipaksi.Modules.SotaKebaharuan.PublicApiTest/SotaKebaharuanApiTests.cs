using MediatR;
using Moq;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.SotaKebaharuan.Application.GetBobotSotaKebaharuan;
using UnpakSipaksi.Modules.SotaKebaharuan.Application.GetSotaKebaharuan;
using UnpakSipaksi.Modules.SotaKebaharuan.Domain.SotaKebaharuan;
using UnpakSipaksi.Modules.SotaKebaharuan.Infrastructure.PublicApi;
using UnpakSipaksi.Modules.SotaKebaharuan.PublicApi;
using Xunit;

namespace UnpakSipaksi.Tests.Modules.SotaKebaharuan.Infrastructure.PublicApiTest
{
    public class SotaKebaharuanApiTests
    {
        private readonly Mock<ISender> _mockSender;
        private readonly SotaKebaharuanApi _api;

        public SotaKebaharuanApiTests()
        {
            _mockSender = new Mock<ISender>();
            _api = new SotaKebaharuanApi(_mockSender.Object);
        }

        [Fact]
        public async Task GetAsync_ShouldReturnNull_WhenResultIsFailure()
        {
            // Arrange
            var failure = Result.Failure<SotaKebaharuanDefaultResponse>(SotaKebaharuanErrors.NotFound(Guid.NewGuid()));

            _mockSender
                .Setup(s => s.Send(It.IsAny<GetSotaKebaharuanDefaultQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(failure);

            // Act
            var result = await _api.GetAsync(Guid.NewGuid());

            // Assert
            Assert.Null(result);
            Assert.True(failure.IsFailure);
        }

        [Fact]
        public async Task GetAsync_ShouldReturnValue_WhenResultIsSuccess()
        {
            // Arrange
            var data = new SotaKebaharuanDefaultResponse
            {
                Id = "SKB-001",
                Uuid = Guid.NewGuid().ToString(),
                Nama = "Kebaharuan AI",
                BobotPDP = 10,
                BobotTerapan = 20,
                BobotKerjasama = 30,
                BobotPenelitianDasar = 40,
                Skor = 95
            };

            var success = Result.Success(data);

            _mockSender
                .Setup(s => s.Send(It.IsAny<GetSotaKebaharuanDefaultQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(success);

            // Act
            var response = await _api.GetAsync(Guid.Parse(data.Uuid));

            // Assert
            Assert.NotNull(response);
            Assert.Equal(data.Id, response!.Id);
            Assert.Equal(data.Uuid, response.Uuid);
            Assert.Equal(data.Nama, response.Nama);
            Assert.Equal(data.BobotPDP, response.BobotPDP);
            Assert.Equal(data.BobotTerapan, response.BobotTerapan);
            Assert.Equal(data.BobotKerjasama, response.BobotKerjasama);
            Assert.Equal(data.BobotPenelitianDasar, response.BobotPenelitianDasar);
            Assert.Equal(data.Skor, response.Skor);
        }

        [Fact]
        public async Task GetBobotWithoutTargetAsync_ShouldReturnNull_WhenResultIsFailure()
        {
            // Arrange
            var failure = Result.Failure<int?>(SotaKebaharuanErrors.UnknownKategoriSkema());

            _mockSender
                .Setup(s => s.Send(It.IsAny<GetBobotSotaKebaharuanQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(failure);

            // Act
            var response = await _api.GetBobotWithoutTargetAsync("PDP");

            // Assert
            Assert.Null(response);
            Assert.True(failure.IsFailure);
        }

        [Fact]
        public async Task GetBobotWithoutTargetAsync_ShouldReturnValue_WhenResultIsSuccess()
        {
            // Arrange
            int value = 75;
            var success = Result.Success<int?>(value);

            _mockSender
                .Setup(s => s.Send(It.IsAny<GetBobotSotaKebaharuanQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(success);

            // Act
            var response = await _api.GetBobotWithoutTargetAsync("PDP");

            // Assert
            Assert.NotNull(response);
            Assert.Equal(value, response);
        }
    }
}
