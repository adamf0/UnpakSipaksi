using System.ComponentModel.DataAnnotations.Schema;
using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.KualitasKuantitasPublikasiProsiding.Domain.KualitasKuantitasPublikasiProsiding
{
    public sealed partial class KualitasKuantitasPublikasiProsiding : Entity
    {
        private KualitasKuantitasPublikasiProsiding()
        {
        }

        public int? Id { get; private set; } = null;

        [Column(TypeName = "VARCHAR(36)")]
        public Guid Uuid{ get; private set; }

        public string Nama { get; private set; } = null!;
        public int Nilai { get; private set; }

        public static KualitasKuantitasPublikasiProsidingBuilder Update(KualitasKuantitasPublikasiProsiding prev) => new KualitasKuantitasPublikasiProsidingBuilder(prev);

        public static Result<KualitasKuantitasPublikasiProsiding> Create(
        string Nama,
        int Nilai
        )
        {
            if (Nilai < 0) {
                return Result.Failure<KualitasKuantitasPublikasiProsiding>(KualitasKuantitasPublikasiProsidingErrors.InvalidValueNilai());
            }
            var asset = new KualitasKuantitasPublikasiProsiding
            {
                Uuid = Guid.NewGuid(),
                Nama = Nama,
                Nilai = Nilai
            };

            asset.Raise(new KualitasKuantitasPublikasiProsidingCreatedDomainEvent(asset.Uuid));

            return asset;
        }
    }
}
