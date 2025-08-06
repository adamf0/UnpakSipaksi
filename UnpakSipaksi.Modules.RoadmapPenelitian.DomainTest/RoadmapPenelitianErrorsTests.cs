using FluentAssertions;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.RoadmapPenelitian.Domain.RoadmapPenelitian;

namespace UnpakSipaksi.Modules.RoadmapPenelitian.DomainTest
{
    public class RoadmapPenelitianErrorsTests
    {
        [Theory]
        [InlineData("RoadmapPenelitian.EmptyData", "data is not found", ErrorType.NotFound)]
        [InlineData("RoadmapPenelitian.UnknownKategoriSkema", "Unknown schema category", ErrorType.NotFound)]
        [InlineData("RoadmapPenelitian.InvalidValueSkor", "Invalid value 'skor'", ErrorType.NotFound)]
        [InlineData("RoadmapPenelitian.NotSameValue", "not the same value in data 'skor'", ErrorType.NotFound)]
        public void StaticError_ShouldReturnCorrectValues(string expectedCode, string expectedDescription, ErrorType expectedType)
        {
            // Act
            Error error = expectedCode switch
            {
                "RoadmapPenelitian.EmptyData" => RoadmapPenelitianErrors.EmptyData(),
                "RoadmapPenelitian.UnknownKategoriSkema" => RoadmapPenelitianErrors.UnknownKategoriSkema(),
                "RoadmapPenelitian.InvalidValueSkor" => RoadmapPenelitianErrors.InvalidValueSkor(),
                "RoadmapPenelitian.NotSameValue" => RoadmapPenelitianErrors.NotSameValue(),
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
            var error = RoadmapPenelitianErrors.NotFound(id);

            // Assert
            error.Code.Should().Be("RoadmapPenelitian.NotFound");
            error.Description.Should().Be($"Roadmap Penelitian with the identifier {id} was not found");
            error.Type.Should().Be(ErrorType.NotFound);
        }
    }
}
