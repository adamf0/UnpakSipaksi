using System.ComponentModel.DataAnnotations.Schema;

namespace UnpakSipaksi.Modules.Substansi.Application.GetSubstansiInternal
{
    public sealed record SubstansiInternalResponse
    {
        public string Uuid { get; set; }
        public string UuidPublikasiDisitasiProposal { get; private set; } = null!;
        public string UuidRelevansiKepakaranTemaProposal { get; private set; } = null!;
        public string UuidJumlahKolaboratorPublikasiBereputasi { get; private set; } = null!;
        public string UuidRelevansiProdukKepentinganNasional { get; private set; } = null!;
        public string UuidKetajamanPerumusanMasalah { get; private set; } = null!;
        public string UuidInovasiPemecahanMasalah { get; private set; } = null!;
        public string UuidSotaKebaharuan { get; private set; } = null!;
        public string UuidRoadmapPenelitian { get; private set; } = null!;
        public string UuidAkurasiPenelitian { get; private set; } = null!;
        public string UuidKejelasanPembagianTugasTim { get; private set; } = null!;
        public string UuidKesesuaianWaktuRabLuaranFasilitas { get; private set; } = null!;
        public string UuidPotensiKetercapaianLuaranDijanjikan { get; private set; } = null!;
        public string UuidModelFeasibilityStudy { get; private set; } = null!;
        public string UuidKesesuaianTkt { get; private set; } = null!;
        public string UuidKredibilitasMitraDukungan { get; private set; } = null!;
        public string UuidKebaruanReferensi { get; private set; } = null!;
        public string UuidRelevansiKualitasReferensi { get; private set; } = null!;
        public string? Komentar { get; private set; } = null!;
        public string ReviewerInternal { get; private set; } = null!;
        public string ReviewerExternal { get; private set; } = null!;
        public string TanggalMulai { get; private set; }
        public string TanggalBerakhir { get; private set; }
    }
}
