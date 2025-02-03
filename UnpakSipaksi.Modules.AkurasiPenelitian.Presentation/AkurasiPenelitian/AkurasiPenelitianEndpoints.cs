using Microsoft.AspNetCore.Routing;

namespace UnpakSipaksi.Modules.AkurasiPenelitian.Presentation.AkurasiPenelitian
{
    public static class AkurasiPenelitianEndpoints
    {
        public static void MapEndpoints(IEndpointRouteBuilder app)
        {
            CreateAkurasiPenelitian.MapEndpoint(app);
            UpdateAkurasiPenelitian.MapEndpoint(app);
            DeleteAkurasiPenelitian.MapEndpoint(app);
            GetAkurasiPenelitian.MapEndpoint(app);
            GetAllAkurasiPenelitian.MapEndpoint(app);
        }
    }
}
