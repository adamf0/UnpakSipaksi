using Microsoft.AspNetCore.Routing;

namespace UnpakSipaksi.Modules.ArtikelMediaMassa.Presentation.ArtikelMediaMassa
{
    public static class ArtikelMediaMassaEndpoints
    {
        public static void MapEndpoints(IEndpointRouteBuilder app)
        {
            CreateArtikelMediaMassa.MapEndpoint(app);
            UpdateArtikelMediaMassa.MapEndpoint(app);
            DeleteArtikelMediaMassa.MapEndpoint(app);
            GetArtikelMediaMassa.MapEndpoint(app);
            GetAllArtikelMediaMassa.MapEndpoint(app);
        }
    }
}
