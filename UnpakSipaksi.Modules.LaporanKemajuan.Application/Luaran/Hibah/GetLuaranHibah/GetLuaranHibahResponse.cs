namespace UnpakSipaksi.Modules.LaporanKemajuan.Application.Luaran.Hibah.GetLuaranHibah
{
    public sealed record LuaranHibahResponse
    {
        public string Uuid { get; set; }
        public string UuidPenelitianHibah { get; set; } = default!;
        public string File { get; set; } = default!;
        public string Type { get; set; } = default!;
    }
}
