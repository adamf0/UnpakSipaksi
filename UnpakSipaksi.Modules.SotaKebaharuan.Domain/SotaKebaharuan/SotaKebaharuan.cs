using System.ComponentModel.DataAnnotations.Schema;
using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.SotaKebaharuan.Domain.SotaKebaharuan
{
    public sealed partial class SotaKebaharuan : Entity
    {
        private SotaKebaharuan()
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

        public static SotaKebaharuanBuilder Update(SotaKebaharuan prev) => new SotaKebaharuanBuilder(prev);

        public static Result<SotaKebaharuan> Create(
        string Nama,
        int Skor
        )
        {
            var asset = new SotaKebaharuan
            {
                Uuid = Guid.NewGuid(),
                Nama = Nama,
                Skor = Skor,
            };

            asset.Raise(new SotaKebaharuanCreatedDomainEvent(asset.Uuid));

            return asset;
        }
    }
}
