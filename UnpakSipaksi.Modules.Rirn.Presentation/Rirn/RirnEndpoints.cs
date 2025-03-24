using Microsoft.AspNetCore.Routing;

namespace UnpakSipaksi.Modules.Rirn.Presentation.Rirn
{
    public static class RirnEndpoints
    {
        public static void MapEndpoints(IEndpointRouteBuilder app)
        {
            CreateRirn.MapEndpoint(app);
            UpdateRirn.MapEndpoint(app);
            DeleteRirn.MapEndpoint(app);
            GetRirn.MapEndpoint(app);
            GetAllRirn.MapEndpoint(app);
        }
    }
}
