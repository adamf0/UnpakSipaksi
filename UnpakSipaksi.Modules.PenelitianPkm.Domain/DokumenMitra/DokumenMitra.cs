using System.ComponentModel.DataAnnotations.Schema;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.PenelitianHibah.Domain.DokumenMitra;

namespace UnpakSipaksi.Modules.PenelitianPkm.Domain.DokumenMitra
{
    public sealed partial class DokumenMitra : Entity
    {
        private DokumenMitra()
        {
        }

        public int? Id { get; private set; } = null;

        [Column(TypeName = "VARCHAR(36)")]
        public Guid Uuid { get; private set; }

        [Column("id_pdp")]
        public int PenelitianPkmId { get; private set; }
        [Column("mitra")]
        public string Mitra { get; set; }
        [Column("provinsi")]
        public string Provinsi { get; set; }
        [Column("kota")]
        public string Kota { get; set; }
        [Column("kelompokMitra")]
        public int KelompokMitraId { get; set; }
        [Column("pemimpinMitra")]
        public string PemimpinMitra { get; set; }
        [Column("kontakMitra")]
        public string? KontakMitra { get; set; }
        [Column("suratPernyataan")]
        public string File { get; set; }

        public static Result<DokumenMitra> Create(
          Guid UuidPenelitianPkm,
          Domain.PenelitianPkm.PenelitianPkm? existingPenelitianPkm,
          string Mitra,
          string Provinsi,
          string Kota,
          int KelompokMitraId,
          string PemimpinMitra,
          string? KontakMitra,
          string File
        )
        {
            if (existingPenelitianPkm == null) {
                return Result.Failure<DokumenMitra>((DokumenMitraErrors.NotFoundPkm(UuidPenelitianPkm)));
            }
            if (string.IsNullOrEmpty(File)) {
                return Result.Failure<DokumenMitra>((DokumenMitraErrors.EmptyResource()));
            }
            if (KelompokMitraId <= 0) {
                return Result.Failure<DokumenMitra>((DokumenMitraErrors.InvalidKelompokMitra()));
            }
            //[PR] penambahan validasi provinsi & kota sesuai db

            var asset = new DokumenMitra
            {
                Uuid = Guid.NewGuid(),
                PenelitianPkmId = existingPenelitianPkm?.Id ?? 0,
                Mitra = Mitra,
                Provinsi = Provinsi,
                Kota = Kota,
                KelompokMitraId = KelompokMitraId,
                PemimpinMitra = PemimpinMitra,
                KontakMitra = KontakMitra,
                File = File
            };

            asset.Raise(new DokumenMitraCreatedDomainEvent(asset.Uuid));

            return asset;
        }

        public static Result<DokumenMitra> Update(
          Guid uuid,
          Guid UuidPenelitianPkm,
          Domain.PenelitianPkm.PenelitianPkm? existingPenelitianPkm,
          DokumenMitra? prev,
          string Mitra,
          string Provinsi,
          string Kota,
          int KelompokMitraId,
          string PemimpinMitra,
          string? KontakMitra,
          string File
        )
        {
            if (prev == null)
            {
                return Result.Failure<DokumenMitra>((DokumenMitraErrors.NotFound(uuid)));
            }
            if (existingPenelitianPkm == null)
            {
                return Result.Failure<DokumenMitra>((DokumenMitraErrors.NotFoundPkm(UuidPenelitianPkm)));
            }
            if (string.IsNullOrEmpty(File))
            {
                return Result.Failure<DokumenMitra>((DokumenMitraErrors.EmptyResource()));
            }
            if (KelompokMitraId <= 0)
            {
                return Result.Failure<DokumenMitra>((DokumenMitraErrors.InvalidKelompokMitra()));
            }
            //[PR] penambahan validasi provinsi & kota sesuai db

            prev.Mitra = Mitra;
            prev.Provinsi = Provinsi;
            prev.Kota = Kota;
            prev.KelompokMitraId = KelompokMitraId;
            prev.PemimpinMitra = PemimpinMitra;
            prev.KontakMitra = KontakMitra;
            prev.File = File;

            return prev;
        }
    }
}
