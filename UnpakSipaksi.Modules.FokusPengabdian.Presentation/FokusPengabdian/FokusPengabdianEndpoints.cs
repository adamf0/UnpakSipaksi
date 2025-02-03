using Microsoft.AspNetCore.Routing;

namespace UnpakSipaksi.Modules.FokusPengabdian.Presentation.FokusPengabdian
{
    public static class FokusPengabdianEndpoints
    {
        public static void MapEndpoints(IEndpointRouteBuilder app)
        {
            CreateFokusPengabdian.MapEndpoint(app);
            UpdateFokusPengabdian.MapEndpoint(app);
            DeleteFokusPengabdian.MapEndpoint(app);
            GetFokusPengabdian.MapEndpoint(app);
            GetAllFokusPengabdian.MapEndpoint(app);
        }
    }
}
