using System.ComponentModel.DataAnnotations.Schema;
using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.KuantitasStatusKi.Domain.KuantitasStatusKi
{
    public sealed partial class KuantitasStatusKi : Entity
    {
        private KuantitasStatusKi()
        {
        }

        public int? Id { get; private set; } = null;

        [Column(TypeName = "VARCHAR(36)")]
        public Guid Uuid{ get; private set; }

        public string Nama { get; private set; } = null!;
        public int Nilai { get; private set; }

        public static KuantitasStatusKiBuilder Update(KuantitasStatusKi prev) => new KuantitasStatusKiBuilder(prev);

        public static Result<KuantitasStatusKi> Create(
        string Nama,
        int Nilai
        )
        {
            var asset = new KuantitasStatusKi
            {
                Uuid = Guid.NewGuid(),
                Nama = Nama,
                Nilai = Nilai
            };

            asset.Raise(new KuantitasStatusKiCreatedDomainEvent(asset.Uuid));

            return asset;
        }
    }
}
