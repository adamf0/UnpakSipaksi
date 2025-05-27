using System.ComponentModel.DataAnnotations.Schema;
using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.PenelitianHibah.Domain.Luaran
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
        public int PenelitianHibahId { get; private set; }
        [Column("id_pdp_kategori")]
        public int? KategoriId { get; private set; }
        [Column("id_pdp_kategori_luaran")]
        public int? LuaranId { get; private set; }
        [Column("keterangan")]
        public string? Keterangan { get; private set; }
        [Column("link")]
        public string? Link { get; private set; }
        [Column("jenis")]
        public string Jenis { get; private set; }


        public static Result<Luaran> Create(
          Guid UuidPenelitianHibah,
          Domain.PenelitianHibah.PenelitianHibah? existingPenelitianHibah,
          int? KategoriId,
          int? LuaranId,
          string? Keterangan,
          string? Link,
          string Jenis
        )
        {
            if (existingPenelitianHibah == null) {
                return Result.Failure<Luaran>((LuaranErrors.NotFoundHibah(UuidPenelitianHibah)));
            }
            /*if (string.IsNullOrEmpty(Jenis)) {
                return Result.Failure<Luaran>((LuaranErrors.EmptyFile()));
            }*/

            var asset = new Luaran
            {
                Uuid = Guid.NewGuid(),
                PenelitianHibahId = existingPenelitianHibah?.Id ?? 0,
                KategoriId = KategoriId,
                LuaranId = LuaranId,
                Keterangan = Keterangan,
                Link = Link,
                Jenis = Jenis,
            };

            asset.Raise(new LuaranCreatedDomainEvent(asset.Uuid));

            return asset;
        }

        public static Result<Luaran> Update(
          Guid uuid,
          Guid UuidPenelitianHibah,
          Domain.PenelitianHibah.PenelitianHibah? existingPenelitianHibah,
          Luaran? prev,
          int? KategoriId,
          int? LuaranId,
          string? Keterangan,
          string? Link,
          string Jenis
        )
        {
            if (prev == null)
            {
                return Result.Failure<Luaran>((LuaranErrors.NotFound(uuid)));
            }
            if (existingPenelitianHibah == null)
            {
                return Result.Failure<Luaran>((LuaranErrors.NotFoundHibah(UuidPenelitianHibah)));
            }
            if (prev?.PenelitianHibahId != existingPenelitianHibah?.Id)
            {
                return Result.Failure<Luaran>(LuaranErrors.InvalidData());
            }

            prev.KategoriId = KategoriId;
            prev.LuaranId = LuaranId;
            prev.Keterangan = Keterangan;
            prev.Link = Link;
            prev.Jenis = Jenis;

            return prev;
        }
    }
}
