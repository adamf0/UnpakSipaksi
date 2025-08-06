using FluentAssertions;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.PenelitianHibah.Domain.DokumenKontrak;

namespace UnpakSipaksi.Modules.PenelitianHibah.DomainTest.DokumenKontrak
{
    public class DokumenKontrakErrorsTests
    {
        [Theory]
        [InlineData("DokumenKontrak.EmptyData", "data is not found", ErrorType.NotFound)]
        [InlineData("DokumenKontrak.EmptyResource", "Resource is not found", ErrorType.NotFound)]
        [InlineData("DokumenKontrak.InvalidData", "Hibah penelitian is not match existing data", ErrorType.NotFound)]
        public void StaticError_ShouldReturnCorrectValues(string expectedCode, string expectedDescription, ErrorType expectedType)
        {
            // Act
            Error error = expectedCode switch
            {
                "DokumenKontrak.EmptyData" => DokumenKontrakErrors.EmptyData(),
                "DokumenKontrak.EmptyResource" => DokumenKontrakErrors.EmptyResource(),
                "DokumenKontrak.InvalidData" => DokumenKontrakErrors.InvalidData(),
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
            var error = DokumenKontrakErrors.NotFound(id);

            // Assert
            error.Code.Should().Be("DokumenKontrak.NotFound");
            error.Description.Should().Be($"DokumenKontrak with identifier {id} not found");
            error.Type.Should().Be(ErrorType.NotFound);
        }

        [Fact]
        public void NotFoundHibah_ShouldReturnCorrectError()
        {
            // Arrange
            var id = Guid.NewGuid();

            // Act
            var error = DokumenKontrakErrors.NotFoundHibah(id);

            // Assert
            error.Code.Should().Be("DokumenKontrak.NotFoundHibah");
            error.Description.Should().Be($"Penelitian hibah with identifier {id} not found");
            error.Type.Should().Be(ErrorType.NotFound);
        }
    }
}
