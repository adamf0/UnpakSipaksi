using Microsoft.AspNetCore.Routing;

namespace UnpakSipaksi.Modules.KelompokRab.Presentation.KelompokRab
{
    public static class KelompokRabEndpoints
    {
        public static void MapEndpoints(IEndpointRouteBuilder app)
        {
            CreateKelompokRab.MapEndpoint(app);
            UpdateKelompokRab.MapEndpoint(app);
            DeleteKelompokRab.MapEndpoint(app);
            GetKelompokRab.MapEndpoint(app);
            GetAllKelompokRab.MapEndpoint(app);
        }
    }
}
