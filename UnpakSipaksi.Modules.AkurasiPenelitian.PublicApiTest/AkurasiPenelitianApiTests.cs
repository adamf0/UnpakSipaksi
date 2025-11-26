using MediatR;
using Moq;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.AkurasiPenelitian.Application.GetAkurasiPenelitian;
using UnpakSipaksi.Modules.AkurasiPenelitian.Application.GetBobotAkurasiPenelitian;
using UnpakSipaksi.Modules.AkurasiPenelitian.Domain.AkurasiPenelitian;
using UnpakSipaksi.Modules.AkurasiPenelitian.Infrastructure.PublicApi;
using Xunit;

namespace UnpakSipaksi.Modules.AkurasiPenelitian.PublicApiTest
{
    public class AkurasiPenelitianApiTests
    {
        private readonly Mock<ISender> _mockSender;
        private readonly AkurasiPenelitianApi _api;

        public AkurasiPenelitianApiTests()
        {
            _mockSender = new Mock<ISender>();
            _api = new AkurasiPenelitianApi(_mockSender.Object);
        }

        [Fact]
        public async Task GetAsync_ShouldReturnNull_WhenResultIsFailure()
        {
            // Arrange
            var failureResult = Result.Failure<AkurasiPenelitianDefaultResponse>(
                AkurasiPenelitianErrors.NotFound(Guid.NewGuid()));

            _mockSender
                .Setup(s => s.Send(It.IsAny<GetAkurasiPenelitianDefaultQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(failureResult);

            // Act
            var apiResponse = await _api.GetAsync(Guid.NewGuid());

            // Assert
            Assert.False(failureResult.IsSuccess);
            Assert.Null(apiResponse);
        }

        [Fact]
        public async Task GetAsync_ShouldReturnResponse_WhenResultIsSuccess()
        {
            // Arrange
            var defaultResponse = new AkurasiPenelitianDefaultResponse
            {
                Id = "id123",
                Uuid = Guid.NewGuid().ToString(),
                Nama = "Penelitian A",
                BobotPDP = 10,
                BobotTerapan = 20,
                BobotKerjasama = 30,
                BobotPenelitianDasar = 40,
                Skor = 100
            };

            var successResult = Result.Success(defaultResponse);

            _mockSender
                .Setup(s => s.Send(It.IsAny<GetAkurasiPenelitianDefaultQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(successResult);

            // Act
            var apiResponse = await _api.GetAsync(Guid.Parse(defaultResponse.Uuid));

            // Assert
            Assert.True(successResult.IsSuccess);
            Assert.NotNull(apiResponse);
            Assert.Equal(defaultResponse.Id, apiResponse!.Id);
            Assert.Equal(defaultResponse.Nama, apiResponse.Nama);
            Assert.Equal(defaultResponse.BobotPDP, apiResponse.BobotPDP);
            Assert.Equal(defaultResponse.BobotTerapan, apiResponse.BobotTerapan);
        }

        [Fact]
        public async Task GetBobotWithoutTargetAsync_ShouldReturnNull_WhenResultIsFailure()
        {
            // Arrange
            var failureResult = Result.Failure<int?>(AkurasiPenelitianErrors.UnknownKategoriSkema());

            _mockSender
                .Setup(s => s.Send(It.IsAny<GetBobotAkurasiPenelitianQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(failureResult);

            // Act
            var response = await _api.GetBobotWithoutTargetAsync("KategoriA");

            // Assert
            Assert.False(failureResult.IsSuccess);
            Assert.Null(response);
        }

        [Fact]
        public async Task GetBobotWithoutTargetAsync_ShouldReturnValue_WhenResultIsSuccess()
        {
            // Arrange
            int bobot = 50;
            var successResult = Result.Success<int?>(bobot);

            _mockSender
                .Setup(s => s.Send(It.IsAny<GetBobotAkurasiPenelitianQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(successResult);

            // Act
            var response = await _api.GetBobotWithoutTargetAsync("KategoriA");

            // Assert
            Assert.True(successResult.IsSuccess);
            Assert.Equal(bobot, response);
        }

    }
}
