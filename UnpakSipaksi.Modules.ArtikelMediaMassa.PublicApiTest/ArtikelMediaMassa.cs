using MediatR;
using Moq;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.ArtikelMediaMassa.Application.GetArtikelMediaMassa;
using UnpakSipaksi.Modules.ArtikelMediaMassa.Application.GetBobotArtikelMediaMassa;
using UnpakSipaksi.Modules.ArtikelMediaMassa.Domain.ArtikelMediaMassa;
using UnpakSipaksi.Modules.ArtikelMediaMassa.Infrastructure.PublicApi;
using Xunit;

namespace UnpakSipaksi.Modules.ArtikelMediaMassa.PublicApiTest
{
    public class ArtikelMediaMassaApiTests
    {
        private readonly Mock<ISender> _mockSender;
        private readonly ArtikelMediaMassaApi _api;

        public ArtikelMediaMassaApiTests()
        {
            _mockSender = new Mock<ISender>();
            _api = new ArtikelMediaMassaApi(_mockSender.Object);
        }

        [Fact]
        public async Task GetAsync_ShouldReturnNull_WhenResultIsFailure()
        {
            // Arrange
            var failure = Result.Failure<ArtikelMediaMassaDefaultResponse>(ArtikelMediaMassaErrors.NotFound(Guid.NewGuid()));

            _mockSender
                .Setup(s => s.Send(It.IsAny<GetArtikelMediaMassaDefaultQuery>(), It.IsAny<CancellationToken>()))
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
            var data = new ArtikelMediaMassaDefaultResponse
            {
                Id = "ID123",
                Uuid = Guid.NewGuid().ToString(),
                Nama = "Judul Artikel",
                Nilai = 90
            };

            var success = Result.Success(data);

            _mockSender
                .Setup(s => s.Send(It.IsAny<GetArtikelMediaMassaDefaultQuery>(), It.IsAny<CancellationToken>()))
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
            var failure = Result.Failure<int?>(ArtikelMediaMassaErrors.UnknownKategoriSkema());

            _mockSender
                .Setup(s => s.Send(It.IsAny<GetBobotArtikelMediaMassaQuery>(), It.IsAny<CancellationToken>()))
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
            int? bobot = 55;
            var success = Result.Success(bobot);

            _mockSender
                .Setup(s => s.Send(It.IsAny<GetBobotArtikelMediaMassaQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(success);

            // Act
            var response = await _api.GetBobotWithoutTargetAsync();

            // Assert
            Assert.Equal(bobot, response);
            Assert.True(success.IsSuccess);
        }
    }
}
