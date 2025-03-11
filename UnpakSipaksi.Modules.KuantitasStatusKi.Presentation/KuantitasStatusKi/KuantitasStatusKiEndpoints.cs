using Microsoft.AspNetCore.Routing;

namespace UnpakSipaksi.Modules.KuantitasStatusKi.Presentation.KuantitasStatusKi
{
    public static class KuantitasStatusKiEndpoints
    {
        public static void MapEndpoints(IEndpointRouteBuilder app)
        {
            CreateKuantitasStatusKi.MapEndpoint(app);
            UpdateKuantitasStatusKi.MapEndpoint(app);
            DeleteKuantitasStatusKi.MapEndpoint(app);
            GetKuantitasStatusKi.MapEndpoint(app);
            GetAllKuantitasStatusKi.MapEndpoint(app);
        }
    }
}
