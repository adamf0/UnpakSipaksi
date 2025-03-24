namespace UnpakSipaksi.Modules.MetodeRencanaKegiatan.Application.GetMetodeRencanaKegiatan
{
    public sealed record MetodeRencanaKegiatanResponse
    {
        public string Uuid { get; set; }
        public string Nama { get; set; } = default!;
        public string Nilai { get; set; } = default!;
    }
}
