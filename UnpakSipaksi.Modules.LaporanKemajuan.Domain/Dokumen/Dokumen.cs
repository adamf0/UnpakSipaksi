using System.ComponentModel.DataAnnotations.Schema;
using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.LaporanKemajuan.Domain.Dokumen
{
    public sealed partial class Dokumen : Entity
    {
        private Dokumen()
        {
        }

        public int? Id { get; private set; } = null;

        [Column(TypeName = "VARCHAR(36)")]
        public Guid Uuid{ get; private set; }

        [Column("name")]
        public string Nama { get; private set; } = null!;
        public string File { get; private set; } = null!;
        public string Type { get; private set; } = null!;

        public static Result<Dokumen> Create(
        string File,
        string Type
        )
        {
            string[] allowType = ["Presentasi", "Dokumen"];

            if (allowType.Contains(Type)) {
                return Result.Failure<Dokumen>(DokumenErrors.InvalidType());
            }
            var asset = new Dokumen
            {
                Uuid = Guid.NewGuid(),
                File = File,
                Type = Type
            };

            asset.Raise(new KetajamanAnalisisCreatedDomainEvent(asset.Uuid));

            return asset;
        }

        public static Result<Dokumen> Update(
        Domain.Dokumen.Dokumen? prev,
        string File,
        string Type
        )
        {
            string[] allowType = ["Presentasi", "Dokumen"];

            if (allowType.Contains(Type))
            {
                return Result.Failure<Dokumen>(DokumenErrors.InvalidType());
            }
            if (prev==null) {
                return Result.Failure<Dokumen>(DokumenErrors.EmptyData());
            }

            var asset = new Dokumen
            {
                Uuid = Guid.NewGuid(),
                File = File,
                Type = Type
            };

            asset.Raise(new KetajamanAnalisisCreatedDomainEvent(asset.Uuid));

            return asset;
        }
    }
}
