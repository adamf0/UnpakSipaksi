namespace UnpakSipaksi.Modules.KetajamanAnalisis.Domain.KetajamanAnalisis
{
    public interface IKetajamanAnalisisRepository
    {
        void Insert(KetajamanAnalisis KetajamanAnalisis);
        Task<KetajamanAnalisis> GetAsync(Guid Uuid, CancellationToken cancellationToken = default);
        Task DeleteAsync(KetajamanAnalisis KetajamanAnalisis);
    }
}
