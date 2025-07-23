using FluentAssertions;
using UnpakSipaksi.Modules.Metode.Domain.Metode;

namespace UnpakSipaksi.Modules.Metode.DomainTest
{
    public class MetodeTests
    {
        [Fact]
        public void Create_WhenValidPropertiesProvided_ShouldReturnKategoriWithCorrectProperties()
        {
            // Arrange
            int AkurasiPenelitianId = 1;
            int KejelasanPembagianTugasTimId = 1;
            int KesesuaianWaktuRabLuaranFasilitasId = 1;
            int PotensiKetercapaianLuaranDijanjikanId = 1;
            int ModelFeasibilityStudyId = 1;
            int KesesuaianTktId = 1;
            int KredibilitasMitraDukunganId = 1;

            // Act
            var result = Domain.Metode.Metode.Create(
                AkurasiPenelitianId,
                KejelasanPembagianTugasTimId,
                KesesuaianWaktuRabLuaranFasilitasId,
                PotensiKetercapaianLuaranDijanjikanId,
                ModelFeasibilityStudyId,
                KesesuaianTktId,
                KredibilitasMitraDukunganId
            );

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.AkurasiPenelitianId.Should().Be(AkurasiPenelitianId);
            result.Value.KejelasanPembagianTugasTimId.Should().Be(KejelasanPembagianTugasTimId);
            result.Value.KesesuaianWaktuRabLuaranFasilitasId.Should().Be(KesesuaianWaktuRabLuaranFasilitasId);
            result.Value.PotensiKetercapaianLuaranDijanjikanId.Should().Be(PotensiKetercapaianLuaranDijanjikanId);
            result.Value.ModelFeasibilityStudyId.Should().Be(ModelFeasibilityStudyId);
            result.Value.KesesuaianTktId.Should().Be(KesesuaianTktId);
            result.Value.KredibilitasMitraDukunganId.Should().Be(KredibilitasMitraDukunganId);
            result.Value.Uuid.Should().NotBe(Guid.Empty);
        }

        [Fact]
        public void Create_WhenValidPropertiesProvided_ShouldRaiseKategoriCreatedDomainEvent()
        {
            // Arrange
            int AkurasiPenelitianId = 1;
            int KejelasanPembagianTugasTimId = 1;
            int KesesuaianWaktuRabLuaranFasilitasId = 1;
            int PotensiKetercapaianLuaranDijanjikanId = 1;
            int ModelFeasibilityStudyId = 1;
            int KesesuaianTktId = 1;
            int KredibilitasMitraDukunganId = 1;

            // Act
            var result = Domain.Metode.Metode.Create(
                AkurasiPenelitianId,
                KejelasanPembagianTugasTimId,
                KesesuaianWaktuRabLuaranFasilitasId,
                PotensiKetercapaianLuaranDijanjikanId,
                ModelFeasibilityStudyId,
                KesesuaianTktId,
                KredibilitasMitraDukunganId
            );

            // Assert
            result.Value.DomainEvents.Should().Contain(e => e is MetodeCreatedDomainEvent);
        }
    }
}
