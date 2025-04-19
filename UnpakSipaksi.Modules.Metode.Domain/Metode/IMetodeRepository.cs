namespace UnpakSipaksi.Modules.Metode.Domain.Metode
{
    public interface IMetodeRepository
    {
        void Insert(Metode Metode);
        Task<Metode> GetAsync(Guid Uuid, CancellationToken cancellationToken = default);
        Task DeleteAsync(Metode Metode);
    }
}
