namespace UnpakSipaksi.Modules.RoadmapPenelitian.Application.GetRoadmapPenelitian
{
    public sealed record RoadmapPenelitianDefaultResponse
    {
        public string Id { get; set; }
        public string Uuid { get; set; }
        public string Nama { get; set; } = default!;
        public int BobotPDP { get; set; }
        public int BobotTerapan { get; set; }
        public int BobotKerjasama { get; set; }
        public int BobotPenelitianDasar { get; set; }
        public int Skor { get; set; }
    }
}
