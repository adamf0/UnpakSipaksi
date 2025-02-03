using Microsoft.AspNetCore.Routing;

namespace UnpakSipaksi.Modules.FokusPenelitian.Presentation.FokusPenelitian
{
    public static class FokusPenelitianEndpoints
    {
        public static void MapEndpoints(IEndpointRouteBuilder app)
        {
            CreateFokusPenelitian.MapEndpoint(app);
            UpdateFokusPenelitian.MapEndpoint(app);
            DeleteFokusPenelitian.MapEndpoint(app);
            GetFokusPenelitian.MapEndpoint(app);
            GetAllFokusPenelitian.MapEndpoint(app);
        }
    }
}
