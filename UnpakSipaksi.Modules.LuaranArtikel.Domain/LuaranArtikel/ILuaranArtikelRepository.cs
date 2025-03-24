namespace UnpakSipaksi.Modules.LuaranArtikel.Domain.LuaranArtikel
{
    public interface ILuaranArtikelRepository
    {
        void Insert(LuaranArtikel LuaranArtikel);
        Task<LuaranArtikel> GetAsync(Guid Uuid, CancellationToken cancellationToken = default);
        Task DeleteAsync(LuaranArtikel LuaranArtikel);
    }
}
