using Microsoft.AspNetCore.Routing;

namespace UnpakSipaksi.Modules.SubstansiBobot.Presentation.SubstansiBobot
{
    public static class SubstansiBobotEndpoints
    {
        public static void MapEndpoints(IEndpointRouteBuilder app)
        {
            UpdateSubstansiBobot.MapEndpoint(app);
            GetSubstansiBobot.MapEndpoint(app);
        }
    }
}
