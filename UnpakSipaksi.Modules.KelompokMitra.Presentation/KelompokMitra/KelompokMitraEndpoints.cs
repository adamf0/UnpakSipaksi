using Microsoft.AspNetCore.Routing;

namespace UnpakSipaksi.Modules.KelompokMitra.Presentation.KelompokMitra
{
    public static class KelompokMitraEndpoints
    {
        public static void MapEndpoints(IEndpointRouteBuilder app)
        {
            CreateKelompokMitra.MapEndpoint(app);
            UpdateKelompokMitra.MapEndpoint(app);
            DeleteKelompokMitra.MapEndpoint(app);
            GetKelompokMitra.MapEndpoint(app);
            GetAllKelompokMitra.MapEndpoint(app);
        }
    }
}
