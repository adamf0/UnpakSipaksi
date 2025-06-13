using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.Administrasi.Application.DeleteAdministrasiInternal;

namespace UnpakSipaksi.Modules.Administrasi.Presentation.AdministrasiInternal
{
    internal class DeleteAdministrasiInternal
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapDelete("AdministrasiInternal/{Uuid}", async (string Uuid, ISender sender) =>
            {
                Result result = await sender.Send(
                    new DeleteAdministrasiInternalCommand(Uuid)
                );

                return result.Match(() => Results.Ok(), ApiResults.Problem);
            }).WithTags(Tags.Administrasi);
        }
    }
}
