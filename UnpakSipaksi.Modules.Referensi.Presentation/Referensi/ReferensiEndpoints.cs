using Microsoft.AspNetCore.Routing;

namespace UnpakSipaksi.Modules.Referensi.Presentation.Referensi
{
    public static class ReferensiEndpoints
    {
        public static void MapEndpoints(IEndpointRouteBuilder app)
        {
            CreateReferensi.MapEndpoint(app);
            UpdateReferensi.MapEndpoint(app);
            DeleteReferensi.MapEndpoint(app);
            GetReferensi.MapEndpoint(app);
            GetAllReferensi.MapEndpoint(app);
        }
    }
}
