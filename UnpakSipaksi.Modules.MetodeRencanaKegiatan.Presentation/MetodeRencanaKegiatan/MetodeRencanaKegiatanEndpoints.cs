using Microsoft.AspNetCore.Routing;

namespace UnpakSipaksi.Modules.MetodeRencanaKegiatan.Presentation.MetodeRencanaKegiatan
{
    public static class MetodeRencanaKegiatanEndpoints
    {
        public static void MapEndpoints(IEndpointRouteBuilder app)
        {
            CreateMetodeRencanaKegiatan.MapEndpoint(app);
            UpdateMetodeRencanaKegiatan.MapEndpoint(app);
            DeleteMetodeRencanaKegiatan.MapEndpoint(app);
            GetMetodeRencanaKegiatan.MapEndpoint(app);
            GetAllMetodeRencanaKegiatan.MapEndpoint(app);
        }
    }
}
