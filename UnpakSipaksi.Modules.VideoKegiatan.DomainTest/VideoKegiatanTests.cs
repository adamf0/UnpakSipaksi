using FluentAssertions;
using UnpakSipaksi.Modules.VideoKegiatan.Domain.VideoKegiatan;

namespace UnpakSipaksi.Modules.VideoKegiatan.DomainTest
{
    public class VideoKegiatanTests
    {
        [Fact]
        public void Create_WhenValidPropertiesProvided_ShouldReturnKategoriWithCorrectProperties()
        {
            // Arrange
            string nama = "ABC";
            int nilai = 1;

            // Act
            var result = Domain.VideoKegiatan.VideoKegiatan.Create(
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
            var result = Domain.VideoKegiatan.VideoKegiatan.Create(
                nama,
                nilai
            );

            // Assert
            result.Value.DomainEvents.Should().Contain(e => e is VideoKegiatanCreatedDomainEvent);
        }
    }
}
