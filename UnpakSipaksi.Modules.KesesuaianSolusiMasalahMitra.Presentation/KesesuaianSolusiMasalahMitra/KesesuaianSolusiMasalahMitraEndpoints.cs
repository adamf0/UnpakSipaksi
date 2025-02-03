using Microsoft.AspNetCore.Routing;

namespace UnpakSipaksi.Modules.KesesuaianSolusiMasalahMitra.Presentation.KesesuaianSolusiMasalahMitra
{
    public static class KesesuaianSolusiMasalahMitraEndpoints
    {
        public static void MapEndpoints(IEndpointRouteBuilder app)
        {
            CreateKesesuaianSolusiMasalahMitra.MapEndpoint(app);
            UpdateKesesuaianSolusiMasalahMitra.MapEndpoint(app);
            DeleteKesesuaianSolusiMasalahMitra.MapEndpoint(app);
            GetKesesuaianSolusiMasalahMitra.MapEndpoint(app);
            GetAllKesesuaianSolusiMasalahMitra.MapEndpoint(app);
        }
    }
}
