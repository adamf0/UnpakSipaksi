using FluentAssertions;
using UnpakSipaksi.Modules.Roadmap.Domain.Roadmap;

namespace UnpakSipaksi.Modules.Roadmap.Domaintest
{
    public class RoadmapTests
    {
        [Fact]
        public void Create_WhenValidPropertiesProvided_ShouldReturnKategoriWithCorrectProperties()
        {
            // Arrange
            string nidn = "065117251";
            string link = "https://drive.google.com/file/d/abc123/view";

            // Act
            var result = Domain.Roadmap.Roadmap.Create(
                nidn,
                link
            );

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Nidn.Should().Be(nidn);
            result.Value.Link.Should().Be(link);
            result.Value.Uuid.Should().NotBe(Guid.Empty);
        }

        [Fact]
        public void Create_WhenValidPropertiesProvided_ShouldRaiseKategoriCreatedDomainEvent()
        {
            // Arrange
            string nidn = "065117251";
            string link = "https://drive.google.com/file/d/abc123/view";

            // Act
            var result = Domain.Roadmap.Roadmap.Create(
                nidn,
                link
            );

            // Assert
            result.Value.DomainEvents.Should().Contain(e => e is RoadmapCreatedDomainEvent);
        }
    }
}
