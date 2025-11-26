using MediatR;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.PrioritasRiset.Application.GetPrioritasRiset;
using UnpakSipaksi.Modules.PrioritasRiset.Domain.PrioritasRiset;
using UnpakSipaksi.Modules.PrioritasRiset.Infrastructure.PublicApi;
using UnpakSipaksi.Modules.PrioritasRiset.PublicApi;
using Xunit;
using PrioritasRisetResponseApi = UnpakSipaksi.Modules.PrioritasRiset.PublicApi.PrioritasRisetResponse;

namespace UnpakSipaksi.Modules.PrioritasRiset.PublicApiTest
{
    public sealed class PrioritasRisetApiTests
    {
        private readonly Mock<ISender> _senderMock;
        private readonly PrioritasRisetApi _api;

        public PrioritasRisetApiTests()
        {
            _senderMock = new Mock<ISender>();
            _api = new PrioritasRisetApi(_senderMock.Object);
        }

        [Fact]
        public async Task GetAsync_WhenSuccess_ReturnsResponse()
        {
            // Arrange
            Guid uuid = Guid.NewGuid();
            var response = new PrioritasRisetDefaultResponse
            {
                Id = "10",
                Uuid = uuid.ToString(),
                Nama = "Riset Unggulan"
            };

            _senderMock
                .Setup(s => s.Send(
                    It.Is<GetPrioritasRisetDefaultQuery>(q => q.PrioritasRisetUuid == uuid),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(Result.Success(response));

            // Act
            PrioritasRisetResponseApi result = await _api.GetAsync(uuid, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("10", result.Id);
            Assert.Equal(uuid.ToString(), result.Uuid);
            Assert.Equal("Riset Unggulan", result.Nama);
        }

        [Fact]
        public async Task GetAsync_WhenFailure_ReturnsNull()
        {
            // Arrange
            Guid uuid = Guid.NewGuid();

            _senderMock
                .Setup(s => s.Send(
                    It.Is<GetPrioritasRisetDefaultQuery>(q => q.PrioritasRisetUuid == uuid),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(Result.Failure<PrioritasRisetDefaultResponse>(PrioritasRisetErrors.NotFound(Guid.NewGuid())));

            // Act
            var result = await _api.GetAsync(uuid, CancellationToken.None);

            // Assert
            Assert.Null(result);
        }
    }
}
