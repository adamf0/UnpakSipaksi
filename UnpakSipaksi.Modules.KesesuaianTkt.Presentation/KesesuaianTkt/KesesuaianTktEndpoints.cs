using Microsoft.AspNetCore.Routing;

namespace UnpakSipaksi.Modules.KesesuaianTkt.Presentation.KesesuaianTkt
{
    public static class KesesuaianTktEndpoints
    {
        public static void MapEndpoints(IEndpointRouteBuilder app)
        {
            CreateKesesuaianTkt.MapEndpoint(app);
            UpdateKesesuaianTkt.MapEndpoint(app);
            DeleteKesesuaianTkt.MapEndpoint(app);
            GetKesesuaianTkt.MapEndpoint(app);
            GetAllKesesuaianTkt.MapEndpoint(app);
        }
    }
}
