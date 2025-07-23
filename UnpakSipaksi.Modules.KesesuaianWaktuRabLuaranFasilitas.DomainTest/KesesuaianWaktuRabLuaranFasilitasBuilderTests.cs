using FluentAssertions;
using UnpakSipaksi.Common.Domain;
using static UnpakSipaksi.Modules.KesesuaianWaktuRabLuaranFasilitas.Domain.KesesuaianWaktuRabLuaranFasilitas.KesesuaianWaktuRabLuaranFasilitas;

namespace UnpakSipaksi.Modules.KesesuaianWaktuRabLuaranFasilitas.DomainTest
{
    public class KesesuaianWaktuRabLuaranFasilitasBuilderTests
    {
        [Fact]
        public void Build_WhenFieldsChanged_ShouldReturnUpdatedKesesuaianWaktuRabLuaranFasilitas()
        {
            // Arrange
            string Nama = "ABC";
            int Skor = 1;

            var createResult = Domain.KesesuaianWaktuRabLuaranFasilitas.KesesuaianWaktuRabLuaranFasilitas.Create(
                Nama,
                Skor
            );
            var KesesuaianWaktuRabLuaranFasilitas = createResult.Value;

            // Act
            string newNama = "ACC";
            int newSkor = 10;

            KesesuaianWaktuRabLuaranFasilitasBuilder builder = Domain.KesesuaianWaktuRabLuaranFasilitas.KesesuaianWaktuRabLuaranFasilitas.Update(KesesuaianWaktuRabLuaranFasilitas);
            builder.ChangeNama(newNama)
                    .ChangeSkor(newSkor);
            var result = builder.Build();

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Nama.Should().Be(newNama);
            result.Value.Skor.Should().Be(newSkor);
            result.Value.Uuid.Should().NotBe(Guid.Empty);
        }

        [Fact]
        public void Build_WithInvalidSkor_ShouldReturnExpectedValidationError()
        {
            // Arrange
            string Nama = "ABC";
            int Skor = 1;

            var createResult = Domain.KesesuaianWaktuRabLuaranFasilitas.KesesuaianWaktuRabLuaranFasilitas.Create(
                Nama,
                Skor
            );
            var KesesuaianWaktuRabLuaranFasilitas = createResult.Value;

            // Act
            string newNama = "ACC";
            int newSkor = -110;

            KesesuaianWaktuRabLuaranFasilitasBuilder builder = Domain.KesesuaianWaktuRabLuaranFasilitas.KesesuaianWaktuRabLuaranFasilitas.Update(KesesuaianWaktuRabLuaranFasilitas);
            builder.ChangeNama(newNama)
                    .ChangeSkor(newSkor);
            var result = builder.Build();

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Code.Should().Be("KesesuaianWaktuRabLuaranFasilitas.InvalidValueSkor");
            result.Error.Description.Should().Be("Invalid value 'skor'");
            result.Error.Type.Should().Be(ErrorType.NotFound);
        }
    }
}
