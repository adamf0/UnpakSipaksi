using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.Roadmap.Application.GetRoadmap;
using UnpakSipaksi.Modules.Roadmap.Presentation;

namespace UnpakSipaksi.Modules.Roadmap.Presentation.Roadmap
{
    internal static class GetRoadmap
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("Roadmap/{id}", async (Guid id, ISender sender) =>
            {
                Result<RoadmapResponse> result = await sender.Send(new GetRoadmapQuery(id));

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.Roadmap);
        }
    }
}
