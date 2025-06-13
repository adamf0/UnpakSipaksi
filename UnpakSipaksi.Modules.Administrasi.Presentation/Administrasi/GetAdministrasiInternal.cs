using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.Administrasi.Application.GetAdministrasiInternal;
using UnpakSipaksi.Modules.Administrasi.Application.GetAdministrasiPkm;

namespace UnpakSipaksi.Modules.Administrasi.Presentation.AdministrasiInternal
{
    internal static class GetAdministrasiInternal
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("AdministrasiInternal/{Uuid}", async (Guid Uuid, ISender sender) =>
            {
                Result<AdministrasiInternalResponse> result = await sender.Send(new GetAdministrasiInternalQuery(Uuid));

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.Administrasi);
        }
    }
}
