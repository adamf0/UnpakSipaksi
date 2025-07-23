using FluentAssertions;
using UnpakSipaksi.Common.Domain;
using static UnpakSipaksi.Modules.PenugasanReviewer.Domain.PenugasanReviewer.PenugasanReviewer;

namespace UnpakSipaksi.Modules.PenugasanReviewer.DomainTest
{
    public class PenugasanReviewerBuilderTests
    {
        [Fact]
        public void Build_WhenFieldsChanged_ShouldReturnUpdatedPenugasanReviewer()
        {
            // Arrange
            string Nidn = "065117251";
            int Status = 1;

            var createResult = Domain.PenugasanReviewer.PenugasanReviewer.Create(
                Nidn,
                Status
            );
            var PenugasanReviewer = createResult.Value;

            // Act
            string newNidn = "065117252";
            int newStatus = 0;

            PenugasanReviewerBuilder builder = Domain.PenugasanReviewer.PenugasanReviewer.Update(PenugasanReviewer);
            builder.ChangeNidn(newNidn)
                    .ChangeStatus(newStatus);
            var result = builder.Build();

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Nidn.Should().Be(newNidn);
            result.Value.Status.Should().Be(newStatus);
            result.Value.Uuid.Should().NotBe(Guid.Empty);
        }

        [Fact]
        public void Build_WithInvalidStatus_ShouldReturnExpectedValidationError()
        {
            // Arrange
            string Nidn = "065117251";
            int Status = 1;

            var createResult = Domain.PenugasanReviewer.PenugasanReviewer.Create(
                Nidn,
                Status
            );
            var PenugasanReviewer = createResult.Value;

            // Act
            string newNidn = "065117252";
            int newStatus = -110;

            PenugasanReviewerBuilder builder = Domain.PenugasanReviewer.PenugasanReviewer.Update(PenugasanReviewer);
            builder.ChangeNidn(newNidn)
                    .ChangeStatus(newStatus);
            var result = builder.Build();

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Code.Should().Be("PenugasanReviewer.InvalidValueStatus");
            result.Error.Description.Should().Be("Invalid value 'status'");
            result.Error.Type.Should().Be(ErrorType.NotFound);
        }
    }
}
