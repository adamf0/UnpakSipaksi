using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.PrioritasRiset.Presentation;
using UnpakSipaksi.Modules.PrioritasRiset.Application.DeletePrioritasRiset;

namespace UnpakSipaksi.Modules.PrioritasRiset.Presentation.PrioritasRiset
{
    internal class DeletePrioritasRiset
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapDelete("PrioritasRiset/{Uuid}", async (string Uuid, ISender sender) =>
            {
                Result result = await sender.Send(
                    new DeletePrioritasRisetCommand(Uuid)
                );

                return result.Match(() => Results.Ok(), ApiResults.Problem);
            }).WithTags(Tags.PrioritasRiset);
        }
    }
}
