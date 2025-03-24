using Microsoft.AspNetCore.Routing;

namespace UnpakSipaksi.Modules.PotensiKetercapaianLuaranDijanjikan.Presentation.PotensiKetercapaianLuaranDijanjikan
{
    public static class PotensiKetercapaianLuaranDijanjikanEndpoints
    {
        public static void MapEndpoints(IEndpointRouteBuilder app)
        {
            CreatePotensiKetercapaianLuaranDijanjikan.MapEndpoint(app);
            UpdatePotensiKetercapaianLuaranDijanjikan.MapEndpoint(app);
            DeletePotensiKetercapaianLuaranDijanjikan.MapEndpoint(app);
            GetPotensiKetercapaianLuaranDijanjikan.MapEndpoint(app);
            GetAllPotensiKetercapaianLuaranDijanjikan.MapEndpoint(app);
        }
    }
}
