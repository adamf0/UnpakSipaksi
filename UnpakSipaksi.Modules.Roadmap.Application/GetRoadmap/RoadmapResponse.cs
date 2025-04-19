namespace UnpakSipaksi.Modules.Roadmap.Application.GetRoadmap
{
    public sealed record RoadmapResponse
    {
        public string Uuid { get; set; }
        public string Nidn { get; set; } = default!;
        public string Link { get; set; } = default!;
    }
}
