
namespace UnpakSipaksi.Modules.KualitasIpteks.Application.GetKualitasIpteks
{
    public sealed record KualitasIpteksResponse
    {
        public string Uuid { get; set; }
        public string Nama { get; set; } = default!;
        public string Nilai { get; set; } = default!;
    }
}
