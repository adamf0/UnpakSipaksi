namespace UnpakSipaksi.Modules.Pengumuman.Domain.Pengumuman
{
    public interface IPengumumanRepository
    {
        void Insert(Pengumuman Pengumuman);
        Task<Pengumuman> GetAsync(Guid Uuid, CancellationToken cancellationToken = default);
        Task DeleteAsync(Pengumuman Pengumuman);
    }
}
