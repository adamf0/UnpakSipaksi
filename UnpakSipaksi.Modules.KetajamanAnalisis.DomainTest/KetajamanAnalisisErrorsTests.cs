using FluentAssertions;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KetajamanAnalisis.Domain.KetajamanAnalisis;

namespace UnpakSipaksi.Modules.KetajamanAnalisis.DomainTest
{
    public class KetajamanAnalisisErrorsTests
    {
        [Theory]
        [InlineData("KetajamanAnalisis.EmptyData", "data is not found", ErrorType.NotFound)]
        [InlineData("KetajamanAnalisis.UnknownKategoriSkema", "Unknown schema category", ErrorType.NotFound)]
        [InlineData("KetajamanAnalisis.InvalidValueNilai", "Invalid value 'nilai'", ErrorType.NotFound)]
        [InlineData("KetajamanAnalisis.NotSameValue", "not the same value in data 'nilai'", ErrorType.NotFound)]
        public void StaticError_ShouldReturnCorrectValues(string expectedCode, string expectedDescription, ErrorType expectedType)
        {
            // Act
            Error error = expectedCode switch
            {
                "KetajamanAnalisis.EmptyData" => KetajamanAnalisisErrors.EmptyData(),
                "KetajamanAnalisis.UnknownKategoriSkema" => KetajamanAnalisisErrors.UnknownKategoriSkema(),
                "KetajamanAnalisis.InvalidValueNilai" => KetajamanAnalisisErrors.InvalidValueNilai(),
                "KetajamanAnalisis.NotSameValue" => KetajamanAnalisisErrors.NotSameValue(),
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
            var error = KetajamanAnalisisErrors.NotFound(id);

            // Assert
            error.Code.Should().Be("KetajamanAnalisis.NotFound");
            error.Description.Should().Be($"Ketajaman analisis with the identifier {id} was not found");
            error.Type.Should().Be(ErrorType.NotFound);
        }
    }
}
