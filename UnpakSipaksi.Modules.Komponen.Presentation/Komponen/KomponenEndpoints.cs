using Microsoft.AspNetCore.Routing;

namespace UnpakSipaksi.Modules.Komponen.Presentation.Komponen
{
    public static class KomponenEndpoints
    {
        public static void MapEndpoints(IEndpointRouteBuilder app)
        {
            CreateKomponen.MapEndpoint(app);
            UpdateKomponen.MapEndpoint(app);
            DeleteKomponen.MapEndpoint(app);
            GetKomponen.MapEndpoint(app);
            GetAllKomponen.MapEndpoint(app);
        }
    }
}
