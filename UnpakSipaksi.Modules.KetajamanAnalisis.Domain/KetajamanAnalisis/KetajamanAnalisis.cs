using System.ComponentModel.DataAnnotations.Schema;
using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.KetajamanAnalisis.Domain.KetajamanAnalisis
{
    public sealed partial class KetajamanAnalisis : Entity
    {
        private KetajamanAnalisis()
        {
        }

        public int? Id { get; private set; } = null;

        [Column(TypeName = "VARCHAR(36)")]
        public Guid Uuid{ get; private set; }

        [Column("name")]
        public string Nama { get; private set; } = null!;
        public int Nilai { get; private set; } = 0;

        public static KetajamanAnalisisBuilder Update(KetajamanAnalisis prev) => new KetajamanAnalisisBuilder(prev);

        public static Result<KetajamanAnalisis> Create(
        string Nama,
        int Nilai
        )
        {
            if (Nilai < 0) {
                return Result.Failure<KetajamanAnalisis>(KetajamanAnalisisErrors.InvalidValueNilai());
            }
            var asset = new KetajamanAnalisis
            {
                Uuid = Guid.NewGuid(),
                Nama = Nama,
                Nilai = Nilai
            };

            asset.Raise(new KetajamanAnalisisCreatedDomainEvent(asset.Uuid));

            return asset;
        }
    }
}
