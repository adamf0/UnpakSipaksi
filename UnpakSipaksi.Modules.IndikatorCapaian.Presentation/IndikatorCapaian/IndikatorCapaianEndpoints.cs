using Microsoft.AspNetCore.Routing;

namespace UnpakSipaksi.Modules.IndikatorCapaian.Presentation.IndikatorCapaian
{
    public static class IndikatorCapaianEndpoints
    {
        public static void MapEndpoints(IEndpointRouteBuilder app)
        {
            CreateIndikatorCapaian.MapEndpoint(app);
            UpdateIndikatorCapaian.MapEndpoint(app);
            DeleteIndikatorCapaian.MapEndpoint(app);
            GetIndikatorCapaian.MapEndpoint(app);
            GetAllIndikatorCapaian.MapEndpoint(app);
        }
    }
}
