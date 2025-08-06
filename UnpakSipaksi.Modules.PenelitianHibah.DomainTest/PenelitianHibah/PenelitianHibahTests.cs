using FluentAssertions;
using Moq;
using UnpakSipaksi.Modules.PenelitianHibah.Domain.PenelitianHibah;

namespace UnpakSipaksi.Modules.PenelitianHibah.DomainTest.PenelitianHibah
{
    public class PenelitianHibahTests
    {
        [Fact]
        public async Task Create_ShouldReturnSuccess_WhenValid()
        {
            // Arrange
            var mockRepo = new Mock<IPenelitianHibahRepository>();
            mockRepo.Setup(x =>
                x.HasUniqueDataAsync(
                    It.IsAny<Guid?>(),
                    It.Is<string>(s => s == "123"),
                    It.Is<string>(j => j == "judul"),
                    It.IsAny<CancellationToken>()
                )
            ).ReturnsAsync(true);

            // Act
            var result = await Domain.PenelitianHibah.PenelitianHibah.Create(mockRepo.Object, "123", "2024-01-01", "judul");

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.NIDN.Should().Be("123");
            result.Value.Judul.Should().Be("judul");
            result.Value.Status.Should().Be("Draf");
        }

        [Fact]
        public async Task Create_ShouldFail_WhenDuplicate()
        {
            var mockRepo = new Mock<IPenelitianHibahRepository>();
            mockRepo.Setup(x =>
                x.HasUniqueDataAsync(
                    It.IsAny<Guid?>(),
                    It.Is<string>(s => s == "123"),
                    It.Is<string>(j => j == "judul"),
                    It.IsAny<CancellationToken>()
                )
            ).ReturnsAsync(false);

            var result = await Domain.PenelitianHibah.PenelitianHibah.Create(mockRepo.Object, "123", "2024-01-01", "judul");

            result.IsFailure.Should().BeTrue();
            result.Error.Code.Should().Be("PenelitianHibah.NotUnique");
        }

        [Theory]
        [InlineData(null)]
        [InlineData("invalid-date")]
        public async Task Create_ShouldFail_WhenInvalidDate(string tahun)
        {
            var mockRepo = new Mock<IPenelitianHibahRepository>();
            mockRepo.Setup(x =>
                x.HasUniqueDataAsync(
                    It.IsAny<Guid?>(),
                    It.Is<string>(s => s == "123"),
                    It.Is<string>(j => j == "judul"),
                    It.IsAny<CancellationToken>()
                )
            ).ReturnsAsync(true);

            var result = await Domain.PenelitianHibah.PenelitianHibah.Create(mockRepo.Object, "123", tahun, "judul");

            result.IsFailure.Should().BeTrue();
            result.Error.Code.Should().Be("PenelitianHibah.InvalidFormatTahunPengajuan");
        }

        [Fact]
        public async void Update_ShouldReturnSuccess_WhenValid()
        {
            var prev = await CreateDummy();
            var result = Domain.PenelitianHibah.PenelitianHibah.Update(prev, "2023-01-01", "Updated Judul");
            result.IsSuccess.Should().BeTrue();
            result.Value.Judul.Should().Be("Updated Judul");
        }

        [Fact]
        public async void Update_ShouldFail_WhenInvalidDate()
        {
            var prev = await CreateDummy();
            var result = Domain.PenelitianHibah.PenelitianHibah.Update(prev, "invalid-date", "Updated Judul");
            result.IsFailure.Should().BeTrue();
            result.Error.Code.Should().Be("PenelitianHibah.InvalidFormatTahunPengajuan");
        }

        [Fact]
        public async void UpdateSkema_ShouldReturnSuccess()
        {
            var prev = await CreateDummy();
            var result = Domain.PenelitianHibah.PenelitianHibah.UpdateSkema(prev, 1, 2, 3);
            result.IsSuccess.Should().BeTrue();
            result.Value.TKT.Should().Be(2);
        }

        [Fact]
        public async void UpdateRiset_ShouldReturnSuccess()
        {
            var prev = await CreateDummy();
            var result = Domain.PenelitianHibah.PenelitianHibah.UpdateRiset(prev, 1, 2, 3, 4);
            result.IsSuccess.Should().BeTrue();
            result.Value.PrioritasRiset.Should().Be(1);
        }

        [Fact]
        public async void UpdateRumpunIlmu_ShouldReturnSuccess()
        {
            var prev = await CreateDummy();
            var result = Domain.PenelitianHibah.PenelitianHibah.UpdateRumpunIlmu(prev, 1, 2, 3);
            result.IsSuccess.Should().BeTrue();
            result.Value.RumpunIlmu1Id.Should().Be(1);
        }

        [Fact]
        public async void UpdateLamaKegiatan_ShouldReturnSuccess()
        {
            var prev = await CreateDummy();
            var result = Domain.PenelitianHibah.PenelitianHibah.UpdateLamaKegiatan(prev, 24);
            result.IsSuccess.Should().BeTrue();
            result.Value.LamaKegiatan.Should().Be(24);
        }

        [Fact]
        public async void UpdateStatus_ShouldReturnSuccess()
        {
            var prev = await CreateDummy();
            var result = Domain.PenelitianHibah.PenelitianHibah.UpdateStatus(prev, "Tolak");
            result.IsSuccess.Should().BeTrue();
            result.Value.Status.Should().Be("Tolak");
        }

        [Fact]
        public async void UpdateStatus_ShouldFail_WhenInvalidEnum()
        {
            var prev = await CreateDummy();
            var result = Domain.PenelitianHibah.PenelitianHibah.UpdateStatus(prev, "InvalidStatus");
            result.IsFailure.Should().BeTrue();
            result.Error.Code.Should().Be("PenelitianHibah.InvalidStatus");
        }

        private async Task<Domain.PenelitianHibah.PenelitianHibah> CreateDummy()
        {
            var mockRepo = new Mock<IPenelitianHibahRepository>();
            mockRepo.Setup(x => x.HasUniqueDataAsync(
                    It.IsAny<Guid?>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()
                ))
                .ReturnsAsync(true);

            var result = await Domain.PenelitianHibah.PenelitianHibah.Create(
                mockRepo.Object,
                "123",
                "2022-01-01",
                "judul"
            );

            return result.Value;
        }

    }
}
