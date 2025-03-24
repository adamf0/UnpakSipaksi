using Microsoft.AspNetCore.Routing;

namespace UnpakSipaksi.Modules.RelevansiProdukKepentinganNasional.Presentation.RelevansiProdukKepentinganNasional
{
    public static class RelevansiProdukKepentinganNasionalEndpoints
    {
        public static void MapEndpoints(IEndpointRouteBuilder app)
        {
            CreateRelevansiProdukKepentinganNasional.MapEndpoint(app);
            UpdateRelevansiProdukKepentinganNasional.MapEndpoint(app);
            DeleteRelevansiProdukKepentinganNasional.MapEndpoint(app);
            GetRelevansiProdukKepentinganNasional.MapEndpoint(app);
            GetAllRelevansiProdukKepentinganNasional.MapEndpoint(app);
        }
    }
}
