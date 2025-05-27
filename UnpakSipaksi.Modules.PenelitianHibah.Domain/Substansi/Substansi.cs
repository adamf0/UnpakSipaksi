using System.ComponentModel.DataAnnotations.Schema;
using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.PenelitianHibah.Domain.Substansi
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
        public int PenelitianHibahId { get; private set; }
        [Column("file")]
        public string File { get; private set; }


        public static Result<Substansi> Create(
          Guid UuidPenelitianHibah,
          Domain.PenelitianHibah.PenelitianHibah? existingPenelitianHibah,
          string? File
        )
        {
            if (existingPenelitianHibah == null) {
                return Result.Failure<Substansi>((SubstansiErrors.NotFoundHibah(UuidPenelitianHibah)));
            }
            if (string.IsNullOrEmpty(File)) {
                return Result.Failure<Substansi>((SubstansiErrors.EmptyFile()));
            }

            var asset = new Substansi
            {
                Uuid = Guid.NewGuid(),
                PenelitianHibahId = existingPenelitianHibah?.Id ?? 0, 
                File = File
            };

            asset.Raise(new SubstansiCreatedDomainEvent(asset.Uuid));

            return asset;
        }

        public static Result<Substansi> Update(
          Guid uuid,
          Guid UuidPenelitianHibah,
          Domain.PenelitianHibah.PenelitianHibah? existingPenelitianHibah,
          Substansi prev,
          string? File
        )
        {
            if (prev == null)
            {
                return Result.Failure<Substansi>((SubstansiErrors.NotFound(uuid)));
            }
            if (existingPenelitianHibah == null)
            {
                return Result.Failure<Substansi>((SubstansiErrors.NotFoundHibah(UuidPenelitianHibah)));
            }
            if (string.IsNullOrEmpty(File))
            {
                return Result.Failure<Substansi>((SubstansiErrors.EmptyFile()));
            }

            prev.File = File;

            return prev;
        }
    }
}
