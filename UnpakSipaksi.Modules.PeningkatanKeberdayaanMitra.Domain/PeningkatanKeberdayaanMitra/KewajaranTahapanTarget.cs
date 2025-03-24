using System.ComponentModel.DataAnnotations.Schema;
using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.PeningkatanKeberdayaanMitra.Domain.PeningkatanKeberdayaanMitra
{
    public sealed partial class PeningkatanKeberdayaanMitra : Entity
    {
        private PeningkatanKeberdayaanMitra()
        {
        }

        public int? Id { get; private set; } = null;

        [Column(TypeName = "VARCHAR(36)")]
        public Guid Uuid{ get; private set; }

        [Column("name")]
        public string Nama { get; private set; } = null!;
        public int Nilai { get; private set; } = 0;

        public static PeningkatanKeberdayaanMitraBuilder Update(PeningkatanKeberdayaanMitra prev) => new PeningkatanKeberdayaanMitraBuilder(prev);

        public static Result<PeningkatanKeberdayaanMitra> Create(
        string Nama,
        int Nilai
        )
        {
            var asset = new PeningkatanKeberdayaanMitra
            {
                Uuid = Guid.NewGuid(),
                Nama = Nama,
                Nilai = Nilai
            };

            asset.Raise(new PeningkatanKeberdayaanMitraCreatedDomainEvent(asset.Uuid));

            return asset;
        }
    }
}
