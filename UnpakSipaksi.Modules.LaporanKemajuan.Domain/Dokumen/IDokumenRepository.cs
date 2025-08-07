namespace UnpakSipaksi.Modules.LaporanKemajuan.Domain.Dokumen
{
    public interface IDokumenRepository
    {
        void Insert(Dokumen KetajamanAnalisis);
        Task<Dokumen> GetAsync(Guid Uuid, CancellationToken cancellationToken = default);
        Task DeleteAsync(Dokumen KetajamanAnalisis);
    }
}
