namespace UnpakSipaksi.Modules.PeningkatanKeberdayaanMitra.Application.GetPeningkatanKeberdayaanMitra
{
    public sealed record PeningkatanKeberdayaanMitraResponse
    {
        public string Uuid { get; set; }
        public string Nama { get; set; } = default!;
        public string Nilai { get; set; } = default!;
    }
}
