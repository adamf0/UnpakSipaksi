namespace UnpakSipaksi.Modules.RelevansiKualitasReferensi.Domain.RelevansiKualitasReferensi
{
    public interface IRelevansiKualitasReferensiRepository
    {
        void Insert(RelevansiKualitasReferensi RelevansiKualitasReferensi);
        Task<RelevansiKualitasReferensi> GetAsync(Guid Uuid, CancellationToken cancellationToken = default);
        Task DeleteAsync(RelevansiKualitasReferensi RelevansiKualitasReferensi);
    }
}
