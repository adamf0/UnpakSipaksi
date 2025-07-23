using FluentAssertions;
using UnpakSipaksi.Common.Domain;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static UnpakSipaksi.Modules.Roadmap.Domain.Roadmap.Roadmap;

namespace UnpakSipaksi.Modules.Roadmap.Domaintest
{
    public class RoadmapBuilderTests
    {
        [Fact]
        public void Build_WhenFieldsChanged_ShouldReturnUpdatedRoadmap()
        {
            // Arrange
            string nidn = "065117251";
            string link = "https://drive.google.com/file/d/abc123/view";

            var createResult = Domain.Roadmap.Roadmap.Create(
                nidn,
                link
            );
            var Roadmap = createResult.Value;

            // Act
            string newNidn = "065117252";
            string newLink = "https://drive.google.com/file/d/abc124/view";

            RoadmapBuilder builder = Domain.Roadmap.Roadmap.Update(Roadmap);
            builder.ChangeNidn(newNidn)
                .ChangeLink(newLink);
            var result = builder.Build();

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Nidn.Should().Be(newNidn);
            result.Value.Link.Should().Be(newLink);
            result.Value.Uuid.Should().NotBe(Guid.Empty);
        }

        [Fact]
        public void Build_WhenInvalidLinkChanged_ShouldReturnInvalidRoadmap()
        {
            // Arrange
            string nidn = "065117251";
            string link = "https://drive.google.com/file/d/abc123/view";

            var createResult = Domain.Roadmap.Roadmap.Create(
                nidn,
                link
            );
            var Roadmap = createResult.Value;

            // Act
            string newNidn = "065117252";
            string newLink = "https://targte.com/yyyy";

            RoadmapBuilder builder = Domain.Roadmap.Roadmap.Update(Roadmap);
            builder.ChangeNidn(newNidn)
                .ChangeLink(newLink);
            var result = builder.Build();

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Code.Should().Be("Roadmap.InvalidLink");
            result.Error.Description.Should().Be("Invalid link format");
            result.Error.Type.Should().Be(ErrorType.NotFound);
        }
    }
}
