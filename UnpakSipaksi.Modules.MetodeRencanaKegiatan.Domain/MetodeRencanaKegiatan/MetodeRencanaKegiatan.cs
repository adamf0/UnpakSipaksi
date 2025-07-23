using System.ComponentModel.DataAnnotations.Schema;
using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.MetodeRencanaKegiatan.Domain.MetodeRencanaKegiatan
{
    public sealed partial class MetodeRencanaKegiatan : Entity
    {
        private MetodeRencanaKegiatan()
        {
        }

        public int? Id { get; private set; } = null;

        [Column(TypeName = "VARCHAR(36)")]
        public Guid Uuid{ get; private set; }

        public string Nama { get; private set; } = null!;
        public int Nilai { get; private set; }

        public static MetodeRencanaKegiatanBuilder Update(MetodeRencanaKegiatan prev) => new MetodeRencanaKegiatanBuilder(prev);

        public static Result<MetodeRencanaKegiatan> Create(
        string Nama,
        int Nilai
        )
        {
            if (Nilai < 0) {
                return Result.Failure<MetodeRencanaKegiatan>(MetodeRencanaKegiatanErrors.InvalidValueNilai());
            }
            var asset = new MetodeRencanaKegiatan
            {
                Uuid = Guid.NewGuid(),
                Nama = Nama,
                Nilai = Nilai
            };

            asset.Raise(new MetodeRencanaKegiatanCreatedDomainEvent(asset.Uuid));

            return asset;
        }
    }
}
