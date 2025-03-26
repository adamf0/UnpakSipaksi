using Microsoft.AspNetCore.Routing;

namespace UnpakSipaksi.Modules.SotaKebaharuan.Presentation.SotaKebaharuan
{
    public static class SotaKebaharuanEndpoints
    {
        public static void MapEndpoints(IEndpointRouteBuilder app)
        {
            CreateSotaKebaharuan.MapEndpoint(app);
            UpdateSotaKebaharuan.MapEndpoint(app);
            DeleteSotaKebaharuan.MapEndpoint(app);
            GetSotaKebaharuan.MapEndpoint(app);
            GetAllSotaKebaharuan.MapEndpoint(app);
        }
    }
}
