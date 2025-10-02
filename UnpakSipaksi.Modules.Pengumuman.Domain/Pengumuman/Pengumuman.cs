using System.ComponentModel.DataAnnotations.Schema;
using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.Pengumuman.Domain.Pengumuman
{
    public sealed partial class Pengumuman : Entity
    {
        private Pengumuman()
        {
        }

        public int Id { get; private set; }   // AUTO_INCREMENT, jadi gak nullable

        [Column(TypeName = "VARCHAR(36)")]
        public Guid Uuid { get; private set; }

        [Column(TypeName = "TEXT")]
        public string Pesan { get; private set; } = null!;

        [Column(TypeName = "VARCHAR(500)")]
        public string? File { get; private set; }

        [Column(TypeName = "VARCHAR(1000)")]
        public string? Url { get; private set; }

        // ENUM sudah diganti jadi VARCHAR(20) + default di OnModelCreating
        [Column(TypeName = "VARCHAR(20)")]
        public string Type { get; private set; } = "pengumuman";

        [Column(TypeName = "VARCHAR(20)")]
        public string Target { get; private set; } = "all";

        [Column(TypeName = "VARCHAR(50)")]
        public string? Nidn { get; private set; }

        [Column(TypeName = "CHAR(9)")]
        public string? KodeFaKultas { get; private set; }

        [Column(TypeName = "VARCHAR(20)")]
        public string TypeExpired { get; private set; } = "no expire";

        [Column(TypeName = "DATETIME")]
        public DateTime? TanggalAwal { get; private set; }

        [Column(TypeName = "DATETIME")]
        public DateTime? TanggalAkhir { get; private set; }


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
            if (!string.IsNullOrEmpty(AnnouncementInfo?.Nidn) && !DomainValidator.IsValidNidn(AnnouncementInfo.Nidn))
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
                TanggalAwal = expiredInfo.TanggalAwal?.ToDateTime(TimeOnly.MinValue),
                TanggalAkhir = expiredInfo.TanggalAkhir?.ToDateTime(TimeOnly.MinValue)
            };

            pengumuman.Raise(new PengumumanCreatedDomainEvent(pengumuman.Uuid));

            //[PR] raise event
            //bisa raise event untuk send email, WS / signalr, fcm

            return Result.Success(pengumuman);
        }
    }
}
