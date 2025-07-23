using FluentAssertions;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.JumlahKolaboratorPublikasBereputasi.Domain.JumlahKolaboratorPublikasBereputasi;
using Xunit;

namespace UnpakSipaksi.Modules.JumlahKolaboratorPublikasBereputasi.DomainTest
{
    public class JumlahKolaboratorPublikasBereputasiErrorsTests
    {
        [Theory]
        [InlineData("JumlahKolaboratorPublikasBereputasi.EmptyData", "data is not found", ErrorType.NotFound)]
        [InlineData("JumlahKolaboratorPublikasBereputasi.NotSameValue", "not the same value in data 'nilai'", ErrorType.NotFound)]
        [InlineData("JumlahKolaboratorPublikasBereputasi.UnknownKategoriSkema", "Unknown schema category", ErrorType.NotFound)]
        [InlineData("JumlahKolaboratorPublikasBereputasi.InvalidSkor", "Skor is invalid format", ErrorType.NotFound)]
        public void StaticError_ShouldReturnCorrectValues(string expectedCode, string expectedDescription, ErrorType expectedType)
        {
            // Act
            Error error = expectedCode switch
            {
                "JumlahKolaboratorPublikasBereputasi.EmptyData" => JumlahKolaboratorPublikasBereputasiErrors.EmptyData(),
                "JumlahKolaboratorPublikasBereputasi.NotSameValue" => JumlahKolaboratorPublikasBereputasiErrors.NotSameValue(),
                "JumlahKolaboratorPublikasBereputasi.UnknownKategoriSkema" => JumlahKolaboratorPublikasBereputasiErrors.UnknownKategoriSkema(),
                "JumlahKolaboratorPublikasBereputasi.InvalidSkor" => JumlahKolaboratorPublikasBereputasiErrors.InvalidSkor(),
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
            var error = JumlahKolaboratorPublikasBereputasiErrors.NotFound(id);

            // Assert
            error.Code.Should().Be("JumlahKolaboratorPublikasBereputasi.NotFound");
            error.Description.Should().Be($"Jumlah kolaborator publikas bereputasi with identifier {id} not found");
            error.Type.Should().Be(ErrorType.NotFound);
        }
    }
}
