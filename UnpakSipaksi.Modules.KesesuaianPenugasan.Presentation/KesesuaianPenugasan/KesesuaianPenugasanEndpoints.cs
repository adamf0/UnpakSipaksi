using Microsoft.AspNetCore.Routing;

namespace UnpakSipaksi.Modules.KesesuaianPenugasan.Presentation.KesesuaianPenugasan
{
    public static class KesesuaianPenugasanEndpoints
    {
        public static void MapEndpoints(IEndpointRouteBuilder app)
        {
            CreateKesesuaianPenugasan.MapEndpoint(app);
            UpdateKesesuaianPenugasan.MapEndpoint(app);
            DeleteKesesuaianPenugasan.MapEndpoint(app);
            GetKesesuaianPenugasan.MapEndpoint(app);
            GetAllKesesuaianPenugasan.MapEndpoint(app);
        }
    }
}
