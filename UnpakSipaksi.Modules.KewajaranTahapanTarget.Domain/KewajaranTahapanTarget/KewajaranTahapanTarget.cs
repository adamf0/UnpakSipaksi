using System.ComponentModel.DataAnnotations.Schema;
using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.KewajaranTahapanTarget.Domain.KewajaranTahapanTarget
{
    public sealed partial class KewajaranTahapanTarget : Entity
    {
        private KewajaranTahapanTarget()
        {
        }

        public int? Id { get; private set; } = null;

        [Column(TypeName = "VARCHAR(36)")]
        public Guid Uuid{ get; private set; }

        [Column("name")]
        public string Nama { get; private set; } = null!;
        public int Nilai { get; private set; } = 0;

        public static KewajaranTahapanTargetBuilder Update(KewajaranTahapanTarget prev) => new KewajaranTahapanTargetBuilder(prev);

        public static Result<KewajaranTahapanTarget> Create(
        string Nama,
        int Nilai
        )
        {
            var asset = new KewajaranTahapanTarget
            {
                Uuid = Guid.NewGuid(),
                Nama = Nama,
                Nilai = Nilai
            };

            asset.Raise(new KewajaranTahapanTargetCreatedDomainEvent(asset.Uuid));

            return asset;
        }
    }
}
