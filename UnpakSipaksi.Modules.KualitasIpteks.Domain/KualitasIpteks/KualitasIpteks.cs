using System.ComponentModel.DataAnnotations.Schema;
using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.KualitasIpteks.Domain.KualitasIpteks
{
    public sealed partial class KualitasIpteks : Entity
    {
        private KualitasIpteks()
        {
        }

        public int? Id { get; private set; } = null;

        [Column(TypeName = "VARCHAR(36)")]
        public Guid Uuid{ get; private set; }

        public string Nama { get; private set; } = null!;
        public int Nilai { get; private set; }

        public static KualitasIpteksBuilder Update(KualitasIpteks prev) => new KualitasIpteksBuilder(prev);

        public static Result<KualitasIpteks> Create(
        string Nama,
        int Nilai
        )
        {
            if (Nilai < 0)
            {
                return Result.Failure<KualitasIpteks>(KualitasIpteksErrors.InvalidValueNilai());
            }
            var asset = new KualitasIpteks
            {
                Uuid = Guid.NewGuid(),
                Nama = Nama,
                Nilai = Nilai
            };

            asset.Raise(new KualitasIpteksCreatedDomainEvent(asset.Uuid));

            return asset;
        }
    }
}
