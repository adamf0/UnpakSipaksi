using MediatR;
using Moq;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.JenisLuaran.Application.GetJenisLuaran;
using UnpakSipaksi.Modules.JenisLuaran.Domain;
using UnpakSipaksi.Modules.JenisLuaran.Infrastructure.PublicApi;
using Xunit;

namespace UnpakSipaksi.Modules.JenisLuaran.PublicApiTest
{
    public sealed class JenisLuaranApiTests
    {
        private readonly Mock<ISender> _senderMock;
        private readonly JenisLuaranApi _api;

        public JenisLuaranApiTests()
        {
            _senderMock = new Mock<ISender>();
            _api = new JenisLuaranApi(_senderMock.Object);
        }

        [Fact]
        public async Task GetAsync_ShouldReturnNull_WhenResultIsFailure()
        {
            // Arrange
            Guid uuid = Guid.NewGuid();

            _senderMock
                .Setup(s => s.Send(It.IsAny<GetJenisLuaranDefaultQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(Result.Failure<JenisLuaranDefaultResponse>(JenisLuaranErrors.NotFound(uuid)));

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

            var response = new JenisLuaranDefaultResponse
            {
                Id = "1",
                Uuid = uuid.ToString(),
                Nama = "Teknik Informatika"
            };

            _senderMock
                .Setup(s => s.Send(It.IsAny<GetJenisLuaranDefaultQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(Result<JenisLuaranDefaultResponse>.Success(response));

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
