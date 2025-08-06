using FluentAssertions;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.RumusanPrioritasMitra.Domain.RumusanPrioritasMitra;

namespace UnpakSipaksi.Modules.RumusanPrioritasMitra.DomainTest
{
    public class RumusanPrioritasMitraErrorsTests
    {
        [Theory]
        [InlineData("RumusanPrioritasMitra.EmptyData", "data is not found", ErrorType.NotFound)]
        [InlineData("RumusanPrioritasMitra.UnknownKategoriSkema", "Unknown schema category", ErrorType.NotFound)]
        [InlineData("RumusanPrioritasMitra.InvalidValueNilai", "Invalid value 'nilai'", ErrorType.NotFound)]
        [InlineData("RumusanPrioritasMitra.NotSameValue", "not the same value in data 'nilai'", ErrorType.NotFound)]
        public void StaticError_ShouldReturnCorrectValues(string expectedCode, string expectedDescription, ErrorType expectedType)
        {
            // Act
            Error error = expectedCode switch
            {
                "RumusanPrioritasMitra.EmptyData" => RumusanPrioritasMitraErrors.EmptyData(),
                "RumusanPrioritasMitra.UnknownKategoriSkema" => RumusanPrioritasMitraErrors.UnknownKategoriSkema(),
                "RumusanPrioritasMitra.InvalidValueNilai" => RumusanPrioritasMitraErrors.InvalidValueNilai(),
                "RumusanPrioritasMitra.NotSameValue" => RumusanPrioritasMitraErrors.NotSameValue(),
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
            var error = RumusanPrioritasMitraErrors.NotFound(id);

            // Assert
            error.Code.Should().Be("RumusanPrioritasMitra.NotFound");
            error.Description.Should().Be($"Rumusan prioritas mitra with the identifier {id} was not found");
            error.Type.Should().Be(ErrorType.NotFound);
        }
    }
}
