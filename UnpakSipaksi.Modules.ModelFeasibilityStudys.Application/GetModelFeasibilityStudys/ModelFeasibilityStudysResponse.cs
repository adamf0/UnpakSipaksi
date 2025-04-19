namespace UnpakSipaksi.Modules.ModelFeasibilityStudys.Application.GetModelFeasibilityStudys
{
    public sealed record ModelFeasibilityStudysResponse
    {
        public string Uuid { get; set; }
        public string Nama { get; set; } = default!;
        public int Skor { get; set; }
    }
}
