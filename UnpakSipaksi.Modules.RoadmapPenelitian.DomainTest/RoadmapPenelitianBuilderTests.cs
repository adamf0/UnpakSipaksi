using FluentAssertions;
using UnpakSipaksi.Common.Domain;
using static UnpakSipaksi.Modules.RoadmapPenelitian.Domain.RoadmapPenelitian.RoadmapPenelitian;

namespace UnpakSipaksi.Modules.RoadmapPenelitian.DomainTest
{
    public class RoadmapPenelitianBuilderTests
    {
        [Fact]
        public void Build_WhenFieldsChanged_ShouldReturnUpdatedRoadmapPenelitian()
        {
            // Arrange
            string Nama = "ABC";
            int Skor = 1;

            var createResult = Domain.RoadmapPenelitian.RoadmapPenelitian.Create(
                Nama,
                Skor
            );
            var RoadmapPenelitian = createResult.Value;

            // Act
            string newNama = "ACC";
            int newSkor = 10;

            RoadmapPenelitianBuilder builder = Domain.RoadmapPenelitian.RoadmapPenelitian.Update(RoadmapPenelitian);
            builder.ChangeNama(newNama)
                    .ChangeSkor(newSkor);
            var result = builder.Build();

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Nama.Should().Be(newNama);
            result.Value.Skor.Should().Be(newSkor);
            result.Value.Uuid.Should().NotBe(Guid.Empty);
        }

        [Fact]
        public void Build_WithInvalidSkor_ShouldReturnExpectedValidationError()
        {
            // Arrange
            string Nama = "ABC";
            int Skor = 1;

            var createResult = Domain.RoadmapPenelitian.RoadmapPenelitian.Create(
                Nama,
                Skor
            );
            var RoadmapPenelitian = createResult.Value;

            // Act
            string newNama = "ACC";
            int newSkor = -110;

            RoadmapPenelitianBuilder builder = Domain.RoadmapPenelitian.RoadmapPenelitian.Update(RoadmapPenelitian);
            builder.ChangeNama(newNama)
                    .ChangeSkor(newSkor);
            var result = builder.Build();

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Code.Should().Be("RoadmapPenelitian.InvalidValueSkor");
            result.Error.Description.Should().Be("Invalid value 'skor'");
            result.Error.Type.Should().Be(ErrorType.NotFound);
        }
    }
}
