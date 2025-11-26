using MediatR;
using Moq;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.FokusPengabdian.Application.GetFokusPengabdian;
using UnpakSipaksi.Modules.FokusPengabdian.Domain.FokusPengabdian;
using UnpakSipaksi.Modules.FokusPengabdian.Infrastructure.PublicApi;
using Xunit;

namespace UnpakSipaksi.Modules.FokusPengabdian.PublicApiTest
{
    public sealed class FokusPengabdianApiTests
    {
        private readonly Mock<ISender> _senderMock;
        private readonly FokusPengabdianApi _api;

        public FokusPengabdianApiTests()
        {
            _senderMock = new Mock<ISender>();
            _api = new FokusPengabdianApi(_senderMock.Object);
        }

        [Fact]
        public async Task GetAsync_ShouldReturnNull_WhenResultIsFailure()
        {
            // Arrange
            Guid uuid = Guid.NewGuid();

            _senderMock
                .Setup(s => s.Send(It.IsAny<GetFokusPengabdianDefaultQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(Result.Failure<FokusPengabdianDefaultResponse>(FokusPengabdianErrors.NotFound(uuid)));

            // Act
            var result = await _api.GetAsync(uuid);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetAsync_ShouldReturnResponse_WhenResultIsSuccess()
        {
            // Arrange
            Guid uuid = Guid.NewGuid();

            var response = new FokusPengabdianDefaultResponse
            {
                Id = "1",
                Uuid = uuid.ToString(),
                Nama = "Teknik Informatika"
            };

            _senderMock
                .Setup(s => s.Send(It.IsAny<GetFokusPengabdianDefaultQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(Result<FokusPengabdianDefaultResponse>.Success(response));

            // Act
            var result = await _api.GetAsync(uuid);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(response.Id, result!.Id);
            Assert.Equal(response.Uuid, result.Uuid);
            Assert.Equal(response.Nama, result.Nama);
        }
    }
}
