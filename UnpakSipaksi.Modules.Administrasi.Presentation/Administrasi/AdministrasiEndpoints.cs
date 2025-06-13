using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Modules.Administrasi.Presentation.AdministrasiInternal;
using UnpakSipaksi.Modules.Administrasi.Presentation.AdministrasiPkm;

namespace UnpakSipaksi.Modules.Administrasi.Presentation.Administrasi
{
    public static class AdministrasiEndpoints
    {
        public static void MapEndpoints(IEndpointRouteBuilder app)
        {
            CreateAdministrasiInternal.MapEndpoint(app);
            UpdateAdministrasiInternal.MapEndpoint(app);
            DeleteAdministrasiInternal.MapEndpoint(app);
            GetAdministrasiInternal.MapEndpoint(app);
            GetAllAdministrasiInternal.MapEndpoint(app);

            CreateAdministrasiPkm.MapEndpoint(app);
            UpdateAdministrasiPkm.MapEndpoint(app);
            DeleteAdministrasiPkm.MapEndpoint(app);
            GetAdministrasiPkm.MapEndpoint(app);
            GetAllAdministrasiPkm.MapEndpoint(app);
        }
    }
}
