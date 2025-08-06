using FluentAssertions;
using Moq;
using UnpakSipaksi.Modules.PenelitianPkm.Domain.PenelitianPkm;

namespace UnpakSipaksi.Modules.PenelitianPkm.DomainTest.MemberNonDosen
{
    public class MemberNonDosenTests
    {
        private static Mock<IPenelitianPkmRepository> CreateMockRepo(bool hasUniqueData = true)
        {
            var mock = new Mock<IPenelitianPkmRepository>();
            mock.Setup(x => x.HasUniqueDataAsync(
                It.IsAny<Guid?>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<CancellationToken>())
            ).ReturnsAsync(hasUniqueData);
            return mock;
        }

        private static void SetId<T>(T obj, int id)
        {
            typeof(T).GetProperty("Id")?.SetValue(obj, id);
        }

        private async Task<Domain.PenelitianPkm.PenelitianPkm> CreateValidHibah(int id = 1)
        {
            var repo = CreateMockRepo();
            var result = await Domain.PenelitianPkm.PenelitianPkm.Create(repo.Object, "123", "2024-01-01", "judul");
            result.IsSuccess.Should().BeTrue();

            var hibah = result.Value;
            SetId(hibah, id);
            return hibah;
        }

        [Fact]
        public void Create_ShouldReturnSuccess_WhenValid()
        {
            // Act
            var result = Domain.MemberNonDosen.MemberNonDosen.Create(1, "KTP123", "Siti Aminah", "PT X");

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.PenelitianPkmId.Should().Be(1);
            result.Value.NomorIdentitas.Should().Be("KTP123");
            result.Value.Nama.Should().Be("Siti Aminah");
            result.Value.Afiliasi.Should().Be("PT X");
            result.Value.Uuid.Should().NotBe(Guid.Empty);
        }

        [Fact]
        public void Create_ShouldReturnFailed_WhenInvalid()
        {
            // Act
            var result = Domain.MemberNonDosen.MemberNonDosen.Create(0, "KTP123", "Siti Aminah", "PT X");

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Code.Should().Be("MemberNonDosen.NotFoundHibah");
            result.Error.Description.Should().Be("Penelitian hibah not found");
        }

        [Fact]
        public async void Update_ShouldReturnSuccess_WhenEntityIsNotNull()
        {
            // Arrange
            var hibah = await CreateValidHibah();
            var member = Domain.MemberNonDosen.MemberNonDosen.Create(1, "123", "Budi", "LIPI").Value;

            // Act
            var result = Domain.MemberNonDosen.MemberNonDosen.Update(member, hibah, "456", "Agus", "BRIN");

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.NomorIdentitas.Should().Be("456");
            result.Value.Nama.Should().Be("Agus");
            result.Value.Afiliasi.Should().Be("BRIN");
        }

        [Fact]
        public async void Update_ShouldFail_WhenEntityIsNull()
        {
            // Act
            var hibah = await CreateValidHibah();
            var result = Domain.MemberNonDosen.MemberNonDosen.Update(null, hibah, "456", "Agus", "BRIN");

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Code.Should().Be("PenelitianPkm.EmptyData");
        }
    }
}
