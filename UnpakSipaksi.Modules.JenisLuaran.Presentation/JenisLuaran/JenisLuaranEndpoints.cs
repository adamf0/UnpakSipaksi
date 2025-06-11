using Microsoft.AspNetCore.Routing;

namespace UnpakSipaksi.Modules.JenisLuaran.Presentation.JenisLuaran
{
    public static class JenisLuaranEndpoints
    {
        public static void MapEndpoints(IEndpointRouteBuilder app)
        {
            CreateJenisLuaran.MapEndpoint(app);
            UpdateJenisLuaran.MapEndpoint(app);
            DeleteJenisLuaran.MapEndpoint(app);
            GetJenisLuaran.MapEndpoint(app);
            GetAllJenisLuaran.MapEndpoint(app);
        }
    }
}
