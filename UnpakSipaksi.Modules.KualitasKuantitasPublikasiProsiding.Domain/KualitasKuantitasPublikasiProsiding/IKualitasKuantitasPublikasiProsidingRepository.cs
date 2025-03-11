namespace UnpakSipaksi.Modules.KualitasKuantitasPublikasiProsiding.Domain.KualitasKuantitasPublikasiProsiding
{
    public interface IKualitasKuantitasPublikasiProsidingRepository
    {
        void Insert(KualitasKuantitasPublikasiProsiding KualitasKuantitasPublikasiProsiding);
        Task<KualitasKuantitasPublikasiProsiding> GetAsync(Guid Uuid, CancellationToken cancellationToken = default);
        Task DeleteAsync(KualitasKuantitasPublikasiProsiding KualitasKuantitasPublikasiProsiding);
    }
}
