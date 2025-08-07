using FluentAssertions;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.AuthorSinta.Domain.AuthorSinta;
using Xunit;

namespace UnpakSipaksi.Modules.AuthorSinta.DomainTest
{
    public class AuthorSintaErrorsTests
    {
        [Theory]
        [InlineData("AuthorSinta.EmptyData", "data is not found", ErrorType.NotFound)]
        [InlineData("AuthorSinta.InvalidAuthorId", "Author identity is invalid format", ErrorType.NotFound)]
        [InlineData("AuthorSinta.InvalidSkor", "Skor is invalid format", ErrorType.NotFound)]
        [InlineData("AuthorSinta.InvalidNidn", "Nidn is invalid format", ErrorType.NotFound)]
        public void StaticError_ShouldReturnCorrectValues(string expectedCode, string expectedDescription, ErrorType expectedType)
        {
            // Act
            Error error = expectedCode switch
            {
                "AuthorSinta.EmptyData" => AuthorSintaErrors.EmptyData(),
                "AuthorSinta.InvalidAuthorId" => AuthorSintaErrors.InvalidAuthorId(),
                "AuthorSinta.InvalidSkor" => AuthorSintaErrors.InvalidSkor(),
                "AuthorSinta.InvalidNidn" => AuthorSintaErrors.InvalidNidn(),
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
            var error = AuthorSintaErrors.NotFound(id);

            // Assert
            error.Code.Should().Be("AuthorSinta.NotFound");
            error.Description.Should().Be($"Author sinta with identifier {id} not found");
            error.Type.Should().Be(ErrorType.NotFound);
        }
    }
}
