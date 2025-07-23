using FluentAssertions;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.Metode.Domain.Metode;

namespace UnpakSipaksi.Modules.Metode.DomainTest
{
    public class MetodeErrorsTests
    {
        [Theory]
        [InlineData("Metode.EmptyData", "data is not found", ErrorType.NotFound)]
        [InlineData("Metode.AkurasiPenelitianNotFound", "data AkurasiPenelitian is not found", ErrorType.NotFound)]
        [InlineData("Metode.KejelasanPembagianTugasTimTaskNotFound", "data KejelasanPembagianTugasTimTask is not found", ErrorType.NotFound)]
        [InlineData("Metode.KesesuaianWaktuRabLuaranFasilitasNotFound", "data KesesuaianWaktuRabLuaranFasilitas is not found", ErrorType.NotFound)]
        [InlineData("Metode.PotensiKetercapaianLuaranDijanjikanNotFound", "data PotensiKetercapaianLuaranDijanjikan is not found", ErrorType.NotFound)]
        [InlineData("Metode.ModelFeasibilityStudyNotFound", "data ModelFeasibilityStudy is not found", ErrorType.NotFound)]
        [InlineData("Metode.KesesuaianTktNotFound", "data KesesuaianTkt is not found", ErrorType.NotFound)]
        [InlineData("Metode.KredibilitasMitraDukunganNotFound", "data ModelFeasibilityStudy is not found", ErrorType.NotFound)]
        public void StaticError_ShouldReturnCorrectValues(string expectedCode, string expectedDescription, ErrorType expectedType)
        {
            // Act
            Error error = expectedCode switch
            {
                "Metode.EmptyData" => MetodeErrors.EmptyData(),
                "Metode.AkurasiPenelitianNotFound" => MetodeErrors.AkurasiPenelitianNotFound(),
                "Metode.KejelasanPembagianTugasTimTaskNotFound" => MetodeErrors.KejelasanPembagianTugasTimTaskNotFound(),
                "Metode.KesesuaianWaktuRabLuaranFasilitasNotFound" => MetodeErrors.KesesuaianWaktuRabLuaranFasilitasNotFound(),
                "Metode.PotensiKetercapaianLuaranDijanjikanNotFound" => MetodeErrors.PotensiKetercapaianLuaranDijanjikanNotFound(),
                "Metode.ModelFeasibilityStudyNotFound" => MetodeErrors.ModelFeasibilityStudyNotFound(),
                "Metode.KesesuaianTktNotFound" => MetodeErrors.KesesuaianTktNotFound(),
                "Metode.KredibilitasMitraDukunganNotFound" => MetodeErrors.KredibilitasMitraDukunganNotFound(),
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
            var error = MetodeErrors.NotFound(id);

            // Assert
            error.Code.Should().Be("Metode.NotFound");
            error.Description.Should().Be($"Metode with the identifier {id} was not found");
            error.Type.Should().Be(ErrorType.NotFound);
        }
    }
}
