namespace UnpakSipaksi.Modules.RoadmapPenelitian.Domain.RoadmapPenelitian
{
    public interface IRoadmapPenelitianRepository
    {
        void Insert(RoadmapPenelitian RoadmapPenelitian);
        Task<RoadmapPenelitian> GetAsync(Guid Uuid, CancellationToken cancellationToken = default);
        Task DeleteAsync(RoadmapPenelitian RoadmapPenelitian);
    }
}
