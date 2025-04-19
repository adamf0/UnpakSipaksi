using Microsoft.AspNetCore.Routing;

namespace UnpakSipaksi.Modules.Metode.Presentation.Metode
{
    public static class MetodeEndpoints
    {
        public static void MapEndpoints(IEndpointRouteBuilder app)
        {
            CreateMetode.MapEndpoint(app);
            UpdateMetode.MapEndpoint(app);
            DeleteMetode.MapEndpoint(app);
            GetMetode.MapEndpoint(app);
            GetAllMetode.MapEndpoint(app);
        }
    }
}
