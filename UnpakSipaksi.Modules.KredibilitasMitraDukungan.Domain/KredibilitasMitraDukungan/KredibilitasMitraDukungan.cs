using System.ComponentModel.DataAnnotations.Schema;
using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.KredibilitasMitraDukungan.Domain.KredibilitasMitraDukungan
{
    public sealed partial class KredibilitasMitraDukungan : Entity
    {
        private KredibilitasMitraDukungan()
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

        public static KredibilitasMitraDukunganBuilder Update(KredibilitasMitraDukungan prev) => new KredibilitasMitraDukunganBuilder(prev);

        public static Result<KredibilitasMitraDukungan> Create(
        string Nama,
        int Skor
        )
        {
            var asset = new KredibilitasMitraDukungan
            {
                Uuid = Guid.NewGuid(),
                Nama = Nama,
                Skor = Skor,
            };

            asset.Raise(new KredibilitasMitraDukunganCreatedDomainEvent(asset.Uuid));

            return asset;
        }
    }
}
