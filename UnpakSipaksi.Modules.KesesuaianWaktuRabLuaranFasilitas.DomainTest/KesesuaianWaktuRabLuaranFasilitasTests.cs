using FluentAssertions;
using UnpakSipaksi.Modules.KesesuaianWaktuRabLuaranFasilitas.Domain.KesesuaianWaktuRabLuaranFasilitas;

namespace UnpakSipaksi.Modules.KesesuaianWaktuRabLuaranFasilitas.DomainTest
{
    public class KesesuaianWaktuRabLuaranFasilitasTests
    {
        [Fact]
        public void Create_WhenValidPropertiesProvided_ShouldReturnKategoriWithCorrectProperties()
        {
            // Arrange
            string nama = "ABC";
            int skor = 1;

            // Act
            var result = Domain.KesesuaianWaktuRabLuaranFasilitas.KesesuaianWaktuRabLuaranFasilitas.Create(
                nama,
                skor
            );

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Nama.Should().Be(nama);
            result.Value.Uuid.Should().NotBe(Guid.Empty);
        }

        [Fact]
        public void Create_WhenValidPropertiesProvided_ShouldRaiseKategoriCreatedDomainEvent()
        {
            // Arrange
            string nama = "ABC";
            int skor = 1;

            // Act
            var result = Domain.KesesuaianWaktuRabLuaranFasilitas.KesesuaianWaktuRabLuaranFasilitas.Create(
                nama,
                skor
            );

            // Assert
            result.Value.DomainEvents.Should().Contain(e => e is KesesuaianWaktuRabLuaranFasilitasCreatedDomainEvent);
        }
    }
}
