namespace UnpakSipaksi.Modules.PotensiKetercapaianLuaranDijanjikan.Domain.PotensiKetercapaianLuaranDijanjikan
{
    public interface IPotensiKetercapaianLuaranDijanjikanRepository
    {
        void Insert(PotensiKetercapaianLuaranDijanjikan PotensiKetercapaianLuaranDijanjikan);
        Task<PotensiKetercapaianLuaranDijanjikan> GetAsync(Guid Uuid, CancellationToken cancellationToken = default);
        Task DeleteAsync(PotensiKetercapaianLuaranDijanjikan PotensiKetercapaianLuaranDijanjikan);
    }
}
