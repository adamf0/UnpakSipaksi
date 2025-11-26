using MediatR;
using Moq;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.AkurasiPenelitian.Domain.AkurasiPenelitian;
using UnpakSipaksi.Modules.Satuan.Application.GetSatuan;
using UnpakSipaksi.Modules.Satuan.Infrastructure.PublicApi;
using Xunit;
using SatuanResponseApi = UnpakSipaksi.Modules.Satuan.PublicApi.SatuanResponse;

namespace UnpakSipaksi.Modules.Satuan.Tests.PublicApiTest
{
    public sealed class SatuanApiTests
    {
        private readonly Mock<ISender> _senderMock;
        private readonly SatuanApi _api;

        public SatuanApiTests()
        {
            _senderMock = new Mock<ISender>();
            _api = new SatuanApi(_senderMock.Object);
        }

        [Fact]
        public async Task GetAsync_ShouldReturnResponse_WhenSuccess()
        {
            // Arrange
            Guid uuid = Guid.NewGuid();
            var response = new SatuanDefaultResponse
            {
                Id = "10",
                Uuid = uuid.ToString(),
                Nama = "Satuan A"
            };

            _senderMock
                .Setup(s => s.Send(It.IsAny<GetSatuanDefaultQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(Result.Success(response));

            // Act
            SatuanResponseApi? result = await _api.GetAsync(uuid);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("10", result.Id);
            Assert.Equal(uuid.ToString(), result.Uuid);
            Assert.Equal("Satuan A", result.Nama);
        }

        [Fact]
        public async Task GetAsync_ShouldReturnNull_WhenFailure()
        {
            // Arrange
            _senderMock
                .Setup(s => s.Send(It.IsAny<GetSatuanDefaultQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(Result.Failure<SatuanDefaultResponse>(AkurasiPenelitianErrors.NotFound(Guid.NewGuid())));

            // Act
            SatuanResponseApi? result = await _api.GetAsync(Guid.NewGuid());

            // Assert
            Assert.Null(result);
        }
    }
}
