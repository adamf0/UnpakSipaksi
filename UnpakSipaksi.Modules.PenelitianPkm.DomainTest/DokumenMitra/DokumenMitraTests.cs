using FluentAssertions;
using Moq;
using UnpakSipaksi.Modules.PenelitianPkm.Domain.PenelitianPkm;

namespace UnpakSipaksi.Modules.PenelitianPkm.DomainTest.DokumenMitra
{
    public class DokumenMitraTests
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

        [Theory]
        [InlineData("Universitas A", "Jawa Barat", "Bogor", 1, "Dr. A", "081234567890", "file.pdf")]
        public async void Create_Should_Return_Success_When_Valid(
            string mitra,
            string provinsi,
            string kota,
            int kelompokMitraId,
            string pemimpinMitra,
            string? kontakMitra,
            string file)
        {
            // Arrange
            var existingPenelitianPkm = await CreateValidHibah();

            // Act
            var result = Domain.DokumenMitra.DokumenMitra.Create(
                existingPenelitianPkm.Uuid,
                existingPenelitianPkm,
                mitra,
                provinsi,
                kota,
                kelompokMitraId,
                pemimpinMitra,
                kontakMitra,
                file
            );

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Mitra.Should().Be(mitra);
            result.Value.Provinsi.Should().Be(provinsi);
            result.Value.Kota.Should().Be(kota);
            result.Value.KelompokMitraId.Should().Be(kelompokMitraId);
            result.Value.PemimpinMitra.Should().Be(pemimpinMitra);
            result.Value.KontakMitra.Should().Be(kontakMitra);
            result.Value.File.Should().Be(file);
        }

        [Theory]
        [InlineData("", "Jawa Barat", "Bogor", 1, "Dr. A", "081234567890", "file.pdf")] // mitra kosong
        [InlineData("Universitas A", "", "Bogor", 1, "Dr. A", "081234567890", "file.pdf")] // provinsi kosong
        [InlineData("Universitas A", "Jawa Barat", "", 1, "Dr. A", "081234567890", "file.pdf")] // kota kosong
        [InlineData("Universitas A", "Jawa Barat", "Bogor", 0, "Dr. A", "081234567890", "file.pdf")] // kelompok mitra 0
        [InlineData("Universitas A", "Jawa Barat", "Bogor", 1, "", "081234567890", "file.pdf")] // pemimpin mitra kosong
        [InlineData("Universitas A", "Jawa Barat", "Bogor", 1, "Dr. A", "081234567890", "")] // file kosong
        [InlineData("Universitas A", "Jawa Barat", "Bogor", 1, "Dr. A", "081234567890", "")] // file bukan dari google drive
        public void Create_Should_Return_Failure_When_Invalid(
            string mitra,
            string provinsi,
            string kota,
            int kelompokMitraId,
            string pemimpinMitra,
            string? kontakMitra,
            string file)
        {
            // Arrange
            var uuidPenelitianPkm = Guid.NewGuid();
            Domain.PenelitianPkm.PenelitianPkm? existingPenelitianPkm = null;

            // Act
            var result = Domain.DokumenMitra.DokumenMitra.Create(
                uuidPenelitianPkm,
                existingPenelitianPkm,
                mitra,
                provinsi,
                kota,
                kelompokMitraId,
                pemimpinMitra,
                kontakMitra,
                file
            );

            // Assert
            result.IsFailure.Should().BeTrue();
        }
    }
}
