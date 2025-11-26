using MediatR;
using Moq;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.RumpunIlmu1.Application.GetRumpunIlmu1;
using UnpakSipaksi.Modules.RumpunIlmu1.Domain.RumpunIlmu1;
using UnpakSipaksi.Modules.RumpunIlmu1.Infrastructure.PublicApi;
using UnpakSipaksi.Modules.RumpunIlmu1.Application.GetRumpunIlmu1;
using UnpakSipaksi.Modules.RumpunIlmu1.Domain.RumpunIlmu1;
using Xunit;

namespace UnpakSipaksi.Modules.RumpunIlmu1.PublicApiTest
{
    public sealed class RumpunIlmu1ApiTests
    {
        private readonly Mock<ISender> _senderMock;
        private readonly RumpunIlmu1Api _api;

        public RumpunIlmu1ApiTests()
        {
            _senderMock = new Mock<ISender>();
            _api = new RumpunIlmu1Api(_senderMock.Object);
        }

        [Fact]
        public async Task GetAsync_ShouldReturnNull_WhenResultIsFailure()
        {
            // Arrange
            Guid uuid = Guid.NewGuid();

            _senderMock
                .Setup(s => s.Send(It.IsAny<GetRumpunIlmu1DefaultQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(Result.Failure<RumpunIlmu1DefaultResponse>(RumpunIlmu1Errors.NotFound(uuid)));

            // Act
            var result = await _api.GetAsync(uuid);

            // Assert
            Assert.Null(result);

            _senderMock.Verify(s =>
                s.Send(It.Is<GetRumpunIlmu1DefaultQuery>(q => q.Uuid == uuid),
                It.IsAny<CancellationToken>()),
                Times.Once);
        }

        [Fact]
        public async Task GetAsync_ShouldReturnResponse_WhenResultIsSuccess()
        {
            // Arrange
            Guid uuid = Guid.NewGuid();

            var response = new RumpunIlmu1DefaultResponse
            {
                Id = "1",
                Uuid = uuid.ToString(),
                Nama = "Teknik Informatika"
            };

            _senderMock
                .Setup(s => s.Send(It.IsAny<GetRumpunIlmu1DefaultQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(Result<RumpunIlmu1DefaultResponse>.Success(response));

            // Act
            var result = await _api.GetAsync(uuid);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(response.Id, result!.Id);
            Assert.Equal(response.Uuid, result.Uuid);
            Assert.Equal(response.Nama, result.Nama);

            _senderMock.Verify(s =>
                s.Send(It.Is<GetRumpunIlmu1DefaultQuery>(q => q.Uuid == uuid),
                It.IsAny<CancellationToken>()),
                Times.Once);
        }
    }
}
