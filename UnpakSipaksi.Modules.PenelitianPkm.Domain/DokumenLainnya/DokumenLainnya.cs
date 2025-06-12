using System.ComponentModel.DataAnnotations.Schema;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.PenelitianHibah.Domain.DokumenLainnya;

namespace UnpakSipaksi.Modules.PenelitianPkm.Domain.DokumenLainnya
{
    public sealed partial class DokumenLainnya : Entity
    {
        private DokumenLainnya()
        {
        }

        public int? Id { get; private set; } = null;

        [Column(TypeName = "VARCHAR(36)")]
        public Guid Uuid { get; private set; }

        [Column("id_pdp")]
        public int PenelitianPkmId { get; private set; }
        [Column("file_kontrak")]
        public string File { get; private set; }
        [Column("keterangan")]
        public string? Keterangan { get; private set; }

        public static Result<DokumenLainnya> Create(
          Guid UuidPenelitianPkm,
          Domain.PenelitianPkm.PenelitianPkm? existingPenelitianPkm,
          string File,
          string? Keterangan
        )
        {
            if (existingPenelitianPkm == null) {
                return Result.Failure<DokumenLainnya>((DokumenLainnyaErrors.NotFoundPkm(UuidPenelitianPkm)));
            }
            if (string.IsNullOrEmpty(File)) {
                return Result.Failure<DokumenLainnya>((DokumenLainnyaErrors.EmptyResource()));
            }

            var asset = new DokumenLainnya
            {
                Uuid = Guid.NewGuid(),
                PenelitianPkmId = existingPenelitianPkm?.Id ?? 0,
                File = File,
                Keterangan = Keterangan
            };

            asset.Raise(new DokumenLainnyaCreatedDomainEvent(asset.Uuid));

            return asset;
        }

        public static Result<DokumenLainnya> Update(
          Guid uuid,
           Guid UuidPenelitianPkm,
          Domain.PenelitianPkm.PenelitianPkm? existingPenelitianPkm,
          DokumenLainnya? prev,
          string File,
          string? Keterangan
        )
        {
            if (prev == null)
            {
                return Result.Failure<DokumenLainnya>((DokumenLainnyaErrors.NotFound(uuid)));
            }
            if (existingPenelitianPkm == null)
            {
                return Result.Failure<DokumenLainnya>((DokumenLainnyaErrors.NotFoundPkm(UuidPenelitianPkm)));
            }
            if (string.IsNullOrEmpty(File))
            {
                return Result.Failure<DokumenLainnya>((DokumenLainnyaErrors.EmptyResource()));
            }

            prev.File = File;
            prev.Keterangan = Keterangan;

            return prev;
        }
    }
}
