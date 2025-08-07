using FluentAssertions;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.PenugasanReviewer.Domain.PenugasanReviewer;

namespace UnpakSipaksi.Modules.PenugasanReviewer.DomainTest
{
    public class PenugasanReviewerErrorsTests
    {
        [Theory]
        [InlineData("PenugasanReviewer.EmptyData", "data is not found", ErrorType.NotFound)]
        [InlineData("PenugasanReviewer.InvalidValueStatus", "Invalid value 'status'", ErrorType.NotFound)]
        [InlineData("PenugasanReviewer.InvalidNidn", "Nidn is invalid format", ErrorType.NotFound)]
        public void StaticError_ShouldReturnCorrectValues(string expectedCode, string expectedDescription, ErrorType expectedType)
        {
            // Act
            Error error = expectedCode switch
            {
                "PenugasanReviewer.EmptyData" => PenugasanReviewerErrors.EmptyData(),
                "PenugasanReviewer.InvalidValueStatus" => PenugasanReviewerErrors.InvalidValueStatus(),
                "PenugasanReviewer.InvalidNidn" => PenugasanReviewerErrors.InvalidNidn(),
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
            var error = PenugasanReviewerErrors.NotFound(id);

            // Assert
            error.Code.Should().Be("PenugasanReviewer.NotFound");
            error.Description.Should().Be($"Penugasan reviewer with the identifier {id} was not found");
            error.Type.Should().Be(ErrorType.NotFound);
        }
    }
}
