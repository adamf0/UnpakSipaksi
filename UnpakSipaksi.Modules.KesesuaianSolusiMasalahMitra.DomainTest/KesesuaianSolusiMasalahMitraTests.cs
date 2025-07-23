using FluentAssertions;
using UnpakSipaksi.Modules.KesesuaianSolusiMasalahMitra.Domain.KesesuaianSolusiMasalahMitra;

namespace UnpakSipaksi.Modules.KesesuaianSolusiMasalahMitra.DomainTest
{
    public class KesesuaianSolusiMasalahMitraTests
    {
        [Fact]
        public void Create_WhenValidPropertiesProvided_ShouldReturnKategoriWithCorrectProperties()
        {
            // Arrange
            string nama = "ABC";
            int nilai = 1;

            // Act
            var result = Domain.KesesuaianSolusiMasalahMitra.KesesuaianSolusiMasalahMitra.Create(
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
            int skor = 1;

            // Act
            var result = Domain.KesesuaianSolusiMasalahMitra.KesesuaianSolusiMasalahMitra.Create(
                nama,
                skor
            );

            // Assert
            result.Value.DomainEvents.Should().Contain(e => e is KesesuaianSolusiMasalahMitraCreatedDomainEvent);
        }
    }
}
