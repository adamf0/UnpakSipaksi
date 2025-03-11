namespace UnpakSipaksi.Modules.Komponen.Domain.Komponen
{
    public interface IKomponenRepository
    {
        void Insert(Komponen Komponen);
        Task<Komponen> GetAsync(Guid Uuid, CancellationToken cancellationToken = default);
        Task DeleteAsync(Komponen Komponen);
    }
}
