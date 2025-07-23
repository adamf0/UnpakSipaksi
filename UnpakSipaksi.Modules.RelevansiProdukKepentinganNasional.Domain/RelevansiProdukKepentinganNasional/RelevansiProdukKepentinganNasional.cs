using System.ComponentModel.DataAnnotations.Schema;
using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.RelevansiProdukKepentinganNasional.Domain.RelevansiProdukKepentinganNasional
{
    public sealed partial class RelevansiProdukKepentinganNasional : Entity
    {
        private RelevansiProdukKepentinganNasional()
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

        public static RelevansiProdukKepentinganNasionalBuilder Update(RelevansiProdukKepentinganNasional prev) => new RelevansiProdukKepentinganNasionalBuilder(prev);

        public static Result<RelevansiProdukKepentinganNasional> Create(
        string Nama,
        int Skor
        )
        {
            if (Skor < 0) {
                return Result.Failure<RelevansiProdukKepentinganNasional>(RelevansiProdukKepentinganNasionalErrors.InvalidValueSkor());
            }
            var asset = new RelevansiProdukKepentinganNasional
            {
                Uuid = Guid.NewGuid(),
                Nama = Nama,
                Skor = Skor,
            };

            asset.Raise(new RelevansiProdukKepentinganNasionalCreatedDomainEvent(asset.Uuid));

            return asset;
        }
    }
}
