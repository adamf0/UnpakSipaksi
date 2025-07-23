using FluentAssertions;
using UnpakSipaksi.Common.Domain;
using static UnpakSipaksi.Modules.ModelFeasibilityStudys.Domain.ModelFeasibilityStudys.ModelFeasibilityStudys;

namespace UnpakSipaksi.Modules.ModelFeasibilityStudys.DomainTest
{
    public class ModelFeasibilityStudysBuilderTests
    {
        [Fact]
        public void Build_WhenFieldsChanged_ShouldReturnUpdatedModelFeasibilityStudys()
        {
            // Arrange
            string Nama = "ABC";
            int Skor = 1;

            var createResult = Domain.ModelFeasibilityStudys.ModelFeasibilityStudys.Create(
                Nama,
                Skor
            );
            var ModelFeasibilityStudys = createResult.Value;

            // Act
            string newNama = "ACC";
            int newSkor = 10;

            ModelFeasibilityStudysBuilder builder = Domain.ModelFeasibilityStudys.ModelFeasibilityStudys.Update(ModelFeasibilityStudys);
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

            var createResult = Domain.ModelFeasibilityStudys.ModelFeasibilityStudys.Create(
                Nama,
                Skor
            );
            var ModelFeasibilityStudys = createResult.Value;

            // Act
            string newNama = "ACC";
            int newSkor = -110;

            ModelFeasibilityStudysBuilder builder = Domain.ModelFeasibilityStudys.ModelFeasibilityStudys.Update(ModelFeasibilityStudys);
            builder.ChangeNama(newNama)
                    .ChangeSkor(newSkor);
            var result = builder.Build();

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Code.Should().Be("ModelFeasibilityStudys.InvalidValueSkor");
            result.Error.Description.Should().Be("Invalid value 'skor'");
            result.Error.Type.Should().Be(ErrorType.NotFound);
        }
    }
}
