namespace UnpakSipaksi.Modules.KualitasKuantitasPublikasiJurnalIlmiah.Domain.KualitasKuantitasPublikasiJurnalIlmiah
{
    public interface IKualitasKuantitasPublikasiJurnalIlmiahRepository
    {
        void Insert(KualitasKuantitasPublikasiJurnalIlmiah KualitasKuantitasPublikasiJurnalIlmiah);
        Task<KualitasKuantitasPublikasiJurnalIlmiah> GetAsync(Guid Uuid, CancellationToken cancellationToken = default);
        Task DeleteAsync(KualitasKuantitasPublikasiJurnalIlmiah KualitasKuantitasPublikasiJurnalIlmiah);
    }
}
