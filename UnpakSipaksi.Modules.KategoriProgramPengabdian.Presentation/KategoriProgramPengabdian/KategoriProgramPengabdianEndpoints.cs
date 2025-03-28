using Microsoft.AspNetCore.Routing;

namespace UnpakSipaksi.Modules.KategoriProgramPengabdian.Presentation.KategoriProgramPengabdian
{
    public static class KategoriProgramPengabdianEndpoints
    {
        public static void MapEndpoints(IEndpointRouteBuilder app)
        {
            CreateKategoriProgramPengabdian.MapEndpoint(app);
            UpdateKategoriProgramPengabdian.MapEndpoint(app);
            DeleteKategoriProgramPengabdian.MapEndpoint(app);
            GetKategoriProgramPengabdian.MapEndpoint(app);
            GetAllKategoriProgramPengabdian.MapEndpoint(app);
        }
    }
}
