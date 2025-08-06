using FluentAssertions;
using UnpakSipaksi.Modules.PenelitianHibah.Domain.RAB;
using UnpakSipaksi.Modules.PenelitianHibah.Domain.PenelitianHibah;
using Moq;

namespace UnpakSipaksi.Modules.PenelitianHibah.DomainTest.RAB
{
    public class RABTests
    {
        [Fact]
        public async void Create_ShouldSucceed_WhenValidInput()
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

            var hibah = await Domain.PenelitianHibah.PenelitianHibah.Create(mockRepo.Object, "123", "2024-01-01", "judul");

            // Act
            var result = Domain.RAB.RAB.Create(
                Guid.NewGuid(),
                hibah.Value,
                KelompokRabId: 1,
                KomponenId: 2,
                Item: 10,
                SatuanId: 3,
                HargaSatuan: 5000,
                Total: 50000
            );

            result.IsSuccess.Should().BeTrue();
            result.Value.Total.Should().Be(50000);
        }

        [Fact]
        public void Create_ShouldFail_WhenPenelitianHibahIsNull()
        {
            Guid id = Guid.NewGuid();
            var result = Domain.RAB.RAB.Create(
                id,
                null,
                KelompokRabId: 1,
                KomponenId: 2,
                Item: 10,
                SatuanId: 3,
                HargaSatuan: 5000,
                Total: 50000
            );

            result.IsFailure.Should().BeTrue();
            result.Error.Code.Should().Be("RAB.NotFoundHibah");
            result.Error.Description.Should().Be($"Penelitian hibah with identifier {id} not found");
        }

        [Fact]
        public async void Create_ShouldFail_WhenTotalMismatch()
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

            var hibah = await Domain.PenelitianHibah.PenelitianHibah.Create(mockRepo.Object, "123", "2024-01-01", "judul");

            // Act
            var result = Domain.RAB.RAB.Create(
                Guid.NewGuid(),
                hibah.Value,
                KelompokRabId: 1,
                KomponenId: 2,
                Item: 10,
                SatuanId: 3,
                HargaSatuan: 5000,
                Total: 12345 // mismatch
            );

            result.IsFailure.Should().BeTrue();
            result.Error.Code.Should().Be("RAB.InvalidTotal");
            result.Error.Description.Should().Be("Total value does not match calculation");
        }

        [Fact]
        public async void Update_ShouldSucceed_WhenValid()
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

            var hibah = await Domain.PenelitianHibah.PenelitianHibah.Create(mockRepo.Object, "123", "2024-01-01", "judul");
            var existing = Domain.RAB.RAB.Create(Guid.NewGuid(), hibah.Value, 1, 1, 2, 3, 1000, 2000).Value;

            // Act
            var result = Domain.RAB.RAB.Update(
                uuid: existing.Uuid,
                UuidPenelitianHibah: Guid.NewGuid(),
                existingPenelitianHibah: hibah.Value,
                prev: existing,
                KelompokRabId: 1,
                KomponenId: 2,
                Item: 3,
                SatuanId: 4,
                HargaSatuan: 1000,
                Total: 3000
            );

            result.IsSuccess.Should().BeTrue();
            result.Value.Total.Should().Be(3000);
            result.Value.Item.Should().Be(3);
        }

        [Fact]
        public async void Update_ShouldFail_WhenPrevIsNull()
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

            var hibah = await Domain.PenelitianHibah.PenelitianHibah.Create(mockRepo.Object, "123", "2024-01-01", "judul");

            // Act
            Guid id = Guid.NewGuid();
            var result = Domain.RAB.RAB.Update(
                uuid: id,
                UuidPenelitianHibah: Guid.NewGuid(),
                existingPenelitianHibah: hibah.Value,
                prev: null,
                KelompokRabId: 1,
                KomponenId: 2,
                Item: 3,
                SatuanId: 4,
                HargaSatuan: 1000,
                Total: 3000
            );

            result.IsFailure.Should().BeTrue();
            result.Error.Code.Should().Be("RAB.NotFound");
            result.Error.Description.Should().Be($"RAB with identifier {id} not found");
        }

        [Fact]
        public async void Update_ShouldFail_WhenTotalMismatch()
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

            var hibah = await Domain.PenelitianHibah.PenelitianHibah.Create(mockRepo.Object, "123", "2024-01-01", "judul");
            var existing = Domain.RAB.RAB.Create(Guid.NewGuid(), hibah.Value, 1, 1, 2, 3, 1000, 2000).Value;

            // Act
            var result = Domain.RAB.RAB.Update(
                uuid: existing.Uuid,
                UuidPenelitianHibah: Guid.NewGuid(),
                existingPenelitianHibah: hibah.Value,
                prev: existing,
                KelompokRabId: 1,
                KomponenId: 2,
                Item: 3,
                SatuanId: 4,
                HargaSatuan: 1000,
                Total: 5000 // mismatch
            );

            result.IsFailure.Should().BeTrue();
            result.Error.Code.Should().Be("RAB.InvalidTotal");
            result.Error.Description.Should().Be("Total value does not match calculation");
        }

        [Fact]
        public async void Update_ShouldFail_WhenHibahMismatch()
        {
            // Arrange
            var mockRepo1 = new Mock<IPenelitianHibahRepository>();
            mockRepo1.Setup(x =>
                x.HasUniqueDataAsync(
                    It.IsAny<Guid?>(),
                    It.Is<string>(s => s == "123"),
                    It.Is<string>(j => j == "judul1"),
                    It.IsAny<CancellationToken>()
                )
            ).ReturnsAsync(true);

            var mockRepo2 = new Mock<IPenelitianHibahRepository>();
            mockRepo2.Setup(x =>
                x.HasUniqueDataAsync(
                    It.IsAny<Guid?>(),
                    It.Is<string>(s => s == "123"),
                    It.Is<string>(j => j == "judul2"),
                    It.IsAny<CancellationToken>()
                )
            ).ReturnsAsync(true);

            var hibah1 = await Domain.PenelitianHibah.PenelitianHibah.Create(mockRepo1.Object, "123", "2024-01-01", "judul1");
            var hibah2 = await Domain.PenelitianHibah.PenelitianHibah.Create(mockRepo2.Object, "123", "2024-01-01", "judul2");

            hibah1.IsSuccess.Should().BeTrue();
            hibah1.Value.GetType()?.GetProperty("Id")?.SetValue(hibah1.Value, 1);

            hibah2.IsSuccess.Should().BeTrue();
            hibah2.Value.GetType()?.GetProperty("Id")?.SetValue(hibah2.Value, 2);

            var createResult = Domain.RAB.RAB.Create(Guid.NewGuid(), hibah1.Value, 1, 1, 2, 3, 1000, 2000);
            createResult.IsSuccess.Should().BeTrue();
            var existing = createResult.Value;

            // Act
            var result = Domain.RAB.RAB.Update(
                uuid: existing.Uuid,
                UuidPenelitianHibah: Guid.NewGuid(),
                existingPenelitianHibah: hibah2.Value, // different ID
                prev: existing,
                KelompokRabId: 1,
                KomponenId: 2,
                Item: 2,
                SatuanId: 3,
                HargaSatuan: 1000,
                Total: 2000
            );

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Code.Should().Be("RAB.InvalidData");
            result.Error.Description.Should().Be("Hibah penelitian is not match existing data");
        }
    }
}
