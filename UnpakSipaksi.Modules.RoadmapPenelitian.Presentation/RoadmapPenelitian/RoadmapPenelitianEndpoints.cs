using Microsoft.AspNetCore.Routing;

namespace UnpakSipaksi.Modules.RoadmapPenelitian.Presentation.RoadmapPenelitian
{
    public static class RoadmapPenelitianEndpoints
    {
        public static void MapEndpoints(IEndpointRouteBuilder app)
        {
            CreateRoadmapPenelitian.MapEndpoint(app);
            UpdateRoadmapPenelitian.MapEndpoint(app);
            DeleteRoadmapPenelitian.MapEndpoint(app);
            GetRoadmapPenelitian.MapEndpoint(app);
            GetAllRoadmapPenelitian.MapEndpoint(app);
        }
    }
}
