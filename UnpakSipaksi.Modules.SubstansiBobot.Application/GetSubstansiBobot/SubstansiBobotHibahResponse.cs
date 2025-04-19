namespace UnpakSipaksi.Modules.LuaranArtikel.Application.GetLuaranArtikel
{
    public sealed record SubstansiBobotHibahResponse
    {
        public int PublikasiDisitasiProposal { get; set; }
        public int RelevansiKepakaranTemaProposal { get; set; }
        public int JumlahKolaboratorPublikasiBereputasi { get; set; }
        public int RelevansiProdukKepentinganNasional { get; set; }
        public int KetajamanPerumusanMasalah { get; set; }
        public int InovasiPemecahanMasalah { get; set; }
        public int SotaKebaharuan { get; set; }
        public int RoadmapPenelitian { get; set; }
        public int AkurasiPenelitian { get; set; }
        public int KejelasanPembagianTugasTim { get; set; }
        public int KesesuaianWaktuRabLuaranFasilitas { get; set; }
        public int PotensiKetercapaianLuaranDijanjikan { get; set; }
        public int ModelFeasibilityStudy { get; set; }
        public int KesesuaianTkt { get; set; }
        public int KredibilitasMitraDukungan { get; set; }
        public int KebaruanReferensi { get; set; }
        public int RelevansiKualitasReferensi { get; set; }
    }
}
