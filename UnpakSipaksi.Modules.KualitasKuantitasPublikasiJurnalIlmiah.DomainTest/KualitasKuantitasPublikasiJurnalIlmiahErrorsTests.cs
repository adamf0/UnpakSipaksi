using FluentAssertions;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KualitasKuantitasPublikasiJurnalIlmiah.Domain.KualitasKuantitasPublikasiJurnalIlmiah;

namespace UnpakSipaksi.Modules.KualitasKuantitasPublikasiJurnalIlmiah.DomainTest
{
    public class KualitasKuantitasPublikasiJurnalIlmiahErrorsTests
    {
        [Theory]
        [InlineData("KualitasKuantitasPublikasiJurnalIlmiah.EmptyData", "data is not found", ErrorType.NotFound)]
        [InlineData("KualitasKuantitasPublikasiJurnalIlmiah.UnknownKategoriSkema", "Unknown schema category", ErrorType.NotFound)]
        [InlineData("KualitasKuantitasPublikasiJurnalIlmiah.InvalidValueNilai", "Invalid value 'nilai'", ErrorType.NotFound)]
        [InlineData("KualitasKuantitasPublikasiJurnalIlmiah.NotSameValue", "not the same value in data 'nilai'", ErrorType.NotFound)]
        public void StaticError_ShouldReturnCorrectValues(string expectedCode, string expectedDescription, ErrorType expectedType)
        {
            // Act
            Error error = expectedCode switch
            {
                "KualitasKuantitasPublikasiJurnalIlmiah.EmptyData" => KualitasKuantitasPublikasiJurnalIlmiahErrors.EmptyData(),
                "KualitasKuantitasPublikasiJurnalIlmiah.UnknownKategoriSkema" => KualitasKuantitasPublikasiJurnalIlmiahErrors.UnknownKategoriSkema(),
                "KualitasKuantitasPublikasiJurnalIlmiah.InvalidValueNilai" => KualitasKuantitasPublikasiJurnalIlmiahErrors.InvalidValueNilai(),
                "KualitasKuantitasPublikasiJurnalIlmiah.NotSameValue" => KualitasKuantitasPublikasiJurnalIlmiahErrors.NotSameValue(),
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
            var error = KualitasKuantitasPublikasiJurnalIlmiahErrors.NotFound(id);

            // Assert
            error.Code.Should().Be("KualitasKuantitasPublikasiJurnalIlmiah.NotFound");
            error.Description.Should().Be($"Kualitas kuantitas publikasi jurnal ilmiah with the identifier {id} was not found");
            error.Type.Should().Be(ErrorType.NotFound);
        }
    }
}
