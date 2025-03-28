namespace UnpakSipaksi.Modules.KategoriProgramPengabdian.Domain.KategoriProgramPengabdian
{
    public interface IKategoriProgramPengabdianRepository
    {
        void Insert(KategoriProgramPengabdian KategoriProgramPengabdian);
        Task<KategoriProgramPengabdian> GetAsync(Guid Uuid, CancellationToken cancellationToken = default);
        Task DeleteAsync(KategoriProgramPengabdian KategoriProgramPengabdian);
    }
}
