using Microsoft.AspNetCore.Routing;

namespace UnpakSipaksi.Modules.Pengumuman.Presentation.Pengumuman
{
    public static class PengumumanEndpoints
    {
        public static void MapEndpoints(IEndpointRouteBuilder app)
        {
            CreatePengumuman.MapEndpoint(app);
            UpdatePengumuman.MapEndpoint(app);
            DeletePengumuman.MapEndpoint(app);
            GetPengumuman.MapEndpoint(app);
            GetAllPengumuman.MapEndpoint(app);
        }
    }
}
