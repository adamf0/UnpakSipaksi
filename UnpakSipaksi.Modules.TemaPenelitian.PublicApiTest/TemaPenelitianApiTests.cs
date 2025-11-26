using MediatR;
using Moq;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.TemaPenelitian.Application.GetTemaPenelitian;
using UnpakSipaksi.Modules.TemaPenelitian.Domain.TemaPenelitian;
using UnpakSipaksi.Modules.TemaPenelitian.Infrastructure.PublicApi;
using UnpakSipaksi.Modules.TemaPenelitian.PublicApi;
using Xunit;

namespace UnpakSipaksi.Tests.Modules.TemaPenelitian.Infrastructure.PublicApiTest
{
    public class TemaPenelitianApiTests
    {
        private readonly Mock<ISender> _mockSender;
        private readonly TemaPenelitianApi _api;

        public TemaPenelitianApiTests()
        {
            _mockSender = new Mock<ISender>();
            _api = new TemaPenelitianApi(_mockSender.Object);
        }

        [Fact]
        public async Task GetAsync_ShouldReturnNull_WhenResultIsFailure()
        {
            // Arrange
            var failureResult = Result.Failure<TemaPenelitianDefaultResponse>(TemaPenelitianErrors.NotFound(Guid.NewGuid()));

            _mockSender
                .Setup(s => s.Send(It.IsAny<GetTemaPenelitianDefaultQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(failureResult);

            // Act
            var result = await _api.GetAsync(Guid.NewGuid());

            // Assert
            Assert.Null(result);
            Assert.True(failureResult.IsFailure);
        }

        [Fact]
        public async Task GetAsync_ShouldReturnMappedResponse_WhenResultIsSuccess()
        {
            // Arrange
            var data = new TemaPenelitianDefaultResponse
            {
                Id = "TMP-001",
                Uuid = Guid.NewGuid().ToString(),
                FokusPenelitianUuid = Guid.NewGuid().ToString(),
                Nama = "Tema Kecerdasan Buatan"
            };

            var success = Result.Success(data);

            _mockSender
                .Setup(s => s.Send(It.IsAny<GetTemaPenelitianDefaultQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(success);

            // Act
            var response = await _api.GetAsync(Guid.Parse(data.Uuid));

            // Assert
            Assert.NotNull(response);
            Assert.Equal(data.Id, response!.Id);
            Assert.Equal(data.Uuid, response.Uuid);
            Assert.Equal(data.FokusPenelitianUuid, response.FokusPenelitianUuid);
            Assert.Equal(data.Nama, response.Nama);
        }
    }
}
