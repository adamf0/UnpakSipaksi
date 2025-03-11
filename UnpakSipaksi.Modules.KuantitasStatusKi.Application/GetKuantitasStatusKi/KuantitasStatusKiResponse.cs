namespace UnpakSipaksi.Modules.KuantitasStatusKi.Application.GetKuantitasStatusKi
{
    public sealed record KuantitasStatusKiResponse
    {
        public string Uuid { get; set; }
        public string Nama { get; set; } = default!;
        public string Nilai { get; set; } = default!;
    }
}
