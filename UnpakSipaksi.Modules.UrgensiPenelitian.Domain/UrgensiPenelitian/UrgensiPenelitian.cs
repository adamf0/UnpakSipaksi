using System.ComponentModel.DataAnnotations.Schema;
using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.UrgensiPenelitian.Domain.UrgensiPenelitian
{
    public sealed partial class UrgensiPenelitian : Entity
    {
        private UrgensiPenelitian()
        {
        }

        public int? Id { get; private set; } = null;

        [Column(TypeName = "VARCHAR(36)")]
        public Guid Uuid { get; private set; }

        public int RelevansiProdukKepentinganNasionalId { get; private set; }
        public int KetajamanPerumusanMasalahId { get; private set; }
        public int InovasiPemecahanMasalahId { get; private set; }
        public int SotaKebaharuanId { get; private set; }
        public int RoadmapPenelitianId { get; private set; }

        public static UrgensiPenelitianBuilder Update(UrgensiPenelitian prev) => new UrgensiPenelitianBuilder(prev);

        public static Result<UrgensiPenelitian> Create(
        int RelevansiProdukKepentinganNasionalId,
        int KetajamanPerumusanMasalahId,
        int InovasiPemecahanMasalahId,
        int SotaKebaharuanId,
        int RoadmapPenelitianId
        )
        {
            var asset = new UrgensiPenelitian
            {
                Uuid = Guid.NewGuid(),
                RelevansiProdukKepentinganNasionalId = RelevansiProdukKepentinganNasionalId,
                KetajamanPerumusanMasalahId = KetajamanPerumusanMasalahId,
                InovasiPemecahanMasalahId = InovasiPemecahanMasalahId,
                SotaKebaharuanId = SotaKebaharuanId,
                RoadmapPenelitianId = RoadmapPenelitianId
            };

            asset.Raise(new UrgensiPenelitianCreatedDomainEvent(asset.Uuid));

            return asset;
        }
    }
}
