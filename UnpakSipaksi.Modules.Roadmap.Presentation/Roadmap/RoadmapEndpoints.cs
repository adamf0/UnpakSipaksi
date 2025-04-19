using Microsoft.AspNetCore.Routing;

namespace UnpakSipaksi.Modules.Roadmap.Presentation.Roadmap
{
    public static class RoadmapEndpoints
    {
        public static void MapEndpoints(IEndpointRouteBuilder app)
        {
            CreateRoadmap.MapEndpoint(app);
            UpdateRoadmap.MapEndpoint(app);
            DeleteRoadmap.MapEndpoint(app);
            GetRoadmap.MapEndpoint(app);
            GetAllRoadmap.MapEndpoint(app);
        }
    }
}
