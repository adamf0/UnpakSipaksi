using Microsoft.AspNetCore.Routing;

namespace UnpakSipaksi.Modules.RumpunIlmu3.Presentation.RumpunIlmu3
{
    public static class RumpunIlmu3Endpoints
    {
        public static void MapEndpoints(IEndpointRouteBuilder app)
        {
            CreateRumpunIlmu3.MapEndpoint(app);
            UpdateRumpunIlmu3.MapEndpoint(app);
            DeleteRumpunIlmu3.MapEndpoint(app);
            GetRumpunIlmu3.MapEndpoint(app);
            GetAllRumpunIlmu3.MapEndpoint(app);
        }
    }
}
