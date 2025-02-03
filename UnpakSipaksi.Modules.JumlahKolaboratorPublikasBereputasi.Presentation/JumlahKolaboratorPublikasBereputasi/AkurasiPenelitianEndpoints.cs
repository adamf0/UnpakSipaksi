using Microsoft.AspNetCore.Routing;

namespace UnpakSipaksi.Modules.JumlahKolaboratorPublikasBereputasi.Presentation.JumlahKolaboratorPublikasBereputasi
{
    public static class JumlahKolaboratorPublikasBereputasiEndpoints
    {
        public static void MapEndpoints(IEndpointRouteBuilder app)
        {
            CreateJumlahKolaboratorPublikasBereputasi.MapEndpoint(app);
            UpdateJumlahKolaboratorPublikasBereputasi.MapEndpoint(app);
            DeleteJumlahKolaboratorPublikasBereputasi.MapEndpoint(app);
            GetJumlahKolaboratorPublikasBereputasi.MapEndpoint(app);
            GetAllJumlahKolaboratorPublikasBereputasi.MapEndpoint(app);
        }
    }
}
