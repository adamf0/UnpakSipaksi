using Microsoft.AspNetCore.Routing;

namespace UnpakSipaksi.Modules.RumpunIlmu2.Presentation.RumpunIlmu2
{
    public static class RumpunIlmu2Endpoints
    {
        public static void MapEndpoints(IEndpointRouteBuilder app)
        {
            CreateRumpunIlmu2.MapEndpoint(app);
            UpdateRumpunIlmu2.MapEndpoint(app);
            DeleteRumpunIlmu2.MapEndpoint(app);
            GetRumpunIlmu2.MapEndpoint(app);
            GetAllRumpunIlmu2.MapEndpoint(app);
        }
    }
}
