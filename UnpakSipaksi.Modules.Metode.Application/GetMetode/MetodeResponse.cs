namespace UnpakSipaksi.Modules.Metode.Application.GetMetode
{
    public sealed record MetodeResponse
    {
        public string Uuid { get; set; }
        public string UuidAkurasiPenelitian { get; set; }
        public string UuidKejelasanPembagianTugasTim { get; set; }
        public string UuidKesesuaianWaktuRabLuaranFasilitas { get; set; }
        public string UuidPotensiKetercapaianLuaranDijanjikan { get; set; }
        public string UuidModelFeasibilityStudy { get; set; }
        public string UuidKesesuaianTkt { get; set; }
        public string UuidKredibilitasMitraDukungan { get; set; }
    }
}
