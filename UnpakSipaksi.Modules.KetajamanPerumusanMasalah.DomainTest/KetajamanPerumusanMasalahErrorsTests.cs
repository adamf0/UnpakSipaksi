using FluentAssertions;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KetajamanPerumusanMasalah.Domain.KetajamanPerumusanMasalah;

namespace UnpakSipaksi.Modules.KetajamanPerumusanMasalah.DomainTest
{
    public class KetajamanPerumusanMasalahErrorsTests
    {
        [Theory]
        [InlineData("KetajamanPerumusanMasalah.EmptyData", "data is not found", ErrorType.NotFound)]
        [InlineData("KetajamanPerumusanMasalah.UnknownKategoriSkema", "Unknown schema category", ErrorType.NotFound)]
        [InlineData("KetajamanPerumusanMasalah.InvalidValueSkor", "Invalid value 'skor'", ErrorType.NotFound)]
        [InlineData("KetajamanPerumusanMasalah.NotSameValue", "not the same value in data 'skor'", ErrorType.NotFound)]
        public void StaticError_ShouldReturnCorrectValues(string expectedCode, string expectedDescription, ErrorType expectedType)
        {
            // Act
            Error error = expectedCode switch
            {
                "KetajamanPerumusanMasalah.EmptyData" => KetajamanPerumusanMasalahErrors.EmptyData(),
                "KetajamanPerumusanMasalah.UnknownKategoriSkema" => KetajamanPerumusanMasalahErrors.UnknownKategoriSkema(),
                "KetajamanPerumusanMasalah.InvalidValueSkor" => KetajamanPerumusanMasalahErrors.InvalidValueSkor(),
                "KetajamanPerumusanMasalah.NotSameValue" => KetajamanPerumusanMasalahErrors.NotSameValue(),
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
            var error = KetajamanPerumusanMasalahErrors.NotFound(id);

            // Assert
            error.Code.Should().Be("KetajamanPerumusanMasalah.NotFound");
            error.Description.Should().Be($"Ketajaman perumusan masalah with the identifier {id} was not found");
            error.Type.Should().Be(ErrorType.NotFound);
        }
    }
}
