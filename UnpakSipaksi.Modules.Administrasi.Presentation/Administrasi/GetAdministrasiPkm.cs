using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.Administrasi.Application.GetAdministrasiPkm;

namespace UnpakSipaksi.Modules.Administrasi.Presentation.AdministrasiPkm
{
    internal static class GetAdministrasiPkm
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("AdministrasiPkm/{Uuid}", async (Guid Uuid, ISender sender) =>
            {
                Result<AdministrasiPkmResponse> result = await sender.Send(new GetAdministrasiPkmQuery(Uuid));

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.Administrasi);
        }
    }
}
