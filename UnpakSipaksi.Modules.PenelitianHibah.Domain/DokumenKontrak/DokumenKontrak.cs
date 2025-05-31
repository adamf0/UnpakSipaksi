using System.ComponentModel.DataAnnotations.Schema;
using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.PenelitianHibah.Domain.DokumenKontrak
{
    public sealed partial class DokumenKontrak : Entity
    {
        private DokumenKontrak()
        {
        }

        public int? Id { get; private set; } = null;

        [Column(TypeName = "VARCHAR(36)")]
        public Guid Uuid { get; private set; }

        [Column("id_pdp")]
        public int PenelitianHibahId { get; private set; }
        [Column("file_kontrak")]
        public string? File { get; private set; }

        public static Result<DokumenKontrak> Create(
          Guid UuidPenelitianHibah,
          Domain.PenelitianHibah.PenelitianHibah? existingPenelitianHibah,
          string? File
        )
        {
            if (existingPenelitianHibah == null) {
                return Result.Failure<DokumenKontrak>((DokumenKontrakErrors.NotFoundHibah(UuidPenelitianHibah)));
            }
            if (string.IsNullOrEmpty(File)) {
                return Result.Failure<DokumenKontrak>((DokumenKontrakErrors.EmptyResource()));
            }

            var asset = new DokumenKontrak
            {
                Uuid = Guid.NewGuid(),
                PenelitianHibahId = existingPenelitianHibah?.Id ?? 0,
                File = File,
            };

            asset.Raise(new DokumenKontrakCreatedDomainEvent(asset.Uuid));

            return asset;
        }

        public static Result<DokumenKontrak> Update(
          Guid uuid,
          Guid UuidPenelitianHibah,
          Domain.PenelitianHibah.PenelitianHibah? existingPenelitianHibah,
          DokumenKontrak? prev,
          string? File
        )
        {
            if (prev == null)
            {
                return Result.Failure<DokumenKontrak>((DokumenKontrakErrors.NotFound(uuid)));
            }
            if (existingPenelitianHibah == null)
            {
                return Result.Failure<DokumenKontrak>((DokumenKontrakErrors.NotFoundHibah(UuidPenelitianHibah)));
            }
            if (prev?.PenelitianHibahId != existingPenelitianHibah?.Id)
            {
                return Result.Failure<DokumenKontrak>(DokumenKontrakErrors.InvalidData());
            }
            if (string.IsNullOrEmpty(File))
            {
                return Result.Failure<DokumenKontrak>((DokumenKontrakErrors.EmptyResource()));
            }

            prev.File = File;

            return prev;
        }
    }
}
