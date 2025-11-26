using MediatR;
using Moq;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.RelevansiKualitasReferensi.Application.GetBobotRelevansiKualitasReferensi;
using UnpakSipaksi.Modules.RelevansiKualitasReferensi.Application.GetRelevansiKualitasReferensi;
using UnpakSipaksi.Modules.RelevansiKualitasReferensi.Domain.RelevansiKualitasReferensi;
using UnpakSipaksi.Modules.RelevansiKualitasReferensi.Infrastructure.PublicApi;
using Xunit;

namespace UnpakSipaksi.Modules.RelevansiKualitasReferensi.PublicApiTest
{
    public class RelevansiKualitasReferensiApiTests
    {
        private readonly Mock<ISender> _sender = new();

        private RelevansiKualitasReferensiApi CreateApi() =>
            new RelevansiKualitasReferensiApi(_sender.Object);

        [Fact]
        public async Task GetAsync_ShouldReturnResponse_WhenSuccess()
        {
            // Arrange
            var uuid = Guid.NewGuid();
            var response = new RelevansiKualitasReferensiDefaultResponse
            {
                Id = "RLV-123",
                Uuid = uuid.ToString(),
                Nama = "Relevansi Test",
                BobotPDP = 10,
                BobotTerapan = 20,
                BobotKerjasama = 30,
                BobotPenelitianDasar = 40,
                Skor = 100
            };

            _sender.Setup(s => s.Send(
                It.Is<GetRelevansiKualitasReferensiDefaultQuery>(x => x.RelevansiKualitasReferensiUuid == uuid),
                It.IsAny<CancellationToken>())
            ).ReturnsAsync(Result.Success(response));

            var api = CreateApi();

            // Act
            var result = await api.GetAsync(uuid);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("RLV-123", result.Id);
            Assert.Equal(uuid.ToString(), result.Uuid);
            Assert.Equal("Relevansi Test", result.Nama);
            Assert.Equal(10, result.BobotPDP);
            Assert.Equal(20, result.BobotTerapan);
            Assert.Equal(30, result.BobotKerjasama);
            Assert.Equal(40, result.BobotPenelitianDasar);
            Assert.Equal(100, result.Skor);
        }

        [Fact]
        public async Task GetAsync_ShouldReturnNull_WhenFailure()
        {
            // Arrange
            var uuid = Guid.NewGuid();

            _sender.Setup(s => s.Send(
                It.IsAny<GetRelevansiKualitasReferensiDefaultQuery>(),
                It.IsAny<CancellationToken>())
            ).ReturnsAsync(Result.Failure<RelevansiKualitasReferensiDefaultResponse>(RelevansiKualitasReferensiErrors.NotFound(Guid.NewGuid())));

            var api = CreateApi();

            // Act
            var result = await api.GetAsync(uuid);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetBobotWithoutTargetAsync_ShouldReturnValue_WhenSuccess()
        {
            // Arrange
            const string kategori = "PDP";

            _sender.Setup(s => s.Send(
                It.Is<GetBobotRelevansiKualitasReferensiQuery>(x => x.KategoriSkema == kategori),
                It.IsAny<CancellationToken>())
            ).ReturnsAsync(Result.Success<int?>(12));

            var api = CreateApi();

            // Act
            var result = await api.GetBobotWithoutTargetAsync(kategori);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(12, result);
        }

        [Fact]
        public async Task GetBobotWithoutTargetAsync_ShouldReturnNull_WhenFailure()
        {
            // Arrange
            const string kategori = "PDP";

            _sender.Setup(s => s.Send(
                It.IsAny<GetBobotRelevansiKualitasReferensiQuery>(),
                It.IsAny<CancellationToken>())
            ).ReturnsAsync(Result.Failure<int?>(RelevansiKualitasReferensiErrors.UnknownKategoriSkema()));

            var api = CreateApi();

            // Act
            var result = await api.GetBobotWithoutTargetAsync(kategori);

            // Assert
            Assert.Null(result);
        }
    }
}
