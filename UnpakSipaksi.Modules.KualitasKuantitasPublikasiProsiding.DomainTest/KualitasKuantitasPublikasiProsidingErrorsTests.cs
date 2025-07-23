using FluentAssertions;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KualitasKuantitasPublikasiProsiding.Domain.KualitasKuantitasPublikasiProsiding;

namespace UnpakSipaksi.Modules.KualitasKuantitasPublikasiProsiding.DomainTest
{
    public class KualitasKuantitasPublikasiProsidingErrorsTests
    {
        [Theory]
        [InlineData("KualitasKuantitasPublikasiProsiding.EmptyData", "data is not found", ErrorType.NotFound)]
        [InlineData("KualitasKuantitasPublikasiProsiding.UnknownKategoriSkema", "Unknown schema category", ErrorType.NotFound)]
        [InlineData("KualitasKuantitasPublikasiProsiding.InvalidValueNilai", "Invalid value 'nilai'", ErrorType.NotFound)]
        [InlineData("KualitasKuantitasPublikasiProsiding.NotSameValue", "not the same value in data 'nilai'", ErrorType.NotFound)]
        public void StaticError_ShouldReturnCorrectValues(string expectedCode, string expectedDescription, ErrorType expectedType)
        {
            // Act
            Error error = expectedCode switch
            {
                "KualitasKuantitasPublikasiProsiding.EmptyData" => KualitasKuantitasPublikasiProsidingErrors.EmptyData(),
                "KualitasKuantitasPublikasiProsiding.UnknownKategoriSkema" => KualitasKuantitasPublikasiProsidingErrors.UnknownKategoriSkema(),
                "KualitasKuantitasPublikasiProsiding.InvalidValueNilai" => KualitasKuantitasPublikasiProsidingErrors.InvalidValueNilai(),
                "KualitasKuantitasPublikasiProsiding.NotSameValue" => KualitasKuantitasPublikasiProsidingErrors.NotSameValue(),
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
            var error = KualitasKuantitasPublikasiProsidingErrors.NotFound(id);

            // Assert
            error.Code.Should().Be("KualitasKuantitasPublikasiProsiding.NotFound");
            error.Description.Should().Be($"Kualitas kuantitas publikasi prosiding with the identifier {id} was not found");
            error.Type.Should().Be(ErrorType.NotFound);
        }
    }
}
