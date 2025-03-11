using Microsoft.AspNetCore.Routing;

namespace UnpakSipaksi.Modules.KredibilitasMitraDukungan.Presentation.KredibilitasMitraDukungan
{
    public static class KredibilitasMitraDukunganEndpoints
    {
        public static void MapEndpoints(IEndpointRouteBuilder app)
        {
            CreateKredibilitasMitraDukungan.MapEndpoint(app);
            UpdateKredibilitasMitraDukungan.MapEndpoint(app);
            DeleteKredibilitasMitraDukungan.MapEndpoint(app);
            GetKredibilitasMitraDukungan.MapEndpoint(app);
            GetAllKredibilitasMitraDukungan.MapEndpoint(app);
        }
    }
}
