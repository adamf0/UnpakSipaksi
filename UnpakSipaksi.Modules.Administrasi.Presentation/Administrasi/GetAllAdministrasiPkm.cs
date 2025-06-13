using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.Administrasi.Application.GetAdministrasiPkm;
using UnpakSipaksi.Modules.Administrasi.Application.GetAllAdministrasiPkm;

namespace UnpakSipaksi.Modules.Administrasi.Presentation.AdministrasiPkm
{
    internal class GetAllAdministrasiPkm
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("AdministrasiPkm", async (ISender sender) =>
            {
                Result<List<AdministrasiPkmResponse>> result = await sender.Send(new GetAllAdministrasiPkmQuery());

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.Administrasi);
        }
    }
}
