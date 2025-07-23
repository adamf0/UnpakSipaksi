using System.ComponentModel.DataAnnotations.Schema;
using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.Komponen.Domain.Komponen
{
    public sealed partial class Komponen : Entity
    {
        private Komponen()
        {
        }

        public int? Id { get; set; } = null;

        [Column(TypeName = "VARCHAR(36)")]
        public Guid Uuid{ get; set; }

        [Column("name")]
        public string Nama { get; set; } = null!;
        public int? MaxBiaya { get; set; } = null;

        public static KomponenBuilder Update(Komponen prev) => new KomponenBuilder(prev);

        public static Result<Komponen> Create(
        string Nama,
        int? MaxBiaya
        )
        {
            var asset = new Komponen
            {
                Uuid = Guid.NewGuid(),
                Nama = Nama,
                MaxBiaya = MaxBiaya
            };

            if (MaxBiaya!=null && MaxBiaya<=0) {
                return Result.Failure<Komponen>(KomponenErrors.InvalidMaxBiaya());
            }

            asset.Raise(new KomponenCreatedDomainEvent(asset.Uuid));

            return asset;
        }
    }
}
