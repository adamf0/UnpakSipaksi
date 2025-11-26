using MediatR;
using Moq;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.Rirn.Application.GetRirn;
using UnpakSipaksi.Modules.Rirn.Domain.Rirn;
using UnpakSipaksi.Modules.Rirn.Infrastructure.PublicApi;
using UnpakSipaksi.Modules.Rirn.PublicApi;
using Xunit;

namespace UnpakSipaksi.Modules.Rirn.PublicApiTest
{
    public class RirnApiTests
    {
        private readonly Mock<ISender> _senderMock;
        private readonly IRirnApi _api;

        public RirnApiTests()
        {
            _senderMock = new Mock<ISender>();
            _api = new RirnApi(_senderMock.Object);
        }

        [Fact]
        public async Task GetAsync_ShouldReturnNull_WhenFailure()
        {
            // Arrange
            _senderMock
                .Setup(s => s.Send(
                    It.IsAny<GetRirnDefaultQuery>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(Result.Failure<RirnDefaultResponse>(RirnErrors.NotFound(Guid.NewGuid())));

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
            var response = new RirnDefaultResponse
            {
                Id = "A123",
                Uuid = uuid.ToString(),
                Nama = "Nama RIRN"
            };

            _senderMock
                .Setup(s => s.Send(
                    It.IsAny<GetRirnDefaultQuery>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(Result.Success(response));

            // Act
            var result = await _api.GetAsync(uuid);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("A123", result.Id);
            Assert.Equal(uuid.ToString(), result.Uuid);
            Assert.Equal("Nama RIRN", result.Nama);
        }
    }
}
