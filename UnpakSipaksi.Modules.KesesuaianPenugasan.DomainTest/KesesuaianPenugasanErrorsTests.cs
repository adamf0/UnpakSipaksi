using FluentAssertions;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KesesuaianPenugasan.Domain.KesesuaianPenugasan;

namespace UnpakSipaksi.Modules.KesesuaianPenugasan.DomainTest
{
    public class KesesuaianPenugasanErrorsTests
    {
        [Theory]
        [InlineData("KesesuaianPenugasan.EmptyData", "data is not found", ErrorType.NotFound)]
        [InlineData("KesesuaianPenugasan.UnknownKategoriSkema", "Unknown schema category", ErrorType.NotFound)]
        [InlineData("KesesuaianPenugasan.InvalidValueNilai", "Invalid value 'nilai'", ErrorType.NotFound)]
        [InlineData("KesesuaianPenugasan.NotSameValue", "not the same value in data 'nilai'", ErrorType.NotFound)]
        public void StaticError_ShouldReturnCorrectValues(string expectedCode, string expectedDescription, ErrorType expectedType)
        {
            // Act
            Error error = expectedCode switch
            {
                "KesesuaianPenugasan.EmptyData" => KesesuaianPenugasanErrors.EmptyData(),
                "KesesuaianPenugasan.UnknownKategoriSkema" => KesesuaianPenugasanErrors.UnknownKategoriSkema(),
                "KesesuaianPenugasan.InvalidValueNilai" => KesesuaianPenugasanErrors.InvalidValueNilai(),
                "KesesuaianPenugasan.NotSameValue" => KesesuaianPenugasanErrors.NotSameValue(),
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
            var error = KesesuaianPenugasanErrors.NotFound(id);

            // Assert
            error.Code.Should().Be("KesesuaianPenugasan.NotFound");
            error.Description.Should().Be($"Kesesuaian penugasan with the identifier {id} was not found");
            error.Type.Should().Be(ErrorType.NotFound);
        }
    }
}
