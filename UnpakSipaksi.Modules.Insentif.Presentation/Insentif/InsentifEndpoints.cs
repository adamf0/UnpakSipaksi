using Microsoft.AspNetCore.Routing;

namespace UnpakSipaksi.Modules.Insentif.Presentation.Insentif
{
    public static class InsentifEndpoints
    {
        public static void MapEndpoints(IEndpointRouteBuilder app)
        {
            CreateInsentif.MapEndpoint(app);
            UpdateInsentif.MapEndpoint(app);
            DeleteInsentif.MapEndpoint(app);
            CreateReviewInsentif.MapEndpoint(app);
            CalculateSbuInsentif.MapEndpoint(app);
            GetInsentif.MapEndpoint(app);
            GetAllInsentif.MapEndpoint(app);
        }
    }
}
