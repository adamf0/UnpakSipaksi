namespace UnpakSipaksi.Modules.KategoriSkema.Domain.KategoriSkema
{
    public interface IKategoriSkemaRepository
    {
        void Insert(KategoriSkema KategoriSkema);
        Task<KategoriSkema> GetAsync(Guid Uuid, CancellationToken cancellationToken = default);
        Task DeleteAsync(KategoriSkema KategoriSkema);
    }
}
