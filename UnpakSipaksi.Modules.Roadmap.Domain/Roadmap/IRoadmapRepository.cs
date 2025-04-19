namespace UnpakSipaksi.Modules.Roadmap.Domain.Roadmap
{
    public interface IRoadmapRepository
    {
        void Insert(Roadmap Roadmap);
        Task<Roadmap> GetAsync(Guid Uuid, CancellationToken cancellationToken = default);
        Task DeleteAsync(Roadmap Roadmap);
    }
}
