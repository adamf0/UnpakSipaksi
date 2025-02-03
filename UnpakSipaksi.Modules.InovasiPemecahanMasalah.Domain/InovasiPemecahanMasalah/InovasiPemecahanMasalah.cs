using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.InovasiPemecahanMasalah.Domain.InovasiPemecahanMasalah
{
    public sealed partial class InovasiPemecahanMasalah : Entity
    {
        private InovasiPemecahanMasalah()
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

        public static InovasiPemecahanMasalahBuilder Update(InovasiPemecahanMasalah prev) => new InovasiPemecahanMasalahBuilder(prev);

        public static Result<InovasiPemecahanMasalah> Create(
        string Nama,
        int BobotPDP,
        int BobotTerapan,
        int BobotKerjasama,
        int BobotPenelitianDasar,
        int Skor
        )
        {
            var asset = new InovasiPemecahanMasalah
            {
                Uuid = Guid.NewGuid(),
                Nama = Nama,
                BobotPDP = BobotPDP,
                BobotTerapan = BobotTerapan,
                BobotKerjasama = BobotKerjasama,
                BobotPenelitianDasar = BobotPenelitianDasar,
                Skor = Skor,
            };

            asset.Raise(new InovasiPemecahanMasalahCreatedDomainEvent(asset.Uuid));

            return asset;
        }
    }
}
