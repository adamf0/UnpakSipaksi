using FluentAssertions;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.PrioritasRiset.Domain.PrioritasRiset;

namespace UnpakSipaksi.Modules.PrioritasRiset.DomainTest
{
    public class PrioritasRisetErrorsTests
    {
        [Theory]
        [InlineData("PrioritasRiset.EmptyData", "data is not found", ErrorType.NotFound)]
        public void StaticError_ShouldReturnCorrectValues(string expectedCode, string expectedDescription, ErrorType expectedType)
        {
            // Act
            Error error = expectedCode switch
            {
                "PrioritasRiset.EmptyData" => PrioritasRisetErrors.EmptyData(),
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
            var error = PrioritasRisetErrors.NotFound(id);

            // Assert
            error.Code.Should().Be("PrioritasRiset.NotFound");
            error.Description.Should().Be($"Prioritas riset with the identifier {id} was not found");
            error.Type.Should().Be(ErrorType.NotFound);
        }
    }
}
