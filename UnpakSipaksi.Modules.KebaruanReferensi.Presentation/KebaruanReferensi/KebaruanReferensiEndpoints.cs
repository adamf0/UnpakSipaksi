using Microsoft.AspNetCore.Routing;

namespace UnpakSipaksi.Modules.KebaruanReferensi.Presentation.KebaruanReferensi
{
    public static class KebaruanReferensiEndpoints
    {
        public static void MapEndpoints(IEndpointRouteBuilder app)
        {
            CreateKebaruanReferensi.MapEndpoint(app);
            UpdateKebaruanReferensi.MapEndpoint(app);
            DeleteKebaruanReferensi.MapEndpoint(app);
            GetKebaruanReferensi.MapEndpoint(app);
            GetAllKebaruanReferensi.MapEndpoint(app);
        }
    }
}
