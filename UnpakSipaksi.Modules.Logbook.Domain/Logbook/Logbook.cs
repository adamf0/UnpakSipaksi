using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.Logbook.Domain.Logbook
{
    public sealed partial class Logbook : Entity
    {
        private Logbook()
        {
        }

        public int? Id { get; private set; } = null;

        [Column(TypeName = "VARCHAR(36)")]
        public Guid Uuid{ get; private set; }

        [Column("tanggal_kegiatan")]
        public DateOnly? TanggalKegiatan { get; private set; }
        public string Lampiran { get; private set; }
        public string Deskripsi { get; private set; }
        public double Persentase { get; private set; }
        [Column("id_pdp")]
        public int PenelitianHibahId { get; private set; }
        [Column("id_pkm")]
        public int PenelitianPkmId { get; private set; }

        public static Result<Logbook> Create(
            int PenelitianHibahId,
            int PenelitianPkmId,
            string TanggalKegiatan,
            string Lampiran,
            string Deskripsi,
            double Persentase,
            double currentTotalPersentase
        )
        {
            DateOnly? tanggalKegiatan = DateOnly.TryParse(TanggalKegiatan, out var tanggal) ? tanggal : null;

            if (tanggalKegiatan==null) {
                return Result.Failure<Logbook>(LogbookErrors.InvalidFormatDate());
            }
            if (Persentase<0) {
                return Result.Failure<Logbook>(LogbookErrors.InvalidFormatPercentage());
            }
            if ((currentTotalPersentase + Persentase) > 100) {
                return Result.Failure<Logbook>(LogbookErrors.OverCapacity());
            }
            if ((PenelitianHibahId <= 0 && PenelitianPkmId <= 0) || (PenelitianHibahId > 0 && PenelitianPkmId > 0)) {
                return Result.Failure<Logbook>(LogbookErrors.NoTargetHibah());
            }

            var asset = new Logbook
            {
                Uuid = Guid.NewGuid(),
                Lampiran = Lampiran,
                Deskripsi = Deskripsi,
                Persentase = Persentase,
                TanggalKegiatan = tanggalKegiatan,
                PenelitianHibahId = PenelitianHibahId,
                PenelitianPkmId = PenelitianPkmId
            };

            asset.Raise(new KelompokMitraCreatedDomainEvent(asset.Uuid));

            return asset;
        }

        public static Result<Logbook> Update(
            Domain.Logbook.Logbook prev,
            int PenelitianHibahId,
            int PenelitianPkmId,
            string TanggalKegiatan,
            string Lampiran,
            string Deskripsi,
            double Persentase,
            double currentTotalPersentase
        )
        {
            DateOnly? tanggalKegiatan = DateOnly.TryParse(TanggalKegiatan, out var tanggal) ? tanggal : null;

            if (tanggalKegiatan == null)
            {
                return Result.Failure<Logbook>(LogbookErrors.InvalidFormatDate());
            }
            if (Persentase < 0)
            {
                return Result.Failure<Logbook>(LogbookErrors.InvalidFormatPercentage());
            }
            if ((currentTotalPersentase + Persentase) > 100)
            {
                return Result.Failure<Logbook>(LogbookErrors.OverCapacity());
            }
            if ((PenelitianHibahId <= 0 && PenelitianPkmId <= 0) || (PenelitianHibahId > 0 && PenelitianPkmId > 0))
            {
                return Result.Failure<Logbook>(LogbookErrors.NoTargetHibah());
            }
            if (prev == null) {
                return Result.Failure<Logbook>(LogbookErrors.EmptyData());
            }
            if (prev?.PenelitianHibahId != PenelitianHibahId || prev?.PenelitianPkmId != PenelitianPkmId)
            {
                return Result.Failure<Logbook>(LogbookErrors.InvalidData());
            }

            var asset = new Logbook
            {
                Uuid = Guid.NewGuid(),
                Lampiran = Lampiran,
                Deskripsi = Deskripsi,
                Persentase = Persentase,
                TanggalKegiatan = tanggalKegiatan,
                PenelitianHibahId = PenelitianHibahId,
                PenelitianPkmId = PenelitianPkmId
            };

            return asset;
        }
    }
}
