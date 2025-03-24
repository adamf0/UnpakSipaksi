using Microsoft.AspNetCore.Routing;

namespace UnpakSipaksi.Modules.RumpunIlmu1.Presentation.RumpunIlmu1
{
    public static class RumpunIlmu1Endpoints
    {
        public static void MapEndpoints(IEndpointRouteBuilder app)
        {
            CreateRumpunIlmu1.MapEndpoint(app);
            UpdateRumpunIlmu1.MapEndpoint(app);
            DeleteRumpunIlmu1.MapEndpoint(app);
            GetRumpunIlmu1.MapEndpoint(app);
            GetAllRumpunIlmu1.MapEndpoint(app);
        }
    }
}
