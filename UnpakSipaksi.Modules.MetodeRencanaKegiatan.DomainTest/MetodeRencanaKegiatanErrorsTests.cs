using FluentAssertions;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.MetodeRencanaKegiatan.Domain.MetodeRencanaKegiatan;

namespace UnpakSipaksi.Modules.MetodeRencanaKegiatan.DomainTest
{
    public class MetodeRencanaKegiatanErrorsTests
    {
        [Theory]
        [InlineData("MetodeRencanaKegiatan.EmptyData", "data is not found", ErrorType.NotFound)]
        [InlineData("MetodeRencanaKegiatan.UnknownKategoriSkema", "Unknown schema category", ErrorType.NotFound)]
        [InlineData("MetodeRencanaKegiatan.InvalidValueNilai", "Invalid value 'nilai'", ErrorType.NotFound)]
        [InlineData("MetodeRencanaKegiatan.NotSameValue", "not the same value in data 'nilai'", ErrorType.NotFound)]
        public void StaticError_ShouldReturnCorrectValues(string expectedCode, string expectedDescription, ErrorType expectedType)
        {
            // Act
            Error error = expectedCode switch
            {
                "MetodeRencanaKegiatan.EmptyData" => MetodeRencanaKegiatanErrors.EmptyData(),
                "MetodeRencanaKegiatan.UnknownKategoriSkema" => MetodeRencanaKegiatanErrors.UnknownKategoriSkema(),
                "MetodeRencanaKegiatan.InvalidValueNilai" => MetodeRencanaKegiatanErrors.InvalidValueNilai(),
                "MetodeRencanaKegiatan.NotSameValue" => MetodeRencanaKegiatanErrors.NotSameValue(),
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
            var error = MetodeRencanaKegiatanErrors.NotFound(id);

            // Assert
            error.Code.Should().Be("MetodeRencanaKegiatan.NotFound");
            error.Description.Should().Be($"Metode rencana kegiatan with the identifier {id} was not found");
            error.Type.Should().Be(ErrorType.NotFound);
        }
    }
}
