using Microsoft.AspNetCore.Routing;

namespace UnpakSipaksi.Modules.KualitasKuantitasPublikasiProsiding.Presentation.KualitasKuantitasPublikasiProsiding
{
    public static class KualitasKuantitasPublikasiProsidingEndpoints
    {
        public static void MapEndpoints(IEndpointRouteBuilder app)
        {
            CreateKualitasKuantitasPublikasiProsiding.MapEndpoint(app);
            UpdateKualitasKuantitasPublikasiProsiding.MapEndpoint(app);
            DeleteKualitasKuantitasPublikasiProsiding.MapEndpoint(app);
            GetKualitasKuantitasPublikasiProsiding.MapEndpoint(app);
            GetAllKualitasKuantitasPublikasiProsiding.MapEndpoint(app);
        }
    }
}
