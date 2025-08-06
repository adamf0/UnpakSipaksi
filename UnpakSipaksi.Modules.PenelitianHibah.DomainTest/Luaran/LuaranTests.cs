using System;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using UnpakSipaksi.Modules.PenelitianHibah.Domain.Luaran;
using UnpakSipaksi.Modules.PenelitianHibah.Domain.PenelitianHibah;
using Xunit;

namespace UnpakSipaksi.Modules.PenelitianHibah.DomainTest.Luaran
{
    public class LuaranTests
    {
        [Fact]
        public void Create_ShouldFail_WhenHibahIsNull()
        {
            // Arrange
            var uuid = Guid.NewGuid();

            // Act
            var result = Domain.Luaran.Luaran.Create(uuid, null, 1, 2, "Ket", "http://link", "Jenis");

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Code.Should().Be("Luaran.NotFoundHibah");
            result.Error.Description.Should().Be($"Penelitian hibah with identifier {uuid} not found");
        }

        [Fact]
        public async Task Create_ShouldSucceed_WhenValid()
        {
            // Arrange
            var hibah = await DummyHibah(10);

            // Act
            var result = Domain.Luaran.Luaran.Create(Guid.NewGuid(), hibah, 1, 2, "Keterangan", "http://link", "Jenis");

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.PenelitianHibahId.Should().Be(10);
            result.Value.Keterangan.Should().Be("Keterangan");
        }

        [Fact]
        public void Update_ShouldFail_WhenPrevIsNull()
        {
            // Arrange
            var uuid = Guid.NewGuid();

            // Act
            var result = Domain.Luaran.Luaran.Update(uuid, Guid.NewGuid(), null, null, 1, 2, "Keterangan", "Link", "Jenis");

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Code.Should().Be("Luaran.NotFound");
        }

        [Fact]
        public async Task Update_ShouldFail_WhenHibahIsNull()
        {
            // Arrange
            var validHibah = await DummyHibah(1);
            var existing = Domain.Luaran.Luaran.Create(Guid.NewGuid(), validHibah, 1, 2, "Ket", "http://link", "Jenis");

            existing.IsSuccess.Should().BeTrue();

            // Act
            var result = Domain.Luaran.Luaran.Update(
                existing.Value.Uuid,
                Guid.NewGuid(),
                null, 
                existing.Value,
                1, 
                2, 
                "Keterangan", 
                "Link", 
                "Jenis"
            );

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Code.Should().Be("Luaran.NotFoundHibah");
        }


        [Fact]
        public async Task Update_ShouldFail_WhenHibahMismatch()
        {
            // Arrange
            var hibah1 = await DummyHibah(1);
            var hibah2 = await DummyHibah(2);

            var existing = Domain.Luaran.Luaran.Create(Guid.NewGuid(), hibah1, 1, 2, "Ket", "http://link", "Jenis");

            // Act
            var result = Domain.Luaran.Luaran.Update(
                existing.Value.Uuid,
                Guid.NewGuid(),
                hibah2,
                existing.Value,
                1, 2, "Keterangan", "Link", "Jenis"
            );

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Code.Should().Be("Luaran.InvalidData");
        }

        [Fact]
        public async Task Update_ShouldSucceed_WhenValid()
        {
            // Arrange
            var hibah = await DummyHibah(88);
            var existing = Domain.Luaran.Luaran.Create(Guid.NewGuid(), hibah, 1, 2, "Ket", "http://link", "Jenis");

            // Act
            var result = Domain.Luaran.Luaran.Update(
                existing.Value.Uuid,
                Guid.NewGuid(),
                hibah,
                existing.Value,
                5, 6, "Updated", "http://updated", "NewJenis"
            );

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Keterangan.Should().Be("Updated");
            result.Value.Link.Should().Be("http://updated");
            result.Value.Jenis.Should().Be("NewJenis");
        }

        private static async Task<Domain.PenelitianHibah.PenelitianHibah> DummyHibah(int id)
        {
            var mockRepo = new Mock<IPenelitianHibahRepository>();
            mockRepo.Setup(x =>
                x.HasUniqueDataAsync(
                    It.IsAny<Guid?>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>())
            ).ReturnsAsync(true);

            var hibahResult = await Domain.PenelitianHibah.PenelitianHibah.Create(mockRepo.Object, "123", "2024-01-01", "judul");

            hibahResult.IsSuccess.Should().BeTrue();
            hibahResult.Value.GetType()?.GetProperty("Id")?.SetValue(hibahResult.Value, id);

            return hibahResult.Value;
        }
    }
}
