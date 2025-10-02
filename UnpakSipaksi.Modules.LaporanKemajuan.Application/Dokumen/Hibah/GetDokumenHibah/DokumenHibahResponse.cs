namespace UnpakSipaksi.Modules.LaporanKemajuan.Application.Dokumen.Hibah.GetDokumenHibah
{
    public sealed record DokumenHibahResponse
    {
        public string Uuid { get; set; }
        public string UuidPenelitianHibah { get; set; } = default!;
        public string File { get; set; } = default!;
        public string Type { get; set; } = default!;
    }
}
