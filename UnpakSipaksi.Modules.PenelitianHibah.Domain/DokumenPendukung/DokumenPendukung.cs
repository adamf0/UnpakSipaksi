using System.ComponentModel.DataAnnotations.Schema;
using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.PenelitianHibah.Domain.DokumenPendukung
{
    public sealed partial class DokumenPendukung : Entity
    {
        private DokumenPendukung()
        {
        }

        public int? Id { get; private set; } = null;

        [Column(TypeName = "VARCHAR(36)")]
        public Guid Uuid { get; private set; }

        [Column("id_pdp")]
        public int PenelitianHibahId { get; private set; }
        [Column("file_mitra")]
        public string? File { get; private set; }
        [Column("link")]
        public string? Link { get; private set; }
        [Column("kategori")]
        public string Kategori { get; private set; }

        public static Result<DokumenPendukung> Create(
          Guid UuidPenelitianHibah,
          Domain.PenelitianHibah.PenelitianHibah? existingPenelitianHibah,
          string? File,
          string? Link,
          string Kategori
        )
        {
            if (existingPenelitianHibah == null) {
                return Result.Failure<DokumenPendukung>((DokumenPendukungErrors.NotFoundHibah(UuidPenelitianHibah)));
            }
            if (string.IsNullOrEmpty(File) && string.IsNullOrEmpty(Link)) {
                return Result.Failure<DokumenPendukung>((DokumenPendukungErrors.EmptyResource()));
            }
            if (!string.IsNullOrEmpty(File) && !string.IsNullOrEmpty(Link))
            {
                return Result.Failure<DokumenPendukung>((DokumenPendukungErrors.DuplicateResource()));
            }
            if (!string.IsNullOrEmpty(Link) && !IsValidGoogleDriveUrl(Link))
            {
                return Result.Failure<DokumenPendukung>((DokumenPendukungErrors.IvalidLink()));
            }

            var asset = new DokumenPendukung
            {
                Uuid = Guid.NewGuid(),
                PenelitianHibahId = existingPenelitianHibah?.Id ?? 0,
                File = File,
                Link = Link,
                Kategori = Kategori
            };

            asset.Raise(new DokumenPendukungCreatedDomainEvent(asset.Uuid));

            return asset;
        }

        public static Result<DokumenPendukung> Update(
          Guid uuid,
          Guid UuidPenelitianHibah,
          Domain.PenelitianHibah.PenelitianHibah? existingPenelitianHibah,
          DokumenPendukung? prev,
          string? File,
          string? Link,
          string Kategori
        )
        {
            if (prev == null)
            {
                return Result.Failure<DokumenPendukung>((DokumenPendukungErrors.NotFound(uuid)));
            }
            if (existingPenelitianHibah == null)
            {
                return Result.Failure<DokumenPendukung>((DokumenPendukungErrors.NotFoundHibah(UuidPenelitianHibah)));
            }
            if (prev?.PenelitianHibahId != existingPenelitianHibah?.Id)
            {
                return Result.Failure<DokumenPendukung>(DokumenPendukungErrors.InvalidData());
            }
            if (string.IsNullOrEmpty(File) && string.IsNullOrEmpty(Link))
            {
                return Result.Failure<DokumenPendukung>((DokumenPendukungErrors.EmptyResource()));
            }
            if (!string.IsNullOrEmpty(File) && !string.IsNullOrEmpty(Link))
            {
                return Result.Failure<DokumenPendukung>((DokumenPendukungErrors.DuplicateResource()));
            }
            if (!string.IsNullOrEmpty(Link) && !IsValidGoogleDriveUrl(Link))
            {
                return Result.Failure<DokumenPendukung>((DokumenPendukungErrors.IvalidLink()));
            }

            prev.File = File;
            prev.Link = Link;
            prev.Kategori = Kategori;

            return prev;
        }

        private static bool IsValidGoogleDriveUrl(string url)
        {
            if (Uri.TryCreate(url, UriKind.Absolute, out var uri))
            {
                var isHttpOrHttps = uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps;
                var isDirectDriveLink = uri.Host.Equals("drive.google.com", StringComparison.OrdinalIgnoreCase);

                return isHttpOrHttps && isDirectDriveLink;
            }

            return false;
        }
    }
}
