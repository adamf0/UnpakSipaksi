using System.ComponentModel.DataAnnotations.Schema;
using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.KualitasKuantitasPublikasiJurnalIlmiah.Domain.KualitasKuantitasPublikasiJurnalIlmiah
{
    public sealed partial class KualitasKuantitasPublikasiJurnalIlmiah : Entity
    {
        private KualitasKuantitasPublikasiJurnalIlmiah()
        {
        }

        public int? Id { get; private set; } = null;

        [Column(TypeName = "VARCHAR(36)")]
        public Guid Uuid{ get; private set; }

        public string Nama { get; private set; } = null!;
        public int Nilai { get; private set; }

        public static KualitasKuantitasPublikasiJurnalIlmiahBuilder Update(KualitasKuantitasPublikasiJurnalIlmiah prev) => new KualitasKuantitasPublikasiJurnalIlmiahBuilder(prev);

        public static Result<KualitasKuantitasPublikasiJurnalIlmiah> Create(
        string Nama,
        int Nilai
        )
        {
            var asset = new KualitasKuantitasPublikasiJurnalIlmiah
            {
                Uuid = Guid.NewGuid(),
                Nama = Nama,
                Nilai = Nilai
            };

            asset.Raise(new KualitasKuantitasPublikasiJurnalIlmiahDomainEvent(asset.Uuid));

            return asset;
        }
    }
}
