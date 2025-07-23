using FluentAssertions;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.ModelFeasibilityStudys.Domain.ModelFeasibilityStudys;

namespace UnpakSipaksi.Modules.ModelFeasibilityStudys.DomainTest
{
    public class ModelFeasibilityStudysErrorsTests
    {
        [Theory]
        [InlineData("ModelFeasibilityStudys.EmptyData", "data is not found", ErrorType.NotFound)]
        [InlineData("ModelFeasibilityStudys.UnknownKategoriSkema", "Unknown schema category", ErrorType.NotFound)]
        [InlineData("ModelFeasibilityStudys.InvalidValueSkor", "Invalid value 'skor'", ErrorType.NotFound)]
        [InlineData("ModelFeasibilityStudys.NotSameValue", "not the same value in data 'skor'", ErrorType.NotFound)]
        public void StaticError_ShouldReturnCorrectValues(string expectedCode, string expectedDescription, ErrorType expectedType)
        {
            // Act
            Error error = expectedCode switch
            {
                "ModelFeasibilityStudys.EmptyData" => ModelFeasibilityStudysErrors.EmptyData(),
                "ModelFeasibilityStudys.UnknownKategoriSkema" => ModelFeasibilityStudysErrors.UnknownKategoriSkema(),
                "ModelFeasibilityStudys.InvalidValueSkor" => ModelFeasibilityStudysErrors.InvalidValueSkor(),
                "ModelFeasibilityStudys.NotSameValue" => ModelFeasibilityStudysErrors.NotSameValue(),
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
            var error = ModelFeasibilityStudysErrors.NotFound(id);

            // Assert
            error.Code.Should().Be("ModelFeasibilityStudys.NotFound");
            error.Description.Should().Be($"Model feasibility study with the identifier {id} was not found");
            error.Type.Should().Be(ErrorType.NotFound);
        }
    }
}
