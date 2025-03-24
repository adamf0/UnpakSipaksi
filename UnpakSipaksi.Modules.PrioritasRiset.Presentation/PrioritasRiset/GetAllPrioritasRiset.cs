using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.PrioritasRiset.Application.GetPrioritasRiset;
using UnpakSipaksi.Modules.PrioritasRiset.Application.GetAllPrioritasRiset;
using UnpakSipaksi.Modules.PrioritasRiset.Presentation;

namespace UnpakSipaksi.Modules.PrioritasRiset.Presentation.PrioritasRiset
{
    internal class GetAllPrioritasRiset
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("PrioritasRiset", async (ISender sender) =>
            {
                Result<List<PrioritasRisetResponse>> result = await sender.Send(new GetAllPrioritasRisetQuery());

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.PrioritasRiset);
        }
    }
}
