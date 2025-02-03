using Microsoft.AspNetCore.Routing;

namespace UnpakSipaksi.Modules.JenisPublikasi.Presentation.JenisPublikasi
{
    public static class JenisPublikasiEndpoints
    {
        public static void MapEndpoints(IEndpointRouteBuilder app)
        {
            CreateJenisPublikasi.MapEndpoint(app);
            UpdateJenisPublikasi.MapEndpoint(app);
            DeleteJenisPublikasi.MapEndpoint(app);
            GetJenisPublikasi.MapEndpoint(app);
            GetAllJenisPublikasi.MapEndpoint(app);
        }
    }
}
