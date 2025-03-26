namespace UnpakSipaksi.Modules.VideoKegiatan.Application.GetVideoKegiatan
{
    public sealed record VideoKegiatanResponse
    {
        public string Uuid { get; set; }
        public string Nama { get; set; } = default!;
        public string Nilai { get; set; } = default!;
    }
}
