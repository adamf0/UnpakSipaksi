using FluentAssertions;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.ArtikelMediaMassa.Domain.ArtikelMediaMassa;
using Xunit;

namespace UnpakSipaksi.Modules.ArtikelMediaMassa.DomainTest
{
    public class ArtikelMediaMassaErrorsTests
    {
        [Theory]
        [InlineData("ArtikelMediaMassa.EmptyData", "data is not found", ErrorType.NotFound)]
        [InlineData("ArtikelMediaMassa.NotSameValue", "not the same value in data 'nilai'", ErrorType.NotFound)]
        [InlineData("ArtikelMediaMassa.UnknownKategoriSkema", "Unknown schema category", ErrorType.NotFound)]
        public void StaticError_ShouldReturnCorrectValues(string expectedCode, string expectedDescription, ErrorType expectedType)
        {
            // Act
            Error error = expectedCode switch
            {
                "ArtikelMediaMassa.EmptyData" => ArtikelMediaMassaErrors.EmptyData(),
                "ArtikelMediaMassa.NotSameValue" => ArtikelMediaMassaErrors.NotSameValue(),
                "ArtikelMediaMassa.UnknownKategoriSkema" => ArtikelMediaMassaErrors.UnknownKategoriSkema(),
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
            var akurasiPenelitianId = Guid.NewGuid();

            // Act
            var error = ArtikelMediaMassaErrors.NotFound(akurasiPenelitianId);

            // Assert
            error.Code.Should().Be("ArtikelMediaMassa.NotFound");
            error.Description.Should().Be($"Artikel media massa with identifier {akurasiPenelitianId} not found");
            error.Type.Should().Be(ErrorType.NotFound);
        }
    }
}
