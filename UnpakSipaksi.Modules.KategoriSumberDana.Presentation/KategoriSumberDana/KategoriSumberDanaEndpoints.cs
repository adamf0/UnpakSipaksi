using Microsoft.AspNetCore.Routing;

namespace UnpakSipaksi.Modules.KategoriSumberDana.Presentation.KategoriSumberDana
{
    public static class KategoriSumberDanaEndpoints
    {
        public static void MapEndpoints(IEndpointRouteBuilder app)
        {
            CreateKategoriSumberDana.MapEndpoint(app);
            UpdateKategoriSumberDana.MapEndpoint(app);
            DeleteKategoriSumberDana.MapEndpoint(app);
            GetKategoriSumberDana.MapEndpoint(app);
            GetAllKategoriSumberDana.MapEndpoint(app);
        }
    }
}
