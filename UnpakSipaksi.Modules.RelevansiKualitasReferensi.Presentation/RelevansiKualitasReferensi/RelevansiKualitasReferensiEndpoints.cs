using Microsoft.AspNetCore.Routing;

namespace UnpakSipaksi.Modules.RelevansiKualitasReferensi.Presentation.RelevansiKualitasReferensi
{
    public static class RelevansiKualitasReferensiEndpoints
    {
        public static void MapEndpoints(IEndpointRouteBuilder app)
        {
            CreateRelevansiKualitasReferensil.MapEndpoint(app);
            UpdateRelevansiKualitasReferensi.MapEndpoint(app);
            DeleteRelevansiKualitasReferensi.MapEndpoint(app);
            GetRelevansiKualitasReferensi.MapEndpoint(app);
            GetAllRelevansiKualitasReferensi.MapEndpoint(app);
        }
    }
}
