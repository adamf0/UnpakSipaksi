using Microsoft.AspNetCore.Routing;

namespace UnpakSipaksi.Modules.Satuan.Presentation.Satuan
{
    public static class SatuanEndpoints
    {
        public static void MapEndpoints(IEndpointRouteBuilder app)
        {
            CreateSatuan.MapEndpoint(app);
            UpdateSatuan.MapEndpoint(app);
            DeleteSatuan.MapEndpoint(app);
            GetSatuan.MapEndpoint(app);
            GetAllSatuan.MapEndpoint(app);
        }
    }
}
