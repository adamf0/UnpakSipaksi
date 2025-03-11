namespace UnpakSipaksi.Modules.KewajaranTahapanTarget.Domain.KewajaranTahapanTarget
{
    public interface IKewajaranTahapanTargetRepository
    {
        void Insert(KewajaranTahapanTarget KewajaranTahapanTarget);
        Task<KewajaranTahapanTarget> GetAsync(Guid Uuid, CancellationToken cancellationToken = default);
        Task DeleteAsync(KewajaranTahapanTarget KewajaranTahapanTarget);
    }
}
