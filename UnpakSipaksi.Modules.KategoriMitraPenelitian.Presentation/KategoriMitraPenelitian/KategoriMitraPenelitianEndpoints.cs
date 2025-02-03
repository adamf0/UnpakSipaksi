using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Modules.KategoriMitraPenelitian.Presentation.KategoriMitraPenelitian;

namespace UnpakSipaksi.Modules.KategoriMitraPenelitian.Presentation.KategoriMitraPenelitian
{
    public static class KategoriMitraPenelitianEndpoints
    {
        public static void MapEndpoints(IEndpointRouteBuilder app)
        {
            CreateKategoriMitraPenelitian.MapEndpoint(app);
            UpdateKategoriMitraPenelitian.MapEndpoint(app);
            DeleteKategoriMitraPenelitian.MapEndpoint(app);
            GetKategoriMitraPenelitian.MapEndpoint(app);
            GetAllKategoriMitraPenelitian.MapEndpoint(app);
        }
    }
}
