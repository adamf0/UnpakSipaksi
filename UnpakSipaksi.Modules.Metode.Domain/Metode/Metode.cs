using System.ComponentModel.DataAnnotations.Schema;
using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.Metode.Domain.Metode
{
    public sealed partial class Metode : Entity
    {
        private Metode()
        {
        }

        public int? Id { get; private set; } = null;

        [Column(TypeName = "VARCHAR(36)")]
        public Guid Uuid{ get; private set; }

        public int AkurasiPenelitianId { get; private set; } = 0;
        public int KejelasanPembagianTugasTimId { get; private set; } = 0;
        public int KesesuaianWaktuRabLuaranFasilitasId { get; private set; } = 0;
        public int PotensiKetercapaianLuaranDijanjikanId { get; private set; } = 0;
        public int ModelFeasibilityStudyId { get; private set; } = 0;
        public int KesesuaianTktId { get; private set; } = 0;
        public int KredibilitasMitraDukunganId { get; private set; } = 0;

        public static MetodeBuilder Update(Metode prev) => new MetodeBuilder(prev);

        public static Result<Metode> Create(
        int AkurasiPenelitianId,
        int KejelasanPembagianTugasTimId,
        int KesesuaianWaktuRabLuaranFasilitasId,
        int PotensiKetercapaianLuaranDijanjikanId,
        int ModelFeasibilityStudyId,
        int KesesuaianTktId,
        int KredibilitasMitraDukunganId

        )
        {
            var asset = new Metode
            {
                Uuid = Guid.NewGuid(),
                AkurasiPenelitianId = AkurasiPenelitianId,
                KejelasanPembagianTugasTimId = KejelasanPembagianTugasTimId,
                KesesuaianWaktuRabLuaranFasilitasId = KesesuaianWaktuRabLuaranFasilitasId,
                PotensiKetercapaianLuaranDijanjikanId = PotensiKetercapaianLuaranDijanjikanId,
                ModelFeasibilityStudyId = ModelFeasibilityStudyId,
                KesesuaianTktId = KesesuaianTktId,
                KredibilitasMitraDukunganId = KredibilitasMitraDukunganId
            };

            asset.Raise(new MetodeCreatedDomainEvent(asset.Uuid));

            return asset;
        }
    }
}
