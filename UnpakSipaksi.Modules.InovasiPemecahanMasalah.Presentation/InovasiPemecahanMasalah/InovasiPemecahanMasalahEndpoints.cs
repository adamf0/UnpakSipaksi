using Microsoft.AspNetCore.Routing;

namespace UnpakSipaksi.Modules.InovasiPemecahanMasalah.Presentation.InovasiPemecahanMasalah
{
    public static class InovasiPemecahanMasalahEndpoints
    {
        public static void MapEndpoints(IEndpointRouteBuilder app)
        {
            CreateInovasiPemecahanMasalah.MapEndpoint(app);
            UpdateInovasiPemecahanMasalah.MapEndpoint(app);
            DeleteInovasiPemecahanMasalah.MapEndpoint(app);
            GetInovasiPemecahanMasalah.MapEndpoint(app);
            GetAllInovasiPemecahanMasalah.MapEndpoint(app);
        }
    }
}
