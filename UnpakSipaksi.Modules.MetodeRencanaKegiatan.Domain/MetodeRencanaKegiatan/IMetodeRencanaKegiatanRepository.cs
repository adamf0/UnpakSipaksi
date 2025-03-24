namespace UnpakSipaksi.Modules.MetodeRencanaKegiatan.Domain.MetodeRencanaKegiatan
{
    public interface IMetodeRencanaKegiatanRepository
    {
        void Insert(MetodeRencanaKegiatan MetodeRencanaKegiatan);
        Task<MetodeRencanaKegiatan> GetAsync(Guid Uuid, CancellationToken cancellationToken = default);
        Task DeleteAsync(MetodeRencanaKegiatan MetodeRencanaKegiatan);
    }
}
