using MediatR;
using Moq;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.RumpunIlmu2.Application.GetRumpunIlmu2;
using UnpakSipaksi.Modules.RumpunIlmu2.Domain.RumpunIlmu2;
using UnpakSipaksi.Modules.RumpunIlmu2.Infrastructure.PublicApi;
using Xunit;

namespace UnpakSipaksi.Modules.RumpunIlmu2.PublicApiTest
{
    public class RumpunIlmu2ApiTests
    {
        private readonly Mock<ISender> _senderMock;
        private readonly RumpunIlmu2Api _api;

        public RumpunIlmu2ApiTests()
        {
            _senderMock = new Mock<ISender>();
            _api = new RumpunIlmu2Api(_senderMock.Object);
        }

        [Fact]
        public async Task GetAsync_ShouldReturnNull_WhenFailure()
        {
            // Arrange
            _senderMock
                .Setup(s => s.Send(It.IsAny<GetRumpunIlmu2DefaultQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(Result.Failure<RumpunIlmu2DefaultResponse>(RumpunIlmu2Errors.NotFound(Guid.NewGuid())));

            // Act
            var result = await _api.GetAsync(Guid.NewGuid());

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetAsync_ShouldReturnResponse_WhenSuccess()
        {
            // Arrange
            var uuid = Guid.NewGuid().ToString();

            var response = new RumpunIlmu2DefaultResponse
            {
                Id = "123",
                Uuid = uuid,
                Nama = "Nama Rumpun Ilmu"
            };

            _senderMock
                .Setup(s => s.Send(It.IsAny<GetRumpunIlmu2DefaultQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(Result.Success(response));

            // Act
            var result = await _api.GetAsync(Guid.Parse(uuid));

            // Assert
            Assert.NotNull(result);
            Assert.Equal("123", result.Id);
            Assert.Equal(uuid, result.Uuid);
            Assert.Equal("Nama Rumpun Ilmu", result.Nama);
        }
    }
}
