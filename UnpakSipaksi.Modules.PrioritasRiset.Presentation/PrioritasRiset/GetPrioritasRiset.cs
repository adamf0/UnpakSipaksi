using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.PrioritasRiset.Presentation;
using UnpakSipaksi.Modules.PrioritasRiset.Application.GetPrioritasRiset;

namespace UnpakSipaksi.Modules.PrioritasRiset.Presentation.PrioritasRiset
{
    internal static class GetPrioritasRiset
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("PrioritasRiset/{id}", async (Guid id, ISender sender) =>
            {
                Result<PrioritasRisetResponse> result = await sender.Send(new GetPrioritasRisetQuery(id));

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.PrioritasRiset);
        }
    }
}
