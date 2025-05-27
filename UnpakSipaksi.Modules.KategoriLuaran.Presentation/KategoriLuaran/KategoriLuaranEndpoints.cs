using Microsoft.AspNetCore.Routing;

namespace UnpakSipaksi.Modules.KategoriLuaran.Presentation.KategoriLuaran
{
    public static class KategoriLuaranEndpoints
    {
        public static void MapEndpoints(IEndpointRouteBuilder app)
        {
            CreateKategoriLuaran.MapEndpoint(app);
            UpdateKategoriLuaran.MapEndpoint(app);
            DeleteKategoriLuaran.MapEndpoint(app);
            GetKategoriLuaran.MapEndpoint(app);
            GetAllKategoriLuaran.MapEndpoint(app);
        }
    }
}
