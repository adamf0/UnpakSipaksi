using Microsoft.AspNetCore.Routing;

namespace UnpakSipaksi.Modules.KetajamanAnalisis.Presentation.KetajamanAnalisis
{
    public static class KetajamanAnalisisEndpoints
    {
        public static void MapEndpoints(IEndpointRouteBuilder app)
        {
            CreateKetajamanAnalisis.MapEndpoint(app);
            UpdateKetajamanAnalisis.MapEndpoint(app);
            DeleteKetajamanAnalisis.MapEndpoint(app);
            GetKetajamanAnalisis.MapEndpoint(app);
            GetAllKetajamanAnalisis.MapEndpoint(app);
        }
    }
}
