using Microsoft.AspNetCore.Routing;

namespace UnpakSipaksi.Modules.Kategori.Presentation.Kategori
{
    public static class KategoriEndpoints
    {
        public static void MapEndpoints(IEndpointRouteBuilder app)
        {
            CreateKategori.MapEndpoint(app);
            UpdateKategori.MapEndpoint(app);
            DeleteKategori.MapEndpoint(app);
            GetKategori.MapEndpoint(app);
            GetAllKategori.MapEndpoint(app);
        }
    }
}
