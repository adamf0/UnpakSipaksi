namespace UnpakSipaksi.Modules.Komponen.Application.GetKomponen
{
    public sealed record KomponenResponse
    {
        public string Uuid { get; set; }
        public string Nama { get; set; } = default!;
        public int Nilai { get; set; }
    }
}
