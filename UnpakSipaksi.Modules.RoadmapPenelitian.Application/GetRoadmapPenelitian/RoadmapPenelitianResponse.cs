namespace UnpakSipaksi.Modules.RoadmapPenelitian.Application.GetRoadmapPenelitian
{
    public sealed record RoadmapPenelitianResponse
    {
        public string Uuid { get; set; }
        public string Nama { get; set; } = default!;
        public int Skor { get; set; }
    }
}
