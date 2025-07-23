using FluentAssertions;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.FokusPenelitian.Domain.FokusPenelitian;
using Xunit;

namespace UnpakSipaksi.Modules.FokusPenelitian.DomainTest
{
    public class FokusPenelitianErrorsTests
    {
        [Theory]
        [InlineData("FokusPenelitian.EmptyData", "data is not found", ErrorType.NotFound)]
        public void StaticError_ShouldReturnCorrectValues(string expectedCode, string expectedDescription, ErrorType expectedType)
        {
            // Act
            Error error = expectedCode switch
            {
                "FokusPenelitian.EmptyData" => FokusPenelitianErrors.EmptyData(),
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
            var error = FokusPenelitianErrors.NotFound(id);

            // Assert
            error.Code.Should().Be("FokusPenelitian.NotFound");
            error.Description.Should().Be($"Fokus penelitian with identifier {id} not found");
            error.Type.Should().Be(ErrorType.NotFound);
        }
    }
}
