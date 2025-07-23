using FluentAssertions;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.Roadmap.Domain.Roadmap;

namespace UnpakSipaksi.Modules.Roadmap.Domaintest
{
    public class RoadmapErrorsTests
    {
        [Theory]
        [InlineData("Roadmap.EmptyData", "data is not found", ErrorType.NotFound)]
        [InlineData("Roadmap.InvalidLink", "Invalid link format", ErrorType.NotFound)]
        public void StaticError_ShouldReturnCorrectValues(string expectedCode, string expectedDescription, ErrorType expectedType)
        {
            // Act
            Error error = expectedCode switch
            {
                "Roadmap.EmptyData" => RoadmapErrors.EmptyData(),
                "Roadmap.InvalidLink" => RoadmapErrors.InvalidLink(),
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
            var error = RoadmapErrors.NotFound(id);

            // Assert
            error.Code.Should().Be("Roadmap.NotFound");
            error.Description.Should().Be($"Roadmap with the identifier {id} was not found");
            error.Type.Should().Be(ErrorType.NotFound);
        }
    }
}
