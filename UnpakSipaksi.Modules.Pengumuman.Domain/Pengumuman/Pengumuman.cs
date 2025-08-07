using System.ComponentModel.DataAnnotations.Schema;
using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.Pengumuman.Domain.Pengumuman
{
    public sealed partial class Pengumuman : Entity
    {
        private Pengumuman()
        {
        }

        public int? Id { get; private set; } = null;

        [Column(TypeName = "VARCHAR(36)")]
        public Guid Uuid{ get; private set; }

        public string Pesan { get; private set; } = null!;
        public string? File { get; private set; }
        public string? Url { get; private set; }
        public string Type { get; private set; } = null!;
        public string? Target { get; private set; }
        public string? Nidn { get; private set; }
        public string? KodeFaKultas { get; private set; }
        public string? TypeExpired { get; private set; }
        public string? TanggalAwal { get; private set; }
        public string? TanggalAkhir { get; private set; }

        public static PengumumanBuilder Update(Pengumuman prev) => new PengumumanBuilder(prev);

        public static Result<Pengumuman> Create(
        string Pesan,
        AnnouncementInfo AnnouncementInfo,
        Attachment? Attachment,
        ExpiredInfo expiredInfo
        )
        {
            if (expiredInfo.validationResult.IsFailure) {
                return expiredInfo.validationResult;
            }
            if (!string.IsNullOrEmpty(AnnouncementInfo?.Nidn) && !DomainValidator.IsValidNidn(AnnouncementInfo?.Nidn))
            {
                return Result.Failure<Pengumuman>(PengumumanErrors.InvalidNidn());
            }

            var pengumuman = new Pengumuman
            {
                Uuid = Guid.NewGuid(),
                Pesan = Pesan,
                Type = AnnouncementInfo.Type.ToString(),
                Target = AnnouncementInfo.Target.ToString(),
                Nidn = AnnouncementInfo?.Nidn,
                KodeFaKultas = AnnouncementInfo?.KodeFakultas,
                File = Attachment?.Path,
                Url = Attachment?.Link,
                TypeExpired = expiredInfo.Type.ToString(),
                TanggalAwal = expiredInfo.TanggalAwal?.ToString(),
                TanggalAkhir = expiredInfo.TanggalAkhir?.ToString()
            };

            pengumuman.Raise(new PengumumanCreatedDomainEvent(pengumuman.Uuid));

            //[PR] raise event
            //bisa raise event untuk send email, WS / signalr, fcm

            return Result.Success(pengumuman);
        }
    }
}
