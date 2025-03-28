namespace UnpakSipaksi.Modules.PenugasanReviewer.Application.GetPenugasanReviewer
{
    public sealed record PenugasanReviewerResponse
    {
        public string Uuid { get; set; }
        public string Nama { get; set; } = default!;
        public string Nilai { get; set; } = default!;
    }
}
