using Microsoft.AspNetCore.Routing;

namespace UnpakSipaksi.Modules.TemaPenelitian.Presentation.TemaPenelitian
{
    public static class TemaPenelitianEndpoints
    {
        public static void MapEndpoints(IEndpointRouteBuilder app)
        {
            CreateTemaPenelitian.MapEndpoint(app);
            UpdateTemaPenelitian.MapEndpoint(app);
            DeleteTemaPenelitian.MapEndpoint(app);
            GetTemaPenelitian.MapEndpoint(app);
            GetAllTemaPenelitian.MapEndpoint(app);
        }
    }
}
