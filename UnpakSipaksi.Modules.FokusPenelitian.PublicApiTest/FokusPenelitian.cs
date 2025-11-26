using MediatR;
using Moq;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.FokusPenelitian.Application.GetFokusPenelitian;
using UnpakSipaksi.Modules.FokusPenelitian.Domain.FokusPenelitian;
using UnpakSipaksi.Modules.FokusPenelitian.Infrastructure.PublicApi;
using Xunit;

namespace UnpakSipaksi.Modules.FokusPenelitian.PublicApiTest
{
    public sealed class FokusPenelitianApiTests
    {
        private readonly Mock<ISender> _senderMock;
        private readonly FokusPenelitianApi _api;

        public FokusPenelitianApiTests()
        {
            _senderMock = new Mock<ISender>();
            _api = new FokusPenelitianApi(_senderMock.Object);
        }

        [Fact]
        public async Task GetAsync_ShouldReturnNull_WhenResultIsFailure()
        {
            // Arrange
            Guid uuid = Guid.NewGuid();

            _senderMock
                .Setup(s => s.Send(It.IsAny<GetFokusPenelitianDefaultQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(Result.Failure<FokusPenelitianDefaultResponse>(FokusPenelitianErrors.NotFound(uuid)));

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

            var response = new FokusPenelitianDefaultResponse
            {
                Id = "1",
                Uuid = uuid.ToString(),
                Nama = "Teknik Informatika"
            };

            _senderMock
                .Setup(s => s.Send(It.IsAny<GetFokusPenelitianDefaultQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(Result<FokusPenelitianDefaultResponse>.Success(response));

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
