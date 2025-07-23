using FluentAssertions;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KesesuaianWaktuRabLuaranFasilitas.Domain.KesesuaianWaktuRabLuaranFasilitas;

namespace UnpakSipaksi.Modules.KesesuaianWaktuRabLuaranFasilitas.DomainTest
{
    public class KesesuaianWaktuRabLuaranFasilitasErrorsTests
    {
        [Theory]
        [InlineData("KesesuaianWaktuRabLuaranFasilitas.EmptyData", "data is not found", ErrorType.NotFound)]
        [InlineData("KesesuaianWaktuRabLuaranFasilitas.UnknownKategoriSkema", "Unknown schema category", ErrorType.NotFound)]
        [InlineData("KesesuaianWaktuRabLuaranFasilitas.InvalidValueSkor", "Invalid value 'skor'", ErrorType.NotFound)]
        [InlineData("KesesuaianWaktuRabLuaranFasilitas.NotSameValue", "not the same value in data 'skor'", ErrorType.NotFound)]
        public void StaticError_ShouldReturnCorrectValues(string expectedCode, string expectedDescription, ErrorType expectedType)
        {
            // Act
            Error error = expectedCode switch
            {
                "KesesuaianWaktuRabLuaranFasilitas.EmptyData" => KesesuaianWaktuRabLuaranFasilitasErrors.EmptyData(),
                "KesesuaianWaktuRabLuaranFasilitas.UnknownKategoriSkema" => KesesuaianWaktuRabLuaranFasilitasErrors.UnknownKategoriSkema(),
                "KesesuaianWaktuRabLuaranFasilitas.InvalidValueSkor" => KesesuaianWaktuRabLuaranFasilitasErrors.InvalidValueSkor(),
                "KesesuaianWaktuRabLuaranFasilitas.NotSameValue" => KesesuaianWaktuRabLuaranFasilitasErrors.NotSameValue(),
                _ => throw new ArgumentException("Unknown error code", nameof(expectedCode))
            };

            // Assert
            error.Code.Should().Be(expectedCode);
            error.Description.Should().Be(expectedDescription);
            error.Type.Should().Be(expectedType);
        }

        [Fact]
        public void NotFound_ShouldReturnCorrectError()
        {
            // Arrange
            var id = Guid.NewGuid();

            // Act
            var error = KesesuaianWaktuRabLuaranFasilitasErrors.NotFound(id);

            // Assert
            error.Code.Should().Be("KesesuaianWaktuRabLuaranFasilitas.NotFound");
            error.Description.Should().Be($"Kesesuaian waktu rab luaran fasilitas with the identifier {id} was not found");
            error.Type.Should().Be(ErrorType.NotFound);
        }
    }
}
