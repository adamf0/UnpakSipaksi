namespace UnpakSipaksi.Modules.PenugasanReviewer.Domain.PenugasanReviewer
{
    public interface IPenugasanReviewerRepository
    {
        void Insert(PenugasanReviewer PenugasanReviewer);
        Task<PenugasanReviewer> GetAsync(Guid Uuid, CancellationToken cancellationToken = default);
        Task DeleteAsync(PenugasanReviewer PenugasanReviewer);
    }
}
