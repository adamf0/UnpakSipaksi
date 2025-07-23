using System.ComponentModel.DataAnnotations.Schema;
using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.RelevansiKualitasReferensi.Domain.RelevansiKualitasReferensi
{
    public sealed partial class RelevansiKualitasReferensi : Entity
    {
        private RelevansiKualitasReferensi()
        {
        }

        public int? Id { get; private set; } = null;

        [Column(TypeName = "VARCHAR(36)")]
        public Guid Uuid{ get; private set; }

        [Column("name")]
        public string Nama { get; private set; } = null!;
        public int BobotPDP { get; private set; } = 0;
        public int BobotTerapan { get; private set; } = 0;
        public int BobotKerjasama { get; private set; } = 0;
        public int BobotPenelitianDasar { get; private set; } = 0;
        public int Skor { get; private set; } = 0;

        public static RelevansiKualitasReferensiBuilder Update(RelevansiKualitasReferensi prev) => new RelevansiKualitasReferensiBuilder(prev);

        public static Result<RelevansiKualitasReferensi> Create(
        string Nama,
        int Skor
        )
        {
            if (Skor < 0) {
                return Result.Failure<RelevansiKualitasReferensi>(RelevansiKualitasReferensiErrors.InvalidValueSkor());
            }
            var asset = new RelevansiKualitasReferensi
            {
                Uuid = Guid.NewGuid(),
                Nama = Nama,
                Skor = Skor,
            };

            asset.Raise(new RelevansiKualitasReferensiCreatedDomainEvent(asset.Uuid));

            return asset;
        }
    }
}
