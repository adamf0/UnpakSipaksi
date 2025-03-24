using Microsoft.AspNetCore.Routing;

namespace UnpakSipaksi.Modules.PeningkatanKeberdayaanMitra.Presentation.PeningkatanKeberdayaanMitra
{
    public static class PeningkatanKeberdayaanMitraEndpoints
    {
        public static void MapEndpoints(IEndpointRouteBuilder app)
        {
            CreatePeningkatanKeberdayaanMitra.MapEndpoint(app);
            UpdatePeningkatanKeberdayaanMitra.MapEndpoint(app);
            DeletePeningkatanKeberdayaanMitra.MapEndpoint(app);
            GetPeningkatanKeberdayaanMitra.MapEndpoint(app);
            GetAllPeningkatanKeberdayaanMitra.MapEndpoint(app);
        }
    }
}
