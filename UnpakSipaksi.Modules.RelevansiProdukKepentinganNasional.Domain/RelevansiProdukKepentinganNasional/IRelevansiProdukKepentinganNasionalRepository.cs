namespace UnpakSipaksi.Modules.RelevansiProdukKepentinganNasional.Domain.RelevansiProdukKepentinganNasional
{
    public interface IRelevansiProdukKepentinganNasionalRepository
    {
        void Insert(RelevansiProdukKepentinganNasional RelevansiProdukKepentinganNasional);
        Task<RelevansiProdukKepentinganNasional> GetAsync(Guid Uuid, CancellationToken cancellationToken = default);
        Task DeleteAsync(RelevansiProdukKepentinganNasional RelevansiProdukKepentinganNasional);
    }
}
