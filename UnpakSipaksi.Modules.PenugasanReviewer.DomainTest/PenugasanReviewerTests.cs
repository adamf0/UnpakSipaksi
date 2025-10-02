using FluentAssertions;
using UnpakSipaksi.Modules.PenugasanReviewer.Domain.PenugasanReviewer;

namespace UnpakSipaksi.Modules.PenugasanReviewer.DomainTest
{
    public class PenugasanReviewerTests
    {
        [Fact]
        public void Create_WhenValidPropertiesProvided_ShouldReturnKategoriWithCorrectProperties()
        {
            // Arrange
            string nidn = "1234567890";
            int status = 1;

            // Act
            var result = Domain.PenugasanReviewer.PenugasanReviewer.Create(
                nidn,
                status
            );

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Nidn.Should().Be(nidn);
            result.Value.Status.Should().Be(status);
            result.Value.Uuid.Should().NotBe(Guid.Empty);
        }

        [Fact]
        public void Create_WhenValidPropertiesProvided_ShouldRaiseKategoriCreatedDomainEvent()
        {
            // Arrange
            string nidn = "1234567890";
            int status = 1;

            // Act
            var result = Domain.PenugasanReviewer.PenugasanReviewer.Create(
                nidn,
                status
            );

            // Assert
            result.Value.DomainEvents.Should().Contain(e => e is PenugasanReviewerCreatedDomainEvent);
        }
    }
}
