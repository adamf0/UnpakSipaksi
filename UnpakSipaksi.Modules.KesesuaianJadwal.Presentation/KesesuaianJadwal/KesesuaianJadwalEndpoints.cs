using Microsoft.AspNetCore.Routing;

namespace UnpakSipaksi.Modules.KesesuaianJadwal.Presentation.KesesuaianJadwal
{
    public static class KesesuaianJadwalEndpoints
    {
        public static void MapEndpoints(IEndpointRouteBuilder app)
        {
            CreateKesesuaianJadwal.MapEndpoint(app);
            UpdateKesesuaianJadwal.MapEndpoint(app);
            DeleteKesesuaianJadwal.MapEndpoint(app);
            GetKesesuaianJadwal.MapEndpoint(app);
            GetAllKesesuaianJadwal.MapEndpoint(app);
        }
    }
}
