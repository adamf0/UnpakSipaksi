using Microsoft.AspNetCore.Routing;

namespace UnpakSipaksi.Modules.UrgensiPenelitian.Presentation.UrgensiPenelitian
{
    public static class UrgensiPenelitianEndpoints
    {
        public static void MapEndpoints(IEndpointRouteBuilder app)
        {
            CreateUrgensiPenelitian.MapEndpoint(app);
            UpdateUrgensiPenelitian.MapEndpoint(app);
            DeleteUrgensiPenelitian.MapEndpoint(app);
            GetUrgensiPenelitian.MapEndpoint(app);
            GetAllUrgensiPenelitian.MapEndpoint(app);
        }
    }
}
