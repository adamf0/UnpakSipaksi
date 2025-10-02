using System.ComponentModel.DataAnnotations.Schema;
using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.LaporanKemajuan.Domain.Luaran
{
    public sealed partial class Luaran : Entity
    {
        private Luaran()
        {
        }

        public int? Id { get; private set; } = null;

        [Column(TypeName = "VARCHAR(36)")]
        public Guid Uuid { get; private set; }

        
        [Column("id_pdp")]
        public int? PenenitianHibahId { get; private set; } = null;
        [Column("id_pdp_kategori")]
        public int? KategoriId { get; private set; } = null;
        [Column("id_pdp_kategori_luaran")]
        public int? LuaranId { get; private set; } = null;


        [Column("id_pkm")]
        public int? PenenitianPkmId { get; private set; } = null;
        [Column("id_jenis_luaran")]
        public int? JenisLuaranId { get; private set; } = null;
        [Column("id_indikator_capaian")]
        public int? IndikatorId { get; private set; } = null;

        public string Status { get; private set; }
        public string? Link { get; private set; } = null;
        public string Jenis { get; private set; }
        public string Type { get; private set; }

        public static Result<Luaran> Create( //[Note] laporan_kemajuan_internal
            int? PenenitianHibahId,
            int? KategoriId,
            int? LuaranId,
            string Status,
            string? Link,
            string Jenis
        )
        {
            string[] allowJenis = ["Tambahan", "Wajib"];

            if (allowJenis.Contains(Jenis)) {
                return Result.Failure<Luaran>(LuaranErrors.InvalidJenis());
            }

            var asset = new Luaran
            {
                Uuid = Guid.NewGuid(),
                PenenitianHibahId = PenenitianHibahId,
                KategoriId = KategoriId,
                LuaranId = LuaranId,
                Status = Status,
                Link = Link,
                Jenis = Jenis, 
                Type = "Hibah"
            };

            asset.Raise(new LuaranCreatedDomainEvent(asset.Uuid));

            return asset;
        }

        public static Result<Luaran> CreatePkm( //[Note] laporan_kemajuan_pkm
            int? PenenitianPkmId,
            int? JenisLuaranId,
            int? IndikatorId,
            string Status,
            string? Link,
            string Jenis
        )
        {
            string[] allowJenis = ["Tambahan", "Wajib"];

            if (allowJenis.Contains(Jenis))
            {
                return Result.Failure<Luaran>(LuaranErrors.InvalidJenis());
            }

            var asset = new Luaran
            {
                Uuid = Guid.NewGuid(),
                PenenitianPkmId = PenenitianPkmId,
                JenisLuaranId = JenisLuaranId,
                IndikatorId = IndikatorId,
                Status = Status,
                Link = Link,
                Jenis = Jenis,
                Type = "Hibah"
            };

            asset.Raise(new LuaranCreatedDomainEvent(asset.Uuid));

            return asset;
        }

        public static Result<Luaran> Update(
            Domain.Luaran.Luaran? prev,
            int? PenenitianHibahId,
            int? KategoriId,
            int? LuaranId,
            string Status,
            string? Link,
            string Jenis
        )
        {
            string[] allowJenis = ["Tambahan", "Wajib"];

            if (allowJenis.Contains(Jenis))
            {
                return Result.Failure<Luaran>(LuaranErrors.InvalidJenis());
            }
            if (prev==null) {
                return Result.Failure<Luaran>(LuaranErrors.EmptyData());
            }

            var asset = new Luaran
            {
                Uuid = Guid.NewGuid(),
                PenenitianHibahId = PenenitianHibahId,
                KategoriId = KategoriId,
                LuaranId = LuaranId,
                Status = Status,
                Link = Link,
                Jenis = Jenis
            };

            asset.Raise(new LuaranCreatedDomainEvent(asset.Uuid));

            return asset;
        }

        public static Result<Luaran> UpdatePkm(
            Domain.Luaran.Luaran? prev,
            int? PenenitianPkmId,
            int? JenisLuaranId,
            int? IndikatorId,
            string Status,
            string? Link,
            string Jenis
        )
        {
            string[] allowJenis = ["Tambahan", "Wajib"];

            if (allowJenis.Contains(Jenis))
            {
                return Result.Failure<Luaran>(LuaranErrors.InvalidJenis());
            }
            if (prev == null)
            {
                return Result.Failure<Luaran>(LuaranErrors.EmptyData());
            }

            var asset = new Luaran
            {
                Uuid = Guid.NewGuid(),
                PenenitianPkmId = PenenitianPkmId,
                JenisLuaranId = JenisLuaranId,
                IndikatorId = IndikatorId,
                Status = Status,
                Link = Link,
                Jenis = Jenis,
                Type = "PKM"
            };

            asset.Raise(new LuaranCreatedDomainEvent(asset.Uuid));

            return asset;
        }
    }
}
