namespace UnpakSipaksi.Modules.RelevansiKualitasReferensi.Application.GetRelevansiKualitasReferensi
{
    public sealed record RelevansiKualitasReferensiResponse
    {
        public string Uuid { get; set; }
        public string Nama { get; set; } = default!;
        public int Skor { get; set; }
    }
}
