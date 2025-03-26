using Microsoft.AspNetCore.Routing;

namespace UnpakSipaksi.Modules.RumusanPrioritasMitra.Presentation.RumusanPrioritasMitra
{
    public static class RumusanPrioritasMitraEndpoints
    {
        public static void MapEndpoints(IEndpointRouteBuilder app)
        {
            CreateRumusanPrioritasMitra.MapEndpoint(app);
            UpdateRumusanPrioritasMitra.MapEndpoint(app);
            DeleteRumusanPrioritasMitra.MapEndpoint(app);
            GetRumusanPrioritasMitra.MapEndpoint(app);
            GetAllRumusanPrioritasMitra.MapEndpoint(app);
        }
    }
}
