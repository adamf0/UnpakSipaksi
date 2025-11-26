using MediatR;
using Moq;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.Kategori.Application.GetKategori;
using UnpakSipaksi.Modules.Kategori.Domain.Kategori;
using UnpakSipaksi.Modules.Kategori.Infrastructure.PublicApi;
using Xunit;

namespace UnpakSipaksi.Modules.Kategori.PublicApiTest
{
    public sealed class KategoriApiTests
    {
        private readonly Mock<ISender> _senderMock;
        private readonly KategoriApi _api;

        public KategoriApiTests()
        {
            _senderMock = new Mock<ISender>();
            _api = new KategoriApi(_senderMock.Object);
        }

        [Fact]
        public async Task GetAsync_ShouldReturnNull_WhenResultIsFailure()
        {
            // Arrange
            Guid uuid = Guid.NewGuid();

            _senderMock
                .Setup(s => s.Send(It.IsAny<GetKategoriDefaultQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(Result.Failure<KategoriDefaultResponse>(KategoriErrors.NotFound(uuid)));

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

            var response = new KategoriDefaultResponse
            {
                Id = "1",
                Uuid = uuid.ToString(),
                Nama = "Teknik Informatika"
            };

            _senderMock
                .Setup(s => s.Send(It.IsAny<GetKategoriDefaultQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(Result<KategoriDefaultResponse>.Success(response));

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
