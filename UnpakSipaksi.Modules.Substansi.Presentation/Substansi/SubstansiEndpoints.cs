using Microsoft.AspNetCore.Routing;

namespace UnpakSipaksi.Modules.Substansi.Presentation.Substansi
{
    public static class SubstansiEndpoints
    {
        public static void MapEndpoints(IEndpointRouteBuilder app)
        {
            CreateSubstansi.MapEndpoint(app);
            UpdateSubstansi.MapEndpoint(app);
            DeleteSubstansi.MapEndpoint(app);
            GetSubstansi.MapEndpoint(app);
            GetAllSubstansi.MapEndpoint(app);
        }
    }
}
