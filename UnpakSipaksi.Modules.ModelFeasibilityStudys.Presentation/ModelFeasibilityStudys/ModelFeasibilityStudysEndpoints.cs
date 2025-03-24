using Microsoft.AspNetCore.Routing;

namespace UnpakSipaksi.Modules.ModelFeasibilityStudys.Presentation.ModelFeasibilityStudys
{
    public static class ModelFeasibilityStudysEndpoints
    {
        public static void MapEndpoints(IEndpointRouteBuilder app)
        {
            CreateModelFeasibilityStudys.MapEndpoint(app);
            UpdateModelFeasibilityStudys.MapEndpoint(app);
            DeleteModelFeasibilityStudys.MapEndpoint(app);
            GetModelFeasibilityStudys.MapEndpoint(app);
            GetAllModelFeasibilityStudys.MapEndpoint(app);
        }
    }
}
