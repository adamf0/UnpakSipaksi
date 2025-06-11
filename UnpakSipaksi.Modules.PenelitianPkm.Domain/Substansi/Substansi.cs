using System.ComponentModel.DataAnnotations.Schema;
using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.PenelitianPkm.Domain.Substansi
{
    public sealed partial class Substansi : Entity
    {
        private Substansi()
        {
        }

        public int? Id { get; private set; } = null;

        [Column(TypeName = "VARCHAR(36)")]
        public Guid Uuid { get; private set; }

        [Column("id_pdp")]
        public int PenelitianPkmId { get; private set; }
        [Column("file")]
        public string File { get; private set; }


        public static Result<Substansi> Create(
          Guid UuidPenelitianPkm,
          PenelitianPkm.PenelitianPkm? existingPenelitianPkm,
          string? File
        )
        {
            if (existingPenelitianPkm == null)
            {
                return Result.Failure<Substansi>(SubstansiErrors.NotFoundHibah(UuidPenelitianPkm));
            }
            if (string.IsNullOrEmpty(File))
            {
                return Result.Failure<Substansi>(SubstansiErrors.EmptyFile());
            }

            var asset = new Substansi
            {
                Uuid = Guid.NewGuid(),
                PenelitianPkmId = existingPenelitianPkm?.Id ?? 0,
                File = File
            };

            asset.Raise(new SubstansiCreatedDomainEvent(asset.Uuid));

            return asset;
        }

        public static Result<Substansi> Update(
          Guid uuid,
          Guid UuidPenelitianPkm,
          PenelitianPkm.PenelitianPkm? existingPenelitianPkm,
          Substansi prev,
          string? File
        )
        {
            if (prev == null)
            {
                return Result.Failure<Substansi>(SubstansiErrors.NotFound(uuid));
            }
            if (existingPenelitianPkm == null)
            {
                return Result.Failure<Substansi>(SubstansiErrors.NotFoundHibah(UuidPenelitianPkm));
            }
            if (string.IsNullOrEmpty(File))
            {
                return Result.Failure<Substansi>(SubstansiErrors.EmptyFile());
            }

            prev.File = File;

            return prev;
        }
    }
}
