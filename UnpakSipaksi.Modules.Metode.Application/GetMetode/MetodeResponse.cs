namespace UnpakSipaksi.Modules.Metode.Application.GetMetode
{
    public sealed record MetodeResponse
    {
        public string Uuid { get; set; }
        public string UuidAkurasiPenelitian { get; private set; }
        public string UuidKejelasanPembagianTugasTim { get; private set; }
        public string UuidKesesuaianWaktuRabLuaranFasilitas { get; private set; }
        public string UuidPotensiKetercapaianLuaranDijanjikan { get; private set; }
        public string UuidModelFeasibilityStudy { get; private set; }
        public string UuidKesesuaianTkt { get; private set; }
        public string UuidKredibilitasMitraDukungan { get; private set; }
    }
}
