using Microsoft.AspNetCore.Routing;

namespace UnpakSipaksi.Modules.KualitasIpteks.Presentation.KualitasIpteks
{
    public static class KualitasIpteksEndpoints
    {
        public static void MapEndpoints(IEndpointRouteBuilder app)
        {
            CreateKualitasIpteks.MapEndpoint(app);
            UpdateKualitasIpteks.MapEndpoint(app);
            DeleteKualitasIpteks.MapEndpoint(app);
            GetKualitasIpteks.MapEndpoint(app);
            GetAllKualitasIpteks.MapEndpoint(app);
        }
    }
}
