using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.Substansi.Application.DeleteSubstansiInternal;
using UnpakSipaksi.Modules.Substansi.Presentation;

namespace UnpakSipaksi.Modules.Substansi.Presentation.Substansi
{
    internal class DeleteSubstansi
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapDelete("SubstansiInternal/{Uuid}", async (string Uuid, ISender sender) =>
            {
                Result result = await sender.Send(
                    new DeleteSubstansiInternalCommand(Uuid)
                );

                return result.Match(() => Results.Ok(), ApiResults.Problem);
            }).WithTags(Tags.Substansi);
        }
    }
}
