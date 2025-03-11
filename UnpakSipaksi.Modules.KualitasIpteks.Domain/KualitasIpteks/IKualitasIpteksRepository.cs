namespace UnpakSipaksi.Modules.KualitasIpteks.Domain.KualitasIpteks
{
    public interface IKualitasIpteksRepository
    {
        void Insert(KualitasIpteks KualitasIpteks);
        Task<KualitasIpteks> GetAsync(Guid Uuid, CancellationToken cancellationToken = default);
        Task DeleteAsync(KualitasIpteks KualitasIpteks);
    }
}
