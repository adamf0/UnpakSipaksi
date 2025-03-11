using Microsoft.AspNetCore.Routing;

namespace UnpakSipaksi.Modules.KewajaranTahapanTarget.Presentation.KewajaranTahapanTarget
{
    public static class KewajaranTahapanTargetEndpoints
    {
        public static void MapEndpoints(IEndpointRouteBuilder app)
        {
            CreateKewajaranTahapanTarget.MapEndpoint(app);
            UpdateKewajaranTahapanTarget.MapEndpoint(app);
            DeleteKewajaranTahapanTarget.MapEndpoint(app);
            GetKewajaranTahapanTarget.MapEndpoint(app);
            GetAllKewajaranTahapanTarget.MapEndpoint(app);
        }
    }
}
