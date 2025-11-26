using MediatR;
using Moq;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.VideoKegiatan.Application.GetBobotVideoKegiatan;
using UnpakSipaksi.Modules.VideoKegiatan.Application.GetVideoKegiatan;
using UnpakSipaksi.Modules.VideoKegiatan.Domain.VideoKegiatan;
using UnpakSipaksi.Modules.VideoKegiatan.Infrastructure.PublicApi;
using Xunit;

namespace UnpakSipaksi.Tests.Modules.VideoKegiatan.Infrastructure.PublicApiTest
{
    public class VideoKegiatanApiTests
    {
        private readonly Mock<ISender> _mockSender;
        private readonly VideoKegiatanApi _api;

        public VideoKegiatanApiTests()
        {
            _mockSender = new Mock<ISender>();
            _api = new VideoKegiatanApi(_mockSender.Object);
        }

        [Fact]
        public async Task GetAsync_ShouldReturnNull_WhenResultIsFailure()
        {
            // Arrange
            var failure = Result.Failure<VideoKegiatanDefaultResponse>(VideoKegiatanErrors.NotFound(Guid.NewGuid()));

            _mockSender
                .Setup(s => s.Send(It.IsAny<GetVideoKegiatanDefaultQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(failure);

            // Act
            var response = await _api.GetAsync(Guid.NewGuid());

            // Assert
            Assert.Null(response);
            Assert.True(failure.IsFailure);
        }

        [Fact]
        public async Task GetAsync_ShouldReturnMappedResponse_WhenResultIsSuccess()
        {
            // Arrange
            var data = new VideoKegiatanDefaultResponse
            {
                Id = "VID123",
                Uuid = Guid.NewGuid().ToString(),
                Nama = "Video Seminar Nasional",
                Nilai = 88
            };

            var success = Result.Success(data);

            _mockSender
                .Setup(s => s.Send(It.IsAny<GetVideoKegiatanDefaultQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(success);

            // Act
            var response = await _api.GetAsync(Guid.Parse(data.Uuid));

            // Assert
            Assert.NotNull(response);
            Assert.Equal(data.Id, response!.Id);
            Assert.Equal(data.Uuid, response.Uuid);
            Assert.Equal(data.Nama, response.Nama);
            Assert.Equal(data.Nilai, response.Nilai);
        }

        [Fact]
        public async Task GetBobotWithoutTargetAsync_ShouldReturnNull_WhenResultIsFailure()
        {
            // Arrange
            var failure = Result.Failure<int?>(VideoKegiatanErrors.NotFound(Guid.NewGuid()));

            _mockSender
                .Setup(s => s.Send(It.IsAny<GetBobotVideoKegiatanQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(failure);

            // Act
            var response = await _api.GetBobotWithoutTargetAsync();

            // Assert
            Assert.Null(response);
            Assert.True(failure.IsFailure);
        }

        [Fact]
        public async Task GetBobotWithoutTargetAsync_ShouldReturnValue_WhenResultIsSuccess()
        {
            // Arrange
            int? bobot = 75;
            var success = Result.Success(bobot);

            _mockSender
                .Setup(s => s.Send(It.IsAny<GetBobotVideoKegiatanQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(success);

            // Act
            var response = await _api.GetBobotWithoutTargetAsync();

            // Assert
            Assert.Equal(bobot, response);
            Assert.True(success.IsSuccess);
        }
    }
}
