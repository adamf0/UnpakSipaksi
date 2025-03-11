using Microsoft.AspNetCore.Routing;

namespace UnpakSipaksi.Modules.KetajamanPerumusanMasalah.Presentation.KetajamanPerumusanMasalah
{
    public static class KetajamanPerumusanMasalahEndpoints
    {
        public static void MapEndpoints(IEndpointRouteBuilder app)
        {
            CreateKetajamanPerumusanMasalah.MapEndpoint(app);
            UpdateKetajamanPerumusanMasalah.MapEndpoint(app);
            DeleteKetajamanPerumusanMasalah.MapEndpoint(app);
            GetKetajamanPerumusanMasalah.MapEndpoint(app);
            GetAllKetajamanPerumusanMasalah.MapEndpoint(app);
        }
    }
}
