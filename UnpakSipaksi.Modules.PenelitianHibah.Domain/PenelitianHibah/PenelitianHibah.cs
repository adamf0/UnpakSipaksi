using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.PenelitianHibah.Domain.PenelitianHibah
{
    public sealed partial class PenelitianHibah : Entity
    {
        private PenelitianHibah()
        {
        }

        public int? Id { get; private set; } = null;

        [Column(TypeName = "VARCHAR(36)")]
        public Guid Uuid { get; private set; }

        [Column("NIDN")]
        public string NIDN { get; private set; }

        [Column("judul")]
        public string Judul { get; private set; }

        [Column("id_skema")]
        public int? SkemaId { get; private set; }

        [Column("tkt")]
        public int TKT { get; private set; }

        [Column("kategori_tkt")]
        public int KategoriTKT { get; private set; }

        [Column("id_bidang_fokus_penelitian")]
        public int? BidangFokusPenelitianId { get; private set; }

        [Column("id_bidang_fokus_penelitian_tema")]
        public int? BidangFokusPenelitianTemaId { get; private set; }

        [Column("id_bidang_fokus_penelitian_tema_topik")]
        public int? BidangFokusPenelitianTemaTopikId { get; private set; }

        [Column("id_rumpun_ilmu1")]
        public int? RumpunIlmu1Id { get; private set; }

        [Column("id_rumpun_ilmu2")]
        public int? RumpunIlmu2Id { get; private set; }

        [Column("id_rumpun_ilmu3")]
        public int? RumpunIlmu3Id { get; private set; }

        [Column("prioritas_riset")]
        public int? PrioritasRiset { get; private set; }

        [Column("tahun_pengajuan")]
        public DateTime TahunPengajuan { get; private set; }

        [Column("lama_kegiatan")]
        public int? LamaKegiatan { get; private set; }

        [Column("status")]
        public string Status { get; private set; }

        [Column("type")]
        public string? Type { get; private set; }

        [Column("catatan")]
        public string? Catatan { get; private set; }

        //[Column("catatan_tolak")]
        //public string? CatatanTolak { get; private set; }


        public static async Task<Result<PenelitianHibah>> Create(
          IPenelitianHibahRepository penelitianHibahRepository,
          string NIDN,
          string TahunPengajuan,
          string Judul
        )
        {
            bool isUnique = await penelitianHibahRepository.HasUniqueDataAsync(
                null,
                NIDN,
                Judul
            );
            if (!isUnique)
            {
                return Result.Failure<PenelitianHibah>(PenelitianHibahErrors.NotUnique(NIDN, Judul));
            }

            var validTanggal = DateTime.TryParseExact(TahunPengajuan, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsedTanggal);

            var asset = new PenelitianHibah
            {
                Uuid = Guid.NewGuid(),
                NIDN = NIDN,
                Judul = Judul,
                TahunPengajuan = parsedTanggal
            };

            asset.Raise(new PenelitianHibahCreatedDomainEvent(asset.Uuid));

            return asset;
        }

        public static Result<PenelitianHibah> Update(
          PenelitianHibah prev,
          string TahunPengajuan,
          string Judul
        )
        {
            var validTanggal = DateTime.TryParseExact(TahunPengajuan, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsedTanggal);

            prev.Judul = Judul;
            prev.TahunPengajuan = parsedTanggal;

            return prev;
        }

        public static Result<PenelitianHibah> UpdateSkema(
          PenelitianHibah? prev,
          int SkemaId,
          int TKT,
          int KategoriTKT
        )
        {
            if (prev is null)
            {
                return Result.Failure<PenelitianHibah>(PenelitianHibahErrors.EmptyData());
            }
            prev.SkemaId = SkemaId;
            prev.TKT = TKT;
            prev.KategoriTKT = KategoriTKT;

            return prev;
        }

        public static Result<PenelitianHibah> UpdateRiset(
          PenelitianHibah? prev,
          int? PrioritasRiset,
          int? BidangFokusPenelitianId,
          int? BidangFokusPenelitianTemaId,
          int? BidangFokusPenelitianTemaTopikId
        )
        {
            if (prev is null)
            {
                return Result.Failure<PenelitianHibah>(PenelitianHibahErrors.EmptyData());
            }

            prev.PrioritasRiset = PrioritasRiset;
            prev.BidangFokusPenelitianId = BidangFokusPenelitianId;
            prev.BidangFokusPenelitianTemaId = BidangFokusPenelitianTemaId;
            prev.BidangFokusPenelitianTemaTopikId = BidangFokusPenelitianTemaTopikId;

            return prev;
        }

        public static Result<PenelitianHibah> UpdateRumpunIlmu(
          PenelitianHibah? prev,
          int? RumpunIlmu1Id,
          int? RumpunIlmu2Id,
          int? RumpunIlmu3Id
        )
        {
            if (prev is null)
            {
                return Result.Failure<PenelitianHibah>(PenelitianHibahErrors.EmptyData());
            }

            prev.RumpunIlmu1Id = RumpunIlmu1Id;
            prev.RumpunIlmu2Id = RumpunIlmu2Id;
            prev.RumpunIlmu3Id = RumpunIlmu3Id;

            return prev;
        }

        public static Result<PenelitianHibah> UpdateLamaKegiatan(
          PenelitianHibah? prev,
          int LamaKegiatan
        )
        {
            if (prev is null)
            {
                return Result.Failure<PenelitianHibah>(PenelitianHibahErrors.EmptyData());
            }
            prev.LamaKegiatan = LamaKegiatan;

            return prev;
        }
    }
}
