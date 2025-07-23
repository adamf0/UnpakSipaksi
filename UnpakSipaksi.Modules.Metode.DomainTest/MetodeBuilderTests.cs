using FluentAssertions;
using UnpakSipaksi.Common.Domain;
using static UnpakSipaksi.Modules.Metode.Domain.Metode.Metode;

namespace UnpakSipaksi.Modules.Metode.DomainTest
{
    public class MetodeBuilderTests
    {
        [Fact]
        public void Build_WhenFieldsChanged_ShouldReturnUpdatedMetode()
        {
            // Arrange
            int AkurasiPenelitianId = 1;
            int KejelasanPembagianTugasTimId = 1;
            int KesesuaianWaktuRabLuaranFasilitasId = 1;
            int PotensiKetercapaianLuaranDijanjikanId = 1;
            int ModelFeasibilityStudyId = 1;
            int KesesuaianTktId = 1;
            int KredibilitasMitraDukunganId = 1;

            var createResult = Domain.Metode.Metode.Create(
                AkurasiPenelitianId,
                KejelasanPembagianTugasTimId,
                KesesuaianWaktuRabLuaranFasilitasId,
                PotensiKetercapaianLuaranDijanjikanId,
                ModelFeasibilityStudyId,
                KesesuaianTktId,
                KredibilitasMitraDukunganId
            );
            var Metode = createResult.Value;

            // Act
            int newAkurasiPenelitianId = 10;
            int newKejelasanPembagianTugasTimId = 10;
            int newKesesuaianWaktuRabLuaranFasilitasId = 10;
            int newPotensiKetercapaianLuaranDijanjikanId = 10;
            int newModelFeasibilityStudyId = 10;
            int newKesesuaianTktId = 10;
            int newKredibilitasMitraDukunganId = 10;

            MetodeBuilder builder = Domain.Metode.Metode.Update(Metode);
            builder.ChangeAkurasiPenelitian(newAkurasiPenelitianId)
                   .ChangeKejelasanPembagianTugasTim(newKejelasanPembagianTugasTimId)
                   .ChangeKesesuaianWaktuRabLuaranFasilitas(newKesesuaianWaktuRabLuaranFasilitasId)
                   .ChangePotensiKetercapaianLuaranDijanjikan(newPotensiKetercapaianLuaranDijanjikanId)
                   .ChangeModelFeasibilityStudy(newModelFeasibilityStudyId)
                   .ChangeKesesuaianTkt(newKesesuaianTktId)
                   .ChangeKredibilitasMitraDukungan(newKredibilitasMitraDukunganId);
            var result = builder.Build();

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.AkurasiPenelitianId.Should().Be(newAkurasiPenelitianId);
            result.Value.KejelasanPembagianTugasTimId.Should().Be(newKejelasanPembagianTugasTimId);
            result.Value.KesesuaianWaktuRabLuaranFasilitasId.Should().Be(newKesesuaianWaktuRabLuaranFasilitasId);
            result.Value.PotensiKetercapaianLuaranDijanjikanId.Should().Be(newPotensiKetercapaianLuaranDijanjikanId);
            result.Value.ModelFeasibilityStudyId.Should().Be(newModelFeasibilityStudyId);
            result.Value.KesesuaianTktId.Should().Be(newKesesuaianTktId);
            result.Value.KredibilitasMitraDukunganId.Should().Be(newKredibilitasMitraDukunganId);
            result.Value.Uuid.Should().NotBe(Guid.Empty);
        }

        [Theory]
        [InlineData("Metode.AkurasiPenelitianNotFound", "data AkurasiPenelitian is not found", ErrorType.NotFound, -1, 1,1,1,1,1,1)]
        [InlineData("Metode.KejelasanPembagianTugasTimTaskNotFound", "data KejelasanPembagianTugasTimTask is not found", ErrorType.NotFound, 1, -1, 1, 1, 1, 1, 1)]
        [InlineData("Metode.KesesuaianWaktuRabLuaranFasilitasNotFound", "data KesesuaianWaktuRabLuaranFasilitas is not found", ErrorType.NotFound, 1, 1, -1, 1, 1, 1, 1)]
        [InlineData("Metode.PotensiKetercapaianLuaranDijanjikanNotFound", "data PotensiKetercapaianLuaranDijanjikan is not found", ErrorType.NotFound, 1, 1, 1, -1, 1, 1, 1)]
        [InlineData("Metode.ModelFeasibilityStudyNotFound", "data ModelFeasibilityStudy is not found", ErrorType.NotFound, 1, 1, 1, 1, -1, 1, 1)]
        [InlineData("Metode.KesesuaianTktNotFound", "data KesesuaianTkt is not found", ErrorType.NotFound, 1, 1, 1, 1, 1, -1, 1)]
        [InlineData("Metode.KredibilitasMitraDukunganNotFound", "data ModelFeasibilityStudy is not found", ErrorType.NotFound, 1, 1, 1, 1, 1, 1, -1)]
        public void Build_WithInvalidValue_ShouldReturnExpectedValidationError(
            string expectedCode, 
            string expectedDescription, 
            ErrorType expectedType,
            int newAkurasiPenelitianId,
            int newKejelasanPembagianTugasTimId,
            int newKesesuaianWaktuRabLuaranFasilitasId,
            int newPotensiKetercapaianLuaranDijanjikanId,
            int newModelFeasibilityStudyId,
            int newKesesuaianTktId,
            int newKredibilitasMitraDukunganId
        )
        {
            // Arrange
            int AkurasiPenelitianId = 1;
            int KejelasanPembagianTugasTimId = 1;
            int KesesuaianWaktuRabLuaranFasilitasId = 1;
            int PotensiKetercapaianLuaranDijanjikanId = 1;
            int ModelFeasibilityStudyId = 1;
            int KesesuaianTktId = 1;
            int KredibilitasMitraDukunganId = 1;

            var createResult = Domain.Metode.Metode.Create(
                AkurasiPenelitianId,
                KejelasanPembagianTugasTimId,
                KesesuaianWaktuRabLuaranFasilitasId,
                PotensiKetercapaianLuaranDijanjikanId,
                ModelFeasibilityStudyId,
                KesesuaianTktId,
                KredibilitasMitraDukunganId
            );
            var Metode = createResult.Value;

            // Act
            MetodeBuilder builder = Domain.Metode.Metode.Update(Metode);
            builder.ChangeAkurasiPenelitian(newAkurasiPenelitianId)
                   .ChangeKejelasanPembagianTugasTim(newKejelasanPembagianTugasTimId)
                   .ChangeKesesuaianWaktuRabLuaranFasilitas(newKesesuaianWaktuRabLuaranFasilitasId)
                   .ChangePotensiKetercapaianLuaranDijanjikan(newPotensiKetercapaianLuaranDijanjikanId)
                   .ChangeModelFeasibilityStudy(newModelFeasibilityStudyId)
                   .ChangeKesesuaianTkt(newKesesuaianTktId)
                   .ChangeKredibilitasMitraDukungan(newKredibilitasMitraDukunganId);

            var result = builder.Build();

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Code.Should().Be(expectedCode);
            result.Error.Description.Should().Be(expectedDescription);
            result.Error.Type.Should().Be(expectedType);
        }
    }
}
