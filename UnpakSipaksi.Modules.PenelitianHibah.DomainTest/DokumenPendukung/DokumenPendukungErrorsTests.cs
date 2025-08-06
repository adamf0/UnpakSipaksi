using System;
using FluentAssertions;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.PenelitianHibah.Domain.DokumenPendukung;
using Xunit;

namespace UnpakSipaksi.Modules.PenelitianHibah.DomainTest.DokumenPendukung
{
    public class DokumenPendukungErrorsTests
    {
        [Theory]
        [InlineData("DokumenPendukung.EmptyData", "data is not found", ErrorType.NotFound)]
        [InlineData("DokumenPendukung.InvalidData", "Hibah penelitian is not match existing data", ErrorType.NotFound)]
        [InlineData("DokumenPendukung.EmptyResource", "Resource is not found", ErrorType.NotFound)]
        [InlineData("DokumenPendukung.DuplicateResource", "Resource is duplicate source", ErrorType.NotFound)]
        [InlineData("DokumenPendukung.InvalidLink", "Invalid link", ErrorType.NotFound)]
        public void StaticError_ShouldReturnCorrectValues(string expectedCode, string expectedDescription, ErrorType expectedType)
        {
            // Act
            Error error = expectedCode switch
            {
                "DokumenPendukung.EmptyData" => DokumenPendukungErrors.EmptyData(),
                "DokumenPendukung.InvalidData" => DokumenPendukungErrors.InvalidData(),
                "DokumenPendukung.EmptyResource" => DokumenPendukungErrors.EmptyResource(),
                "DokumenPendukung.DuplicateResource" => DokumenPendukungErrors.DuplicateResource(),
                "DokumenPendukung.InvalidLink" => DokumenPendukungErrors.InvalidLink(),
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
            var error = DokumenPendukungErrors.NotFound(id);

            // Assert
            error.Code.Should().Be("DokumenPendukung.NotFound");
            error.Description.Should().Be($"DokumenPendukung with identifier {id} not found");
            error.Type.Should().Be(ErrorType.NotFound);
        }

        [Fact]
        public void NotFoundHibah_ShouldReturnCorrectError()
        {
            // Arrange
            var id = Guid.NewGuid();

            // Act
            var error = DokumenPendukungErrors.NotFoundHibah(id);

            // Assert
            error.Code.Should().Be("DokumenPendukung.NotFoundHibah");
            error.Description.Should().Be($"Penelitian hibah with identifier {id} not found");
            error.Type.Should().Be(ErrorType.NotFound);
        }
    }
}
