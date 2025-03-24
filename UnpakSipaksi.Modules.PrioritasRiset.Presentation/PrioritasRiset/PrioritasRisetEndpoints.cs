using Microsoft.AspNetCore.Routing;

namespace UnpakSipaksi.Modules.PrioritasRiset.Presentation.PrioritasRiset
{
    public static class PrioritasRisetEndpoints
    {
        public static void MapEndpoints(IEndpointRouteBuilder app)
        {
            CreatePrioritasRiset.MapEndpoint(app);
            UpdatePrioritasRiset.MapEndpoint(app);
            DeletePrioritasRiset.MapEndpoint(app);
            GetPrioritasRiset.MapEndpoint(app);
            GetAllPrioritasRiset.MapEndpoint(app);
        }
    }
}
