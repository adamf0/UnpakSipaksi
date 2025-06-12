using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.KewajaranTahapanTarget.Application.DeleteKewajaranTahapanTarget;

namespace UnpakSipaksi.Modules.KewajaranTahapanTarget.Presentation.KewajaranTahapanTarget
{
    internal class DeleteKewajaranTahapanTarget
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapDelete("KewajaranTahapanTarget/{Uuid}", async (string Uuid, ISender sender) =>
            {
                Result result = await sender.Send(
                    new DeleteKewajaranTahapanTargetCommand(Uuid)
                );

                return result.Match(() => Results.Ok(), ApiResults.Problem);
            }).WithTags(Tags.KewajaranTahapanTarget);
        }
    }
}
