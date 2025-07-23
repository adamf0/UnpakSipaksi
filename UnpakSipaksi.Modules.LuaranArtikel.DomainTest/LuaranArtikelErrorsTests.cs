using FluentAssertions;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.LuaranArtikel.Domain.LuaranArtikel;

namespace UnpakSipaksi.Modules.LuaranArtikel.DomainTest
{
    public class LuaranArtikelErrorsTests
    {
        [Theory]
        [InlineData("LuaranArtikel.EmptyData", "data is not found", ErrorType.NotFound)]
        [InlineData("LuaranArtikel.UnknownKategoriSkema", "Unknown schema category", ErrorType.NotFound)]
        [InlineData("LuaranArtikel.InvalidValueNilai", "Invalid value 'nilai'", ErrorType.NotFound)]
        [InlineData("LuaranArtikel.NotSameValue", "not the same value in data 'nilai'", ErrorType.NotFound)]
        public void StaticError_ShouldReturnCorrectValues(string expectedCode, string expectedDescription, ErrorType expectedType)
        {
            // Act
            Error error = expectedCode switch
            {
                "LuaranArtikel.EmptyData" => LuaranArtikelErrors.EmptyData(),
                "LuaranArtikel.UnknownKategoriSkema" => LuaranArtikelErrors.UnknownKategoriSkema(),
                "LuaranArtikel.InvalidValueNilai" => LuaranArtikelErrors.InvalidValueNilai(),
                "LuaranArtikel.NotSameValue" => LuaranArtikelErrors.NotSameValue(),
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
            var error = LuaranArtikelErrors.NotFound(id);

            // Assert
            error.Code.Should().Be("LuaranArtikel.NotFound");
            error.Description.Should().Be($"Luaran artikel with the identifier {id} was not found");
            error.Type.Should().Be(ErrorType.NotFound);
        }
    }
}
