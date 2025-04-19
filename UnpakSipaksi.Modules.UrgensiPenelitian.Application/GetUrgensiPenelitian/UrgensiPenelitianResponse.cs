namespace UnpakSipaksi.Modules.UrgensiPenelitian.Application.GetUrgensiPenelitian
{
    public sealed record UrgensiPenelitianResponse
    {
        public string Uuid { get; set; }
        public string UuidRelevansiProdukKepentinganNasional { get; set; } = default!;
        public string UuidKetajamanPerumusanMasalah { get; set; } = default!;
        public string UuidInovasiPemecahanMasalah { get; set; } = default!;
        public string UuidSotaKebaharuan { get; set; } = default!;
        public string UuidRoadmapPenelitian { get; set; } = default!;
        public int Nilai { get; set; }
    }
}
