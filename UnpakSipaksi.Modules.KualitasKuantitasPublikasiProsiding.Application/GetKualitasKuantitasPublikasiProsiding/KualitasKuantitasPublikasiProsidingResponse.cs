
namespace UnpakSipaksi.Modules.KualitasKuantitasPublikasiProsiding.Application.GetKualitasKuantitasPublikasiProsiding
{
    public sealed record KualitasKuantitasPublikasiProsidingResponse
    {
        public string Uuid { get; set; }
        public string Nama { get; set; } = default!;
        public string Nilai { get; set; } = default!;
    }
}
