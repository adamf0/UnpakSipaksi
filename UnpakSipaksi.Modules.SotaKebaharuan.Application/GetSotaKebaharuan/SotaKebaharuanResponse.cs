namespace UnpakSipaksi.Modules.SotaKebaharuan.Application.GetSotaKebaharuan
{
    public sealed record SotaKebaharuanResponse
    {
        public string Uuid { get; set; }
        public string Nama { get; set; } = default!;
        public int Skor { get; set; }
    }
}
