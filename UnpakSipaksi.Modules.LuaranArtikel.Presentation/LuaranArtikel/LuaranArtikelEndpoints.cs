using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Modules.LuaranArtikel.Presentation.LuaranArtikel;

namespace UnpakSipaksi.Modules.LuaranArtikel.Presentation.LuaranArtikel
{
    public static class LuaranArtikelEndpoints
    {
        public static void MapEndpoints(IEndpointRouteBuilder app)
        {
            CreateLuaranArtikel.MapEndpoint(app);
            UpdateLuaranArtikel.MapEndpoint(app);
            DeleteLuaranArtikel.MapEndpoint(app);
            GetLuaranArtikel.MapEndpoint(app);
            GetAllLuaranArtikel.MapEndpoint(app);
        }
    }
}
