using System.ComponentModel.DataAnnotations.Schema;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.LaporanKemajuan.Domain.Luaran;

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
        [Column("id_pdp")]
        public int? PenenitianHibahId { get; private set; } = null;
        [Column("id_pkm")]
        public int? PenenitianPkmId { get; private set; } = null;
        [Column("id_jenis_luaran")]
        public string File { get; private set; } = null!;
        public string Type { get; private set; } = null!;

        public static Result<Dokumen> Create( //persentasi_penelitian, dokumen_internal, dokumen_nasional, dokumen_pkm, dokumen_pkm_nasional
        int? PenenitianHibahId,
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
                Type = Type,
                PenenitianHibahId = PenenitianHibahId
            };

            asset.Raise(new LuaranCreatedDomainEvent(asset.Uuid));

            return asset;
        }

        public static Result<Dokumen> CreatePkm( //persentasi_penelitian, dokumen_internal, dokumen_nasional, dokumen_pkm, dokumen_pkm_nasional
        int? PenenitianPkmId,
        string File,
        string Type
        )
        {
            string[] allowType = ["Presentasi", "Dokumen"];

            if (allowType.Contains(Type))
            {
                return Result.Failure<Dokumen>(DokumenErrors.InvalidType());
            }
            var asset = new Dokumen
            {
                Uuid = Guid.NewGuid(),
                File = File,
                Type = Type,
                PenenitianPkmId = PenenitianPkmId
            };

            asset.Raise(new LuaranCreatedDomainEvent(asset.Uuid));

            return asset;
        }

        public static Result<Dokumen> Update(
        Domain.Dokumen.Dokumen? prev,
        int? PenenitianHibahId,
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
            if (prev?.PenenitianHibahId != PenenitianHibahId) {
                return Result.Failure<Dokumen>(DokumenErrors.InvalidData());
            }

            var asset = new Dokumen
            {
                Uuid = Guid.NewGuid(),
                File = File,
                Type = Type,
                PenenitianHibahId = PenenitianHibahId
            };

            asset.Raise(new LuaranCreatedDomainEvent(asset.Uuid));

            return asset;
        }

        public static Result<Dokumen> UpdatePkm(
        Domain.Dokumen.Dokumen? prev,
        int? PenenitianPkmId,
        string File,
        string Type
        )
        {
            string[] allowType = ["Presentasi", "Dokumen"];

            if (allowType.Contains(Type))
            {
                return Result.Failure<Dokumen>(DokumenErrors.InvalidType());
            }
            if (prev == null)
            {
                return Result.Failure<Dokumen>(DokumenErrors.EmptyData());
            }
            if (prev?.PenenitianPkmId != PenenitianPkmId)
            {
                return Result.Failure<Dokumen>(DokumenErrors.InvalidData());
            }

            var asset = new Dokumen
            {
                Uuid = Guid.NewGuid(),
                File = File,
                Type = Type,
                PenenitianPkmId = PenenitianPkmId
            };

            asset.Raise(new LuaranCreatedDomainEvent(asset.Uuid));

            return asset;
        }
    }
}
