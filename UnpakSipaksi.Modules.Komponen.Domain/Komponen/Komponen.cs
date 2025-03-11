using System.ComponentModel.DataAnnotations.Schema;
using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.Komponen.Domain.Komponen
{
    public sealed partial class Komponen : Entity
    {
        private Komponen()
        {
        }

        public int? Id { get; private set; } = null;

        [Column(TypeName = "VARCHAR(36)")]
        public Guid Uuid{ get; private set; }

        [Column("name")]
        public string Nama { get; private set; } = null!;
        public int MaxBiaya { get; private set; } = 0;

        public static KomponenBuilder Update(Komponen prev) => new KomponenBuilder(prev);

        public static Result<Komponen> Create(
        string Nama,
        int MaxBiaya
        )
        {
            var asset = new Komponen
            {
                Uuid = Guid.NewGuid(),
                Nama = Nama,
                MaxBiaya = MaxBiaya
            };

            asset.Raise(new KomponenCreatedDomainEvent(asset.Uuid));

            return asset;
        }
    }
}
