using FluentAssertions;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.PenelitianPkm.Domain.DokumenLainnya;

namespace UnpakSipaksi.Modules.PenelitianPkm.DomainTest.DokumenLainnya
{
    public class DokumenLainnyaErrorsTests
    {
        [Theory]
        [InlineData("DokumenLainnya.EmptyData", "data is not found", ErrorType.NotFound)]
        [InlineData("DokumenLainnya.EmptyResource", "Resource is not found", ErrorType.NotFound)]
        [InlineData("DokumenLainnya.InvalidData", "Hibah penelitian is not match existing data", ErrorType.NotFound)]
        [InlineData("DokumenLainnya.DuplicateResource", "Resource is duplicate source", ErrorType.NotFound)]
        [InlineData("DokumenLainnya.InvalidLink", "Invalid link", ErrorType.NotFound)]
        public void StaticError_ShouldReturnCorrectValues(string expectedCode, string expectedDescription, ErrorType expectedType)
        {
            // Act
            Error error = expectedCode switch
            {
                "DokumenLainnya.EmptyData" => DokumenLainnyaErrors.EmptyData(),
                "DokumenLainnya.EmptyResource" => DokumenLainnyaErrors.EmptyResource(),
                "DokumenLainnya.InvalidData" => DokumenLainnyaErrors.InvalidData(),
                "DokumenLainnya.DuplicateResource" => DokumenLainnyaErrors.DuplicateResource(),
                "DokumenLainnya.InvalidLink" => DokumenLainnyaErrors.InvalidLink(),
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
            var error = DokumenLainnyaErrors.NotFound(id);

            // Assert
            error.Code.Should().Be("DokumenLainnya.NotFound");
            error.Description.Should().Be($"DokumenLainnya with identifier {id} not found");
            error.Type.Should().Be(ErrorType.NotFound);
        }

        [Fact]
        public void NotFoundPkm_ShouldReturnCorrectError()
        {
            // Arrange
            var id = Guid.NewGuid();

            // Act
            var error = DokumenLainnyaErrors.NotFoundPkm(id);

            // Assert
            error.Code.Should().Be("DokumenLainnya.NotFoundPkm");
            error.Description.Should().Be($"Pkm with identifier {id} not found");
            error.Type.Should().Be(ErrorType.NotFound);
        }
    }
}
