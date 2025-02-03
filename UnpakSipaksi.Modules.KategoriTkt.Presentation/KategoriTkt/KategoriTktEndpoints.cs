using Microsoft.AspNetCore.Routing;

namespace UnpakSipaksi.Modules.KategoriTkt.Presentation.KategoriTkt
{
    public static class KategoriTktEndpoints
    {
        public static void MapEndpoints(IEndpointRouteBuilder app)
        {
            CreateKategoriTkt.MapEndpoint(app);
            UpdateKategoriTkt.MapEndpoint(app);
            DeleteKategoriTkt.MapEndpoint(app);
            GetKategoriTkt.MapEndpoint(app);
            GetAllKategoriTkt.MapEndpoint(app);
        }
    }
}
