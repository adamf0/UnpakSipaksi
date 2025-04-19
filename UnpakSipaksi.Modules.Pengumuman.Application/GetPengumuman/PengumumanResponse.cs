namespace UnpakSipaksi.Modules.Pengumuman.Application.GetPengumuman
{
    public sealed record PengumumanResponse
    {
        public string Pesan { get;  set; } = null!;
        public string? File { get;  set; }
        public string? Url { get;  set; }
        public string Type { get;  set; } = null!;
        public string? Target { get;  set; }
        public string? Nidn { get;  set; }
        public string? KodeFaKultas { get;  set; }
        public string? TypeExpired { get;  set; }
        public string? TanggalAwal { get;  set; }
        public string? TanggalAkhir { get;  set; }
    }
}
