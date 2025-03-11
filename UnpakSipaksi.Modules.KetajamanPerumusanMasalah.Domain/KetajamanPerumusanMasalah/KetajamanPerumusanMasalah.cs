using System.ComponentModel.DataAnnotations.Schema;
using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.KetajamanPerumusanMasalah.Domain.KetajamanPerumusanMasalah
{
    public sealed partial class KetajamanPerumusanMasalah : Entity
    {
        private KetajamanPerumusanMasalah()
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

        public static KetajamanPerumusanMasalahBuilder Update(KetajamanPerumusanMasalah prev) => new KetajamanPerumusanMasalahBuilder(prev);

        public static Result<KetajamanPerumusanMasalah> Create(
        string Nama,
        int BobotPDP,
        int BobotTerapan,
        int BobotKerjasama,
        int BobotPenelitianDasar,
        int Skor
        )
        {
            var asset = new KetajamanPerumusanMasalah
            {
                Uuid = Guid.NewGuid(),
                Nama = Nama,
                BobotPDP = BobotPDP,
                BobotTerapan = BobotTerapan,
                BobotKerjasama = BobotKerjasama,
                BobotPenelitianDasar = BobotPenelitianDasar,
                Skor = Skor,
            };

            asset.Raise(new KetajamanPerumusanMasalahCreatedDomainEvent(asset.Uuid));

            return asset;
        }
    }
}
