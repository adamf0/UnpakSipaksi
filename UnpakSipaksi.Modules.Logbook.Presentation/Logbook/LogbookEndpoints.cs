using Microsoft.AspNetCore.Routing;

namespace UnpakSipaksi.Modules.Logbook.Presentation.Logbook
{
    public static class LogbookEndpoints
    {
        public static void MapEndpoints(IEndpointRouteBuilder app)
        {
            CreateLogbook.MapEndpoint(app);
            UpdateLogbook.MapEndpoint(app);
            DeleteLogbook.MapEndpoint(app);
            GetLogbook.MapEndpoint(app);
            GetAllLogbook.MapEndpoint(app);
        }
    }
}
