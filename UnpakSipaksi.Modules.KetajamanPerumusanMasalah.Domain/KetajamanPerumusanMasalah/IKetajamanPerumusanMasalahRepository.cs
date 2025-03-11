
namespace UnpakSipaksi.Modules.KetajamanPerumusanMasalah.Domain.KetajamanPerumusanMasalah
{
    public interface IKetajamanPerumusanMasalahRepository
    {
        void Insert(KetajamanPerumusanMasalah KetajamanPerumusanMasalah);
        Task<KetajamanPerumusanMasalah> GetAsync(Guid Uuid, CancellationToken cancellationToken = default);
        Task DeleteAsync(KetajamanPerumusanMasalah KetajamanPerumusanMasalah);
    }
}
