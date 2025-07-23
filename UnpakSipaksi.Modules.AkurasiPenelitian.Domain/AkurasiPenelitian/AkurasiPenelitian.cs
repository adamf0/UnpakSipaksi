using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.AkurasiPenelitian.Domain.AkurasiPenelitian
{
    public sealed partial class AkurasiPenelitian : Entity
    {
        private AkurasiPenelitian()
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

        public static AkurasiPenelitianBuilder Update(AkurasiPenelitian prev) => new AkurasiPenelitianBuilder(prev);

        public static Result<AkurasiPenelitian> Create(
        string Nama,
        int Skor
        )
        {
            var asset = new AkurasiPenelitian
            {
                Uuid = Guid.NewGuid(),
                Nama = Nama,
                Skor = Skor,
            };

            if (Skor < 0 || Skor > int.MaxValue) {
                return Result.Failure<AkurasiPenelitian>(AkurasiPenelitianErrors.InvalidSkor());
            }

            asset.Raise(new AkurasiPenelitianCreatedDomainEvent(asset.Uuid));

            return asset;
        }
    }
}
