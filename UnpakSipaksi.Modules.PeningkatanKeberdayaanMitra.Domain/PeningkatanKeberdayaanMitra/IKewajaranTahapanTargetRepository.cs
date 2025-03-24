namespace UnpakSipaksi.Modules.PeningkatanKeberdayaanMitra.Domain.PeningkatanKeberdayaanMitra
{
    public interface IPeningkatanKeberdayaanMitraRepository
    {
        void Insert(PeningkatanKeberdayaanMitra PeningkatanKeberdayaanMitra);
        Task<PeningkatanKeberdayaanMitra> GetAsync(Guid Uuid, CancellationToken cancellationToken = default);
        Task DeleteAsync(PeningkatanKeberdayaanMitra PeningkatanKeberdayaanMitra);
    }
}
