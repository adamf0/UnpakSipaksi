using FluentAssertions;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KredibilitasMitraDukungan.Domain.KredibilitasMitraDukungan;

namespace UnpakSipaksi.Modules.KredibilitasMitraDukungan.DomainTest
{
    public class KredibilitasMitraDukunganErrorsTests
    {
        [Theory]
        [InlineData("KredibilitasMitraDukungan.EmptyData", "data is not found", ErrorType.NotFound)]
        [InlineData("KredibilitasMitraDukungan.UnknownKategoriSkema", "Unknown schema category", ErrorType.NotFound)]
        [InlineData("KredibilitasMitraDukungan.InvalidValueSkor", "Invalid value 'skor'", ErrorType.NotFound)]
        [InlineData("KredibilitasMitraDukungan.NotSameValue", "not the same value in data 'skor'", ErrorType.NotFound)]
        public void StaticError_ShouldReturnCorrectValues(string expectedCode, string expectedDescription, ErrorType expectedType)
        {
            // Act
            Error error = expectedCode switch
            {
                "KredibilitasMitraDukungan.EmptyData" => KredibilitasMitraDukunganErrors.EmptyData(),
                "KredibilitasMitraDukungan.UnknownKategoriSkema" => KredibilitasMitraDukunganErrors.UnknownKategoriSkema(),
                "KredibilitasMitraDukungan.InvalidValueSkor" => KredibilitasMitraDukunganErrors.InvalidValueSkor(),
                "KredibilitasMitraDukungan.NotSameValue" => KredibilitasMitraDukunganErrors.NotSameValue(),
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
            var error = KredibilitasMitraDukunganErrors.NotFound(id);

            // Assert
            error.Code.Should().Be("KredibilitasMitraDukungan.NotFound");
            error.Description.Should().Be($"Kredibilitas mitra dukungan with the identifier {id} was not found");
            error.Type.Should().Be(ErrorType.NotFound);
        }
    }
}
