using MediatR;
using Moq;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.RumpunIlmu3.Application.GetRumpunIlmu3;
using UnpakSipaksi.Modules.RumpunIlmu3.Domain.RumpunIlmu3;
using UnpakSipaksi.Modules.RumpunIlmu3.Infrastructure.PublicApi;
using Xunit;

namespace UnpakSipaksi.Modules.RumpunIlmu3.PublicApiTest
{
    public class RumpunIlmu3ApiTests
    {
        private readonly Mock<ISender> _senderMock;
        private readonly RumpunIlmu3Api _api;

        public RumpunIlmu3ApiTests()
        {
            _senderMock = new Mock<ISender>();
            _api = new RumpunIlmu3Api(_senderMock.Object);
        }

        [Fact]
        public async Task GetAsync_ShouldReturnNull_WhenFailure()
        {
            // Arrange
            _senderMock
                .Setup(s => s.Send(It.IsAny<GetRumpunIlmu3DefaultQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(Result.Failure<RumpunIlmu3DefaultResponse>(RumpunIlmu3Errors.NotFound(Guid.NewGuid())));

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

            var response = new RumpunIlmu3DefaultResponse
            {
                Id = "123",
                Uuid = uuid,
                Nama = "Nama Rumpun Ilmu"
            };

            _senderMock
                .Setup(s => s.Send(It.IsAny<GetRumpunIlmu3DefaultQuery>(), It.IsAny<CancellationToken>()))
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
