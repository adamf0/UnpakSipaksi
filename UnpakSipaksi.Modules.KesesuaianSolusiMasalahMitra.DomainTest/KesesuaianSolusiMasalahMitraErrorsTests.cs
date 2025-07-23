using FluentAssertions;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KesesuaianSolusiMasalahMitra.Domain.KesesuaianSolusiMasalahMitra;

namespace UnpakSipaksi.Modules.KesesuaianSolusiMasalahMitra.DomainTest
{
    public class KesesuaianSolusiMasalahMitraErrorsTests
    {
        [Theory]
        [InlineData("KesesuaianSolusiMasalahMitra.EmptyData", "data is not found", ErrorType.NotFound)]
        [InlineData("KesesuaianSolusiMasalahMitra.UnknownKategoriSkema", "Unknown schema category", ErrorType.NotFound)]
        [InlineData("KesesuaianSolusiMasalahMitra.InvalidValueNilai", "Invalid value 'nilai'", ErrorType.NotFound)]
        [InlineData("KesesuaianSolusiMasalahMitra.NotSameValue", "not the same value in data 'nilai'", ErrorType.NotFound)]
        public void StaticError_ShouldReturnCorrectValues(string expectedCode, string expectedDescription, ErrorType expectedType)
        {
            // Act
            Error error = expectedCode switch
            {
                "KesesuaianSolusiMasalahMitra.EmptyData" => KesesuaianSolusiMasalahMitraErrors.EmptyData(),
                "KesesuaianSolusiMasalahMitra.UnknownKategoriSkema" => KesesuaianSolusiMasalahMitraErrors.UnknownKategoriSkema(),
                "KesesuaianSolusiMasalahMitra.InvalidValueNilai" => KesesuaianSolusiMasalahMitraErrors.InvalidValueNilai(),
                "KesesuaianSolusiMasalahMitra.NotSameValue" => KesesuaianSolusiMasalahMitraErrors.NotSameValue(),
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
            var error = KesesuaianSolusiMasalahMitraErrors.NotFound(id);

            // Assert
            error.Code.Should().Be("KesesuaianSolusiMasalahMitra.NotFound");
            error.Description.Should().Be($"Kesesuaian solusi masalah mitra with the identifier {id} was not found");
            error.Type.Should().Be(ErrorType.NotFound);
        }
    }
}
