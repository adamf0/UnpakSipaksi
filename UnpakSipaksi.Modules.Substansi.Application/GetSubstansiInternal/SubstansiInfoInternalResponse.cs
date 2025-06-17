using System.ComponentModel.DataAnnotations.Schema;

namespace UnpakSipaksi.Modules.Substansi.Application.GetSubstansiInternal
{
    public sealed record SubstansiInfoInternalResponse
    {
        public string Uuid { get; set; }
        public string? NamaPublikasiDisitasiProposal { get; private set; } = null!;
        public string? BobotPublikasiDisitasiProposal { get; private set; } = null!;

        public string? NamaRelevansiKepakaranTemaProposal { get; private set; } = null!;
        public string? BobotRelevansiKepakaranTemaProposal { get; private set; } = null!;

        public string? NamaJumlahKolaboratorPublikasiBereputasi { get; private set; } = null!;
        public string? BobotJumlahKolaboratorPublikasiBereputasi { get; private set; } = null!;

        public string? NamaRelevansiProdukKepentinganNasional { get; private set; } = null!;
        public string? BobotRelevansiProdukKepentinganNasional { get; private set; } = null!;

        public string? NamaKetajamanPerumusanMasalah { get; private set; } = null!;
        public string? BobotKetajamanPerumusanMasalah { get; private set; } = null!;

        public string? NamaInovasiPemecahanMasalah { get; private set; } = null!;
        public string? BobotInovasiPemecahanMasalah { get; private set; } = null!;

        public string? NamaSotaKebaharuan { get; private set; } = null!;
        public string? BobotSotaKebaharuan { get; private set; } = null!;

        public string? NamaRoadmapPenelitian { get; private set; } = null!;
        public string? BobotRoadmapPenelitian { get; private set; } = null!;

        public string? NamaAkurasiPenelitian { get; private set; } = null!;
        public string? BobotAkurasiPenelitian { get; private set; } = null!;

        public string? NamaKejelasanPembagianTugasTim { get; private set; } = null!;
        public string? BobotKejelasanPembagianTugasTim { get; private set; } = null!;

        public string? NamaKesesuaianWaktuRabLuaranFasilitas { get; private set; } = null!;
        public string? BobotKesesuaianWaktuRabLuaranFasilitas { get; private set; } = null!;

        public string? NamaPotensiKetercapaianLuaranDijanjikan { get; private set; } = null!;
        public string? BobotPotensiKetercapaianLuaranDijanjikan { get; private set; } = null!;

        public string? NamaModelFeasibilityStudy { get; private set; } = null!;
        public string? BobotModelFeasibilityStudy { get; private set; } = null!;

        public string? NamaKesesuaianTkt { get; private set; } = null!;
        public string? BobotKesesuaianTkt { get; private set; } = null!;

        public string? NamaKredibilitasMitraDukungan { get; private set; } = null!;
        public string? BobotKredibilitasMitraDukungan { get; private set; } = null!;

        public string? NamaKebaruanReferensi { get; private set; } = null!;
        public string? BobotKebaruanReferensi { get; private set; } = null!;

        public string? NamaRelevansiKualitasReferensi { get; private set; } = null!;
        public string? BobotRelevansiKualitasReferensi { get; private set; } = null!;

        public string? Komentar { get; private set; } = null!;
        public string? ReviewerInternal { get; private set; } = null!;
        public string? ReviewerExternal { get; private set; } = null!;
        public string TanggalMulai { get; private set; }
        public string TanggalBerakhir { get; private set; }
        public int? Total { get; private set; }
    }
}
