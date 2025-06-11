using System.ComponentModel.DataAnnotations.Schema;
using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.PenelitianPkm.Domain.Luaran
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
        public int PenelitianPkmId { get; private set; }
        [Column("id_jenis_luaran")]
        public int? JenisLuaranId { get; private set; }
        [Column("id_indikator_capaian")]
        public int? IndikatorCapaianId { get; private set; }
        [Column("keterangan")]
        public string? Keterangan { get; private set; }
        [Column("link")]
        public string? Link { get; private set; }
        [Column("jenis")]
        public string Jenis { get; private set; }


        public static Result<Luaran> Create(
          Guid UuidPenelitianPkm,
          PenelitianPkm.PenelitianPkm? existingPenelitianPkm,
          int? JenisLuaranId,
          int? IndikatorCapaianId,
          string? Keterangan,
          string? Link,
          string Jenis
        )
        {
            if (existingPenelitianPkm == null)
            {
                return Result.Failure<Luaran>(LuaranErrors.NotFoundHibah(UuidPenelitianPkm));
            }
            /*if (JenisLuaranId == null || JenisLuaranId==0)
            {
                return Result.Failure<Luaran>(LuaranErrors.UnknownJenisLuaran());
            }
            if (IndikatorCapaianId == null || IndikatorCapaianId == 0)
            {
                return Result.Failure<Luaran>(LuaranErrors.UnknownIndikatorCapaian());
            }*/

            var asset = new Luaran
            {
                Uuid = Guid.NewGuid(),
                PenelitianPkmId = existingPenelitianPkm?.Id ?? 0,
                JenisLuaranId = JenisLuaranId,
                IndikatorCapaianId = IndikatorCapaianId,
                Keterangan = Keterangan,
                Link = Link,
                Jenis = Jenis,
            };

            asset.Raise(new LuaranCreatedDomainEvent(asset.Uuid));

            return asset;
        }

        public static Result<Luaran> Update(
          Guid uuid,
          Guid UuidPenelitianPkm,
          PenelitianPkm.PenelitianPkm? existingPenelitianPkm,
          Luaran? prev,
          int? JenisLuaranId,
          int? IndikatorCapaianId,
          string? Keterangan,
          string? Link,
          string Jenis
        )
        {
            if (prev == null)
            {
                return Result.Failure<Luaran>(LuaranErrors.NotFound(uuid));
            }
            if (existingPenelitianPkm == null)
            {
                return Result.Failure<Luaran>(LuaranErrors.NotFoundHibah(UuidPenelitianPkm));
            }
            if (prev?.PenelitianPkmId != existingPenelitianPkm?.Id)
            {
                return Result.Failure<Luaran>(LuaranErrors.InvalidData());
            }
            /*if (JenisLuaranId == null || JenisLuaranId == 0)
            {
                return Result.Failure<Luaran>(LuaranErrors.UnknownJenisLuaran());
            }
            if (IndikatorCapaianId == null || IndikatorCapaianId == 0)
            {
                return Result.Failure<Luaran>(LuaranErrors.UnknownIndikatorCapaian());
            }*/

            prev.JenisLuaranId = JenisLuaranId;
            prev.IndikatorCapaianId = IndikatorCapaianId;
            prev.Keterangan = Keterangan;
            prev.Link = Link;
            prev.Jenis = Jenis;

            return prev;
        }
    }
}
