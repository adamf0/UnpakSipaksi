using Microsoft.AspNetCore.Routing;

namespace UnpakSipaksi.Modules.KesesuaianWaktuRabLuaranFasilitas.Presentation.KesesuaianWaktuRabLuaranFasilitas
{
    public static class KesesuaianWaktuRabLuaranFasilitasEndpoints
    {
        public static void MapEndpoints(IEndpointRouteBuilder app)
        {
            CreateKesesuaianWaktuRabLuaranFasilitas.MapEndpoint(app);
            UpdateKesesuaianWaktuRabLuaranFasilitas.MapEndpoint(app);
            DeleteKesesuaianWaktuRabLuaranFasilitas.MapEndpoint(app);
            GetKesesuaianWaktuRabLuaranFasilitas.MapEndpoint(app);
            GetAllKesesuaianWaktuRabLuaranFasilitas.MapEndpoint(app);
        }
    }
}
