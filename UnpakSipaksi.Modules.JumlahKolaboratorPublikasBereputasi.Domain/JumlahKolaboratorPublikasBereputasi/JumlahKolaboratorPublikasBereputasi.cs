using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.JumlahKolaboratorPublikasBereputasi.Domain.JumlahKolaboratorPublikasBereputasi
{
    public sealed partial class JumlahKolaboratorPublikasBereputasi : Entity
    {
        private JumlahKolaboratorPublikasBereputasi()
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

        public static JumlahKolaboratorPublikasBereputasiBuilder Update(JumlahKolaboratorPublikasBereputasi prev) => new JumlahKolaboratorPublikasBereputasiBuilder(prev);

        public static Result<JumlahKolaboratorPublikasBereputasi> Create(
        string Nama,
        int BobotPDP,
        int BobotTerapan,
        int BobotKerjasama,
        int BobotPenelitianDasar,
        int Skor
        )
        {
            var asset = new JumlahKolaboratorPublikasBereputasi
            {
                Uuid = Guid.NewGuid(),
                Nama = Nama,
                BobotPDP = BobotPDP,
                BobotTerapan = BobotTerapan,
                BobotKerjasama = BobotKerjasama,
                BobotPenelitianDasar = BobotPenelitianDasar,
                Skor = Skor,
            };

            asset.Raise(new JumlahKolaboratorPublikasBereputasiCreatedDomainEvent(asset.Uuid));

            return asset;
        }
    }
}
