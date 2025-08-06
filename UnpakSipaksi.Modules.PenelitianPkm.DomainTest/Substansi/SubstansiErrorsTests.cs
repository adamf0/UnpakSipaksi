using System;
using FluentAssertions;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.PenelitianPkm.Domain.Substansi;
using Xunit;

namespace UnpakSipaksi.Modules.PenelitianPkm.DomainTest.Substansi
{
    public class SubstansiErrorsTests
    {
        [Theory]
        [InlineData("Substansi.EmptyData", "data is not found", ErrorType.NotFound)]
        [InlineData("Substansi.EmptyFile", "file can't be empty", ErrorType.NotFound)]
        public void StaticError_ShouldReturnCorrectValues(string expectedCode, string expectedDescription, ErrorType expectedType)
        {
            // Act
            Error error = expectedCode switch
            {
                "Substansi.EmptyData" => SubstansiErrors.EmptyData(),
                "Substansi.EmptyFile" => SubstansiErrors.EmptyFile(),
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
            var error = SubstansiErrors.NotFound(id);

            // Assert
            error.Code.Should().Be("Substansi.NotFound");
            error.Description.Should().Be($"Substansi file with identifier {id} not found");
            error.Type.Should().Be(ErrorType.NotFound);
        }

        [Fact]
        public void NotFoundHibah_ShouldReturnCorrectError()
        {
            // Arrange
            var id = Guid.NewGuid();

            // Act
            var error = SubstansiErrors.NotFoundHibah(id);

            // Assert
            error.Code.Should().Be("Substansi.NotFoundHibah");
            error.Description.Should().Be($"Penelitian hibah with identifier {id} not found");
            error.Type.Should().Be(ErrorType.NotFound);
        }
    }
}
