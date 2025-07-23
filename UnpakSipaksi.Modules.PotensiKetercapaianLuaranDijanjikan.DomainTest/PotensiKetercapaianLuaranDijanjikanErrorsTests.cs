using FluentAssertions;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.PotensiKetercapaianLuaranDijanjikan.Domain.PotensiKetercapaianLuaranDijanjikan;

namespace UnpakSipaksi.Modules.PotensiKetercapaianLuaranDijanjikan.DomainTest
{
    public class PotensiKetercapaianLuaranDijanjikanErrorsTests
    {
        [Theory]
        [InlineData("PotensiKetercapaianLuaranDijanjikan.EmptyData", "data is not found", ErrorType.NotFound)]
        [InlineData("PotensiKetercapaianLuaranDijanjikan.UnknownKategoriSkema", "Unknown schema category", ErrorType.NotFound)]
        [InlineData("PotensiKetercapaianLuaranDijanjikan.InvalidValueSkor", "Invalid value 'skor'", ErrorType.NotFound)]
        [InlineData("PotensiKetercapaianLuaranDijanjikan.NotSameValue", "not the same value in data 'skor'", ErrorType.NotFound)]
        public void StaticError_ShouldReturnCorrectValues(string expectedCode, string expectedDescription, ErrorType expectedType)
        {
            // Act
            Error error = expectedCode switch
            {
                "PotensiKetercapaianLuaranDijanjikan.EmptyData" => PotensiKetercapaianLuaranDijanjikanErrors.EmptyData(),
                "PotensiKetercapaianLuaranDijanjikan.UnknownKategoriSkema" => PotensiKetercapaianLuaranDijanjikanErrors.UnknownKategoriSkema(),
                "PotensiKetercapaianLuaranDijanjikan.InvalidValueSkor" => PotensiKetercapaianLuaranDijanjikanErrors.InvalidValueSkor(),
                "PotensiKetercapaianLuaranDijanjikan.NotSameValue" => PotensiKetercapaianLuaranDijanjikanErrors.NotSameValue(),
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
            var error = PotensiKetercapaianLuaranDijanjikanErrors.NotFound(id);

            // Assert
            error.Code.Should().Be("PotensiKetercapaianLuaranDijanjikan.NotFound");
            error.Description.Should().Be($"Potensi ketercapaian luaran dijanjikan with the identifier {id} was not found");
            error.Type.Should().Be(ErrorType.NotFound);
        }
    }
}
