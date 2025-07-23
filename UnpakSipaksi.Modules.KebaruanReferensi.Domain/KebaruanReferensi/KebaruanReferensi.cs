using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.KebaruanReferensi.Domain.KebaruanReferensi
{
    public sealed partial class KebaruanReferensi : Entity
    {
        private KebaruanReferensi()
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
        public int Skor { get; private set; }

        public static KebaruanReferensiBuilder Update(KebaruanReferensi prev) => new KebaruanReferensiBuilder(prev);

        public static Result<KebaruanReferensi> Create(
        string Nama,
        int Skor
        )
        {
            if (Skor <0) {
                return Result.Failure<KebaruanReferensi>(KebaruanReferensiErrors.InvalidValueSkor());
            }

            var asset = new KebaruanReferensi
            {
                Uuid = Guid.NewGuid(),
                Nama = Nama,
                Skor = Skor,
            };

            asset.Raise(new KebaruanReferensiCreatedDomainEvent(asset.Uuid));

            return asset;
        }
    }
}
