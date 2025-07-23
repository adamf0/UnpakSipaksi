using FluentAssertions;
using UnpakSipaksi.Modules.MetodeRencanaKegiatan.Domain.MetodeRencanaKegiatan;

namespace UnpakSipaksi.Modules.MetodeRencanaKegiatan.DomainTest
{
    public class MetodeRencanaKegiatanTests
    {
        [Fact]
        public void Create_WhenValidPropertiesProvided_ShouldReturnKategoriWithCorrectProperties()
        {
            // Arrange
            string nama = "ABC";
            int nilai = 1;

            // Act
            var result = Domain.MetodeRencanaKegiatan.MetodeRencanaKegiatan.Create(
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
        public void Create_WhenValidPropertiesProvided_ShouldRaiseKategoriCreatedDomainEvent()
        {
            // Arrange
            string nama = "ABC";
            int nilai = 1;

            // Act
            var result = Domain.MetodeRencanaKegiatan.MetodeRencanaKegiatan.Create(
                nama,
                nilai
            );

            // Assert
            result.Value.DomainEvents.Should().Contain(e => e is MetodeRencanaKegiatanCreatedDomainEvent);
        }
    }
}
