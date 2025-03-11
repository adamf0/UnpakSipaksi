using Microsoft.AspNetCore.Routing;

namespace UnpakSipaksi.Modules.KualitasKuantitasPublikasiJurnalIlmiah.Presentation.KualitasKuantitasPublikasiJurnalIlmiah
{
    public static class KualitasKuantitasPublikasiJurnalIlmiahEndpoints
    {
        public static void MapEndpoints(IEndpointRouteBuilder app)
        {
            CreateKualitasKuantitasPublikasiJurnalIlmiah.MapEndpoint(app);
            UpdateKualitasKuantitasPublikasiJurnalIlmiah.MapEndpoint(app);
            DeleteKualitasKuantitasPublikasiJurnalIlmiah.MapEndpoint(app);
            GetKualitasKuantitasPublikasiJurnalIlmiah.MapEndpoint(app);
            GetAllKualitasKuantitasPublikasiJurnalIlmiah.MapEndpoint(app);
        }
    }
}
