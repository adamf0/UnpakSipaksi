using FluentAssertions;
using Moq;
using UnpakSipaksi.Modules.ArtikelMediaMassa.Domain.ArtikelMediaMassa;

namespace UnpakSipaksi.Modules.ArtikelMediaMassa.DomainTest
{
    public class ArtikelMediaMassaTests
    {
        [Fact]
        public void Create_ShouldReturnArtikelMediaMassaWithCorrectProperties()
        {
            // Arrange
            string nama = "ArtikelMediaMassa A";
            int nilai = 1000;

            // Act
            var result = Domain.ArtikelMediaMassa.ArtikelMediaMassa.Create(
                nama,
                nilai
            );

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Nama.Should().Be(nama);
            result.Value.Nilai.Should().Be(nilai);
            result.Value.Uuid.Should().NotBe(Guid.Empty);
        }

        [Fact]
        public void Create_ShouldFail_WhenNilaiIsNegative()
        {
            // Arrange
            string nama = "ArtikelMediaMassa A";
            int nilai = -1000;

            // Act
            var result = Domain.ArtikelMediaMassa.ArtikelMediaMassa.Create(
                nama,
                nilai
            );

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Code.Should().Be("ArtikelMediaMassa.InvalidNilai");
            result.Error.Description.Should().Be("Nilai is invalid format");
        }

        [Fact]
        public void Create_ShouldRaiseArtikelMediaMassaCreatedDomainEvent()
        {
            // Arrange
            string nama = "ArtikelMediaMassa A";
            int nilai = 1000;

            // Act
            var komponen = Domain.ArtikelMediaMassa.ArtikelMediaMassa.Create(nama, nilai);

            // Assert
            komponen.Value.DomainEvents.Should().Contain(e => e is ArtikelMediaMassaCreatedDomainEvent);
        }

        [Fact]
        public void Update_ShouldReturnUpdatedArtikelMediaMassa()
        {
            // Arrange
            // Pertama, buat komponen menggunakan metode Create
            var komponen = Domain.ArtikelMediaMassa.ArtikelMediaMassa.Create("Old ArtikelMediaMassa", 500).Value;

            // Act
            // Gunakan builder untuk mengupdate komponen yang ada
            var namaChange = "Updated ArtikelMediaMassa";
            var nilaiChange = 1400;

            var builder = Domain.ArtikelMediaMassa.ArtikelMediaMassa.Update(komponen);
            builder.ChangeNama(namaChange);
            builder.ChangeNilai(nilaiChange);
            var updatedArtikelMediaMassa = builder.Build();

            // Assert
            updatedArtikelMediaMassa.IsSuccess.Should().BeTrue();
            updatedArtikelMediaMassa.Value.Nama.Should().Be(namaChange);
            updatedArtikelMediaMassa.Value.Nilai.Should().Be(nilaiChange);
            updatedArtikelMediaMassa.Value.Uuid.Should().NotBe(Guid.Empty);
        }
    }
}