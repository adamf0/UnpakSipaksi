using UnpakSipaksi.Modules.Komponen.Domain.Komponen;
using UnpakSipaksi.Common.Domain;
using FluentAssertions;
using Moq;

namespace UnpakSipaksi.Modules.Komponen.DomainTest
{
    public class KomponenTests
    {
        [Fact]
        public void Create_ShouldReturnKomponenWithCorrectProperties()
        {
            // Arrange
            string nama = "Komponen A";
            int? maxBiaya = 1000;

            // Act
            var result = Domain.Komponen.Komponen.Create(
                nama,
                maxBiaya
            );

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Nama.Should().Be(nama);
            result.Value.MaxBiaya.Should().Be(maxBiaya);
            result.Value.Uuid.Should().NotBe(Guid.Empty);
        }

        [Fact]
        public void Create_ShouldRaiseKomponenCreatedDomainEvent()
        {
            // Arrange
            string nama = "Komponen A";
            int? maxBiaya = 1000;

            // Act
            var komponen = Domain.Komponen.Komponen.Create(nama, maxBiaya);

            // Assert
            komponen.Value.DomainEvents.Should().Contain(e => e is KomponenCreatedDomainEvent);
        }

        [Fact]
        public void Update_ShouldReturnUpdatedKomponen()
        {
            // Arrange
            // Pertama, buat komponen menggunakan metode Create
            var komponen = Domain.Komponen.Komponen.Create("Old Komponen", 500).Value;

            // Act
            // Gunakan builder untuk mengupdate komponen yang ada
            var namaChange = "Updated Komponen";
            var maxBiayaChange = 1400;

            var builder = Domain.Komponen.Komponen.Update(komponen);
            builder.ChangeNama(namaChange);
            builder.ChangeMaxBiaya(maxBiayaChange);
            var updatedKomponen = builder.Build();

            // Assert
            updatedKomponen.IsSuccess.Should().BeTrue();
            updatedKomponen.Value.Nama.Should().Be(namaChange);
            updatedKomponen.Value.MaxBiaya.Should().Be(maxBiayaChange);
            updatedKomponen.Value.Uuid.Should().NotBe(Guid.Empty);
        }
    }
}