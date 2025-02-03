using Microsoft.AspNetCore.Routing;

namespace UnpakSipaksi.Modules.TopikPenelitian.Presentation.TopikPenelitian
{
    public static class TopikPenelitianEndpoints
    {
        public static void MapEndpoints(IEndpointRouteBuilder app)
        {
            CreateTopikPenelitian.MapEndpoint(app);
            UpdateTopikPenelitian.MapEndpoint(app);
            DeleteTopikPenelitian.MapEndpoint(app);
            GetTopikPenelitian.MapEndpoint(app);
            GetAllTopikPenelitian.MapEndpoint(app);
        }
    }
}
