using Microsoft.AspNetCore.Routing;

namespace UnpakSipaksi.Modules.VideoKegiatan.Presentation.VideoKegiatan
{
    public static class VideoKegiatanEndpoints
    {
        public static void MapEndpoints(IEndpointRouteBuilder app)
        {
            CreateVideoKegiatan.MapEndpoint(app);
            UpdateVideoKegiatan.MapEndpoint(app);
            DeleteVideoKegiatan.MapEndpoint(app);
            GetVideoKegiatan.MapEndpoint(app);
            GetAllVideoKegiatan.MapEndpoint(app);
        }
    }
}
