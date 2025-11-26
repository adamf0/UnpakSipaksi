using MediatR;
using Moq;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KelompokRab.Application.GetKelompokRab;
using UnpakSipaksi.Modules.KelompokRab.Domain.KelompokRab;
using UnpakSipaksi.Modules.KelompokRab.Infrastructure.PublicApi;
using Xunit;

namespace UnpakSipaksi.Modules.KelompokRab.PublicApiTest
{
    public sealed class KelompokRabApiTests
    {
        private readonly Mock<ISender> _senderMock;
        private readonly KelompokRabApi _api;

        public KelompokRabApiTests()
        {
            _senderMock = new Mock<ISender>();
            _api = new KelompokRabApi(_senderMock.Object);
        }

        [Fact]
        public async Task GetAsync_ShouldReturnNull_WhenResultIsFailure()
        {
            // Arrange
            Guid uuid = Guid.NewGuid();

            _senderMock
                .Setup(s => s.Send(It.IsAny<GetKelompokRabDefaultQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(Result.Failure<KelompokRabDefaultResponse>(KelompokRabErrors.NotFound(uuid)));

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

            var response = new KelompokRabDefaultResponse
            {
                Id = "1",
                Uuid = uuid.ToString(),
                Nama = "Teknik Informatika"
            };

            _senderMock
                .Setup(s => s.Send(It.IsAny<GetKelompokRabDefaultQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(Result<KelompokRabDefaultResponse>.Success(response));

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
