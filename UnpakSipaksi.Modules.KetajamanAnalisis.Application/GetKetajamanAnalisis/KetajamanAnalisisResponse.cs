namespace UnpakSipaksi.Modules.KetajamanAnalisis.Application.GetKetajamanAnalisis
{
    public sealed record KetajamanAnalisisResponse
    {
        public string Uuid { get; set; }
        public string Nama { get; set; } = default!;
        public int Nilai { get; set; }
    }
}
