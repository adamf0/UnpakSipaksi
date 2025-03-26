namespace UnpakSipaksi.Modules.SotaKebaharuan.Domain.SotaKebaharuan
{
    public interface ISotaKebaharuanRepository
    {
        void Insert(SotaKebaharuan SotaKebaharuan);
        Task<SotaKebaharuan> GetAsync(Guid Uuid, CancellationToken cancellationToken = default);
        Task DeleteAsync(SotaKebaharuan SotaKebaharuan);
    }
}
