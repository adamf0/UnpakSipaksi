using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.Substansi.Domain.SubstansiInternal
{
    public sealed partial class SubstansiInternal : Entity
    {
        private SubstansiInternal()
        {
        }

        public int? Id { get; private set; } = null;

        [Column(TypeName = "VARCHAR(36)")]
        public Guid Uuid { get; private set; }

        [Column("id_pdp")]
        public int PenelitianHibahId { get; private set; }

        [Column("id_publikasi_disitasi_proposal")]
        public int? PublikasiDisitasiProposalId { get; private set; } = null!;

        [Column("id_relevansi_kepakaran_tema_proposal")]
        public int? RelevansiKepakaranTemaProposalId { get; private set; } = null!;

        [Column("id_jumlah_kolaborator_publikasi_bereputasi")]
        public int? JumlahKolaboratorPublikasiBereputasiId { get; private set; } = null!;

        [Column("id_relevansi_produk_kepentingan_nasional")]
        public int? RelevansiProdukKepentinganNasionalId { get; private set; } = null!;

        [Column("id_ketajaman_perumusan_masalah")]
        public int? KetajamanPerumusanMasalahId { get; private set; } = null!;

        [Column("id_inovasi_pemecahan_masalah")]
        public int? InovasiPemecahanMasalahId { get; private set; } = null!;

        [Column("id_sota_kebaharuan")]
        public int? SotaKebaharuanId { get; private set; } = null!;

        [Column("id_roadmap_penelitian")]
        public int? RoadmapPenelitianId { get; private set; } = null!;

        [Column("id_akurasi_penelitian")]
        public int? AkurasiPenelitianId { get; private set; } = null!;

        [Column("id_kejelasan_pembagian_tugas_tim")]
        public int? KejelasanPembagianTugasTimId { get; private set; } = null!;

        [Column("id_kesesuaian_waktu_rab_luaran_fasilitas")]
        public int? KesesuaianWaktuRabLuaranFasilitasId { get; private set; } = null!;

        [Column("id_potensi_ketercapaian_luaran_dijanjikan")]
        public int? PotensiKetercapaianLuaranDijanjikanId { get; private set; } = null!;

        [Column("id_model_feasibility_study")]
        public int? ModelFeasibilityStudyId { get; private set; } = null!;

        [Column("id_kesesuaian_tkt")]
        public int? KesesuaianTktId { get; private set; } = null!;

        [Column("id_kredibilitas_mitra_dukungan")]
        public int? KredibilitasMitraDukunganId { get; private set; } = null!;

        [Column("id_kebaruan_referensi")]
        public int? KebaruanReferensiId { get; private set; } = null!;

        [Column("id_relevansi_kualitas_referensi")]
        public int? RelevansiKualitasReferensiId { get; private set; } = null!;

        [Column("Komentar")]
        public string? Komentar { get; private set; } = null!;

        [Column("reviewerInternal")]
        public string? ReviewerInternal { get; private set; } = null!;

        [Column("reviewerExternal", TypeName = "bigint unsigned")]
        public ulong? ReviewerExternal { get; private set; } = null!;

        [Column("tanggal_mulai")]
        public DateTime TanggalMulai { get; private set; }

        [Column("tanggal_berakhir")]
        public DateTime TanggalBerakhir { get; private set; }


        public static Result<SubstansiInternal> Create(
            int PenelitianHibahId,
            int PublikasiDisitasiProposalId,
            int RelevansiKepakaranTemaProposalId,
            int JumlahKolaboratorPublikasiBereputasiId,
            int RelevansiProdukKepentinganNasionalId,
            int KetajamanPerumusanMasalahId,
            int InovasiPemecahanMasalahId,
            int SotaKebaharuanId,
            int RoadmapPenelitianId,
            int AkurasiPenelitianId,
            int KejelasanPembagianTugasTimId,
            int KesesuaianWaktuRabLuaranFasilitasId,
            int PotensiKetercapaianLuaranDijanjikanId,
            int ModelFeasibilityStudyId,
            int KesesuaianTktId,
            int KredibilitasMitraDukunganId,
            int KebaruanReferensiId,
            int RelevansiKualitasReferensiId,
            string Komentar,
            string? ReviewerInternal,
            ulong? ReviewerExternal,
            string TanggalMulai,
            string TanggalBerakhir
        )
        {
            if (PenelitianHibahId == 0)
            {
                return Result.Failure<SubstansiInternal>(SubstansiInternalErrors.InvalidPenelitianHibah());
            }

            var validTanggalMulai = DateTime.TryParseExact(TanggalMulai, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsedTanggalMulai);
            var validTanggalBerakhir = DateTime.TryParseExact(TanggalBerakhir, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsedTanggalBerakhir);
            if (
                !validTanggalMulai ||
                !validTanggalBerakhir ||
                parsedTanggalMulai > parsedTanggalBerakhir
            )
            {
                return Result.Failure<SubstansiInternal>(SubstansiInternalErrors.InvalidDateRangeReview());
            }

            var entity = new SubstansiInternal
            {
                Uuid = Guid.NewGuid(),
                PenelitianHibahId = PenelitianHibahId,
                PublikasiDisitasiProposalId = PublikasiDisitasiProposalId,
                RelevansiKepakaranTemaProposalId = RelevansiKepakaranTemaProposalId,
                JumlahKolaboratorPublikasiBereputasiId = JumlahKolaboratorPublikasiBereputasiId,
                RelevansiProdukKepentinganNasionalId = RelevansiProdukKepentinganNasionalId,
                KetajamanPerumusanMasalahId = KetajamanPerumusanMasalahId,
                InovasiPemecahanMasalahId = InovasiPemecahanMasalahId,
                SotaKebaharuanId = SotaKebaharuanId,
                RoadmapPenelitianId = RoadmapPenelitianId,
                AkurasiPenelitianId = AkurasiPenelitianId,
                KejelasanPembagianTugasTimId = KejelasanPembagianTugasTimId,
                KesesuaianWaktuRabLuaranFasilitasId = KesesuaianWaktuRabLuaranFasilitasId,
                PotensiKetercapaianLuaranDijanjikanId = PotensiKetercapaianLuaranDijanjikanId,
                ModelFeasibilityStudyId = ModelFeasibilityStudyId,
                KesesuaianTktId = KesesuaianTktId,
                KredibilitasMitraDukunganId = KredibilitasMitraDukunganId,
                KebaruanReferensiId = KebaruanReferensiId,
                RelevansiKualitasReferensiId = RelevansiKualitasReferensiId,
                Komentar = Komentar,
                ReviewerInternal = ReviewerInternal,
                ReviewerExternal = ReviewerExternal,
                TanggalMulai = parsedTanggalMulai,
                TanggalBerakhir = parsedTanggalBerakhir
            };

            var emptyFields = entity.GetContentAttributes()
                                .Where(kv => int.Parse(kv.Value ?? "0") <= 0)
                                .Select(kv => kv.Key)
                                .ToList();

            if (emptyFields.Any() && emptyFields.Count < 21)
            {
                return Result.Failure<SubstansiInternal>(SubstansiInternalErrors.InvalidArgument());
            }
            if (ReviewerInternal == null && ReviewerExternal == null)
            {
                return Result.Failure<SubstansiInternal>(SubstansiInternalErrors.InvalidReviewer());
            }

            entity.Raise(new SubstansiCreatedDomainEvent(entity.Uuid));

            return entity;
        }

        public static Result<SubstansiInternal> Update(
            Guid Uuid,
            SubstansiInternal? prev,
            int PenelitianHibahId,
            int PublikasiDisitasiProposalId,
            int RelevansiKepakaranTemaProposalId,
            int JumlahKolaboratorPublikasiBereputasiId,
            int RelevansiProdukKepentinganNasionalId,
            int KetajamanPerumusanMasalahId,
            int InovasiPemecahanMasalahId,
            int SotaKebaharuanId,
            int RoadmapPenelitianId,
            int AkurasiPenelitianId,
            int KejelasanPembagianTugasTimId,
            int KesesuaianWaktuRabLuaranFasilitasId,
            int PotensiKetercapaianLuaranDijanjikanId,
            int ModelFeasibilityStudyId,
            int KesesuaianTktId,
            int KredibilitasMitraDukunganId,
            int KebaruanReferensiId,
            int RelevansiKualitasReferensiId,
            string Komentar,
            string? ReviewerInternal,
            ulong? ReviewerExternal,
            string TanggalMulai,
            string TanggalBerakhir
        )
        {
            if (prev == null)
            {
                return Result.Failure<SubstansiInternal>(SubstansiInternalErrors.NotFound(Uuid));
            }
            if (PenelitianHibahId == 0 || PenelitianHibahId != prev?.PenelitianHibahId)
            {
                return Result.Failure<SubstansiInternal>(SubstansiInternalErrors.InvalidPenelitianHibah());
            }

            var validTanggalMulai = DateTime.TryParseExact(TanggalMulai, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsedTanggalMulai);
            var validTanggalBerakhir = DateTime.TryParseExact(TanggalBerakhir, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsedTanggalBerakhir);
            if (
                !validTanggalMulai ||
                !validTanggalBerakhir ||
                parsedTanggalMulai > parsedTanggalBerakhir
            )
            {
                return Result.Failure<SubstansiInternal>(SubstansiInternalErrors.InvalidDateRangeReview());
            }

            prev.PublikasiDisitasiProposalId = PublikasiDisitasiProposalId;
            prev.RelevansiKepakaranTemaProposalId = RelevansiKepakaranTemaProposalId;
            prev.JumlahKolaboratorPublikasiBereputasiId = JumlahKolaboratorPublikasiBereputasiId;
            prev.RelevansiProdukKepentinganNasionalId = RelevansiProdukKepentinganNasionalId;
            prev.KetajamanPerumusanMasalahId = KetajamanPerumusanMasalahId;
            prev.InovasiPemecahanMasalahId = InovasiPemecahanMasalahId;
            prev.SotaKebaharuanId = SotaKebaharuanId;
            prev.RoadmapPenelitianId = RoadmapPenelitianId;
            prev.AkurasiPenelitianId = AkurasiPenelitianId;
            prev.KejelasanPembagianTugasTimId = KejelasanPembagianTugasTimId;
            prev.KesesuaianWaktuRabLuaranFasilitasId = KesesuaianWaktuRabLuaranFasilitasId;
            prev.PotensiKetercapaianLuaranDijanjikanId = PotensiKetercapaianLuaranDijanjikanId;
            prev.ModelFeasibilityStudyId = ModelFeasibilityStudyId;
            prev.KesesuaianTktId = KesesuaianTktId;
            prev.KredibilitasMitraDukunganId = KredibilitasMitraDukunganId;
            prev.KebaruanReferensiId = KebaruanReferensiId;
            prev.RelevansiKualitasReferensiId = RelevansiKualitasReferensiId;
            prev.Komentar = Komentar;
            prev.ReviewerInternal = ReviewerInternal;
            prev.ReviewerExternal = ReviewerExternal;
            prev.TanggalMulai = parsedTanggalMulai;
            prev.TanggalBerakhir = parsedTanggalBerakhir;

            var emptyFields = prev.GetContentAttributes()
                                .Where(kv => int.Parse(kv.Value ?? "0") <= 0)
                                .Select(kv => kv.Key)
                                .ToList();

            if (emptyFields.Any() && emptyFields.Count < 21)
            {
                return Result.Failure<SubstansiInternal>(SubstansiInternalErrors.InvalidArgument());
            }
            if (ReviewerInternal == null && ReviewerExternal == null)
            {
                return Result.Failure<SubstansiInternal>(SubstansiInternalErrors.InvalidReviewer());
            }

            return prev;
        }
    }
}
