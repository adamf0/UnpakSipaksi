using FluentAssertions;
using UnpakSipaksi.Modules.KesesuaianPenugasan.Domain.KesesuaianPenugasan;

namespace UnpakSipaksi.Modules.KesesuaianPenugasan.DomainTest
{
    public class KesesuaianPenugasanTests
    {
        [Fact]
        public void Create_WhenValidPropertiesProvided_ShouldReturnKategoriWithCorrectProperties()
        {
            // Arrange
            string nama = "ABC";
            int nilai = 1;

            // Act
            var result = Domain.KesesuaianPenugasan.KesesuaianPenugasan.Create(
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
            var result = Domain.KesesuaianPenugasan.KesesuaianPenugasan.Create(
                nama,
                skor
            );

            // Assert
            result.Value.DomainEvents.Should().Contain(e => e is KesesuaianPenugasanCreatedDomainEvent);
        }
    }
}
