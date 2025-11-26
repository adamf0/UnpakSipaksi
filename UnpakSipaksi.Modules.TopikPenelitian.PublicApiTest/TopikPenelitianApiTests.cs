using MediatR;
using Moq;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.TopikPenelitian.Application.GetTopikPenelitian;
using UnpakSipaksi.Modules.TopikPenelitian.Domain.TopikPenelitian;
using UnpakSipaksi.Modules.TopikPenelitian.Infrastructure.PublicApi;
using UnpakSipaksi.Modules.TopikPenelitian.PublicApi;
using Xunit;

namespace UnpakSipaksi.Tests.Modules.TopikPenelitian.Infrastructure.PublicApiTest
{
    public class TopikPenelitianApiTests
    {
        private readonly Mock<ISender> _mockSender;
        private readonly TopikPenelitianApi _api;

        public TopikPenelitianApiTests()
        {
            _mockSender = new Mock<ISender>();
            _api = new TopikPenelitianApi(_mockSender.Object);
        }

        [Fact]
        public async Task GetAsync_ShouldReturnNull_WhenResultIsFailure()
        {
            // Arrange
            var failure = Result.Failure<TopikPenelitianDefaultResponse>(TopikPenelitianErrors.NotFound(Guid.NewGuid()));

            _mockSender
                .Setup(s => s.Send(It.IsAny<GetTopikPenelitianDefaultQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(failure);

            // Act
            var result = await _api.GetAsync(Guid.NewGuid());

            // Assert
            Assert.Null(result);
            Assert.True(failure.IsFailure);
        }

        [Fact]
        public async Task GetAsync_ShouldReturnMappedResponse_WhenResultIsSuccess()
        {
            // Arrange
            var data = new TopikPenelitianDefaultResponse
            {
                Id = "TP-100",
                Uuid = Guid.NewGuid().ToString(),
                TemaPenelitianUuid = Guid.NewGuid().ToString(),
                Nama = "Topik AI dan Machine Learning"
            };

            var success = Result.Success(data);

            _mockSender
                .Setup(s => s.Send(It.IsAny<GetTopikPenelitianDefaultQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(success);

            // Act
            var response = await _api.GetAsync(Guid.Parse(data.Uuid));

            // Assert
            Assert.NotNull(response);
            Assert.Equal(data.Id, response!.Id);
            Assert.Equal(data.Uuid, response.Uuid);
            Assert.Equal(data.TemaPenelitianUuid, response.TemaPenelitianUuid);
            Assert.Equal(data.Nama, response.Nama);
        }
    }
}
