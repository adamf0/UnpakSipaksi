using Microsoft.AspNetCore.Routing;

namespace UnpakSipaksi.Modules.KategoriSkema.Presentation.KategoriSkema
{
    public static class KategoriSkemaEndpoints
    {
        public static void MapEndpoints(IEndpointRouteBuilder app)
        {
            CreateKategoriSkema.MapEndpoint(app);
            UpdateKategoriSkema.MapEndpoint(app);
            DeleteKategoriSkema.MapEndpoint(app);
            GetKategoriSkema.MapEndpoint(app);
            GetAllKategoriSkema.MapEndpoint(app);
        }
    }
}
