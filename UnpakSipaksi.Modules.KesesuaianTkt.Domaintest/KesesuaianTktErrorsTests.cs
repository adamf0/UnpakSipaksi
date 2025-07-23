using FluentAssertions;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KesesuaianTkt.Domain.KesesuaianTkt;

namespace UnpakSipaksi.Modules.KesesuaianTkt.Domaintest
{
    public class KesesuaianTktErrorsTests
    {
        [Theory]
        [InlineData("KesesuaianTkt.EmptyData", "data is not found", ErrorType.NotFound)]
        [InlineData("KesesuaianTkt.UnknownKategoriSkema", "Unknown schema category", ErrorType.NotFound)]
        [InlineData("KesesuaianTkt.InvalidValueSkor", "Invalid value 'skor'", ErrorType.NotFound)]
        [InlineData("KesesuaianTkt.NotSameValue", "not the same value in data 'skor'", ErrorType.NotFound)]
        public void StaticError_ShouldReturnCorrectValues(string expectedCode, string expectedDescription, ErrorType expectedType)
        {
            // Act
            Error error = expectedCode switch
            {
                "KesesuaianTkt.EmptyData" => KesesuaianTktErrors.EmptyData(),
                "KesesuaianTkt.UnknownKategoriSkema" => KesesuaianTktErrors.UnknownKategoriSkema(),
                "KesesuaianTkt.InvalidValueSkor" => KesesuaianTktErrors.InvalidValueSkor(),
                "KesesuaianTkt.NotSameValue" => KesesuaianTktErrors.NotSameValue(),
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
            var error = KesesuaianTktErrors.NotFound(id);

            // Assert
            error.Code.Should().Be("KesesuaianTkt.NotFound");
            error.Description.Should().Be($"Kesesuaian tkt with the identifier {id} was not found");
            error.Type.Should().Be(ErrorType.NotFound);
        }
    }
}
