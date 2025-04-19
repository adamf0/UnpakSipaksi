using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.Roadmap.Application.GetRoadmap;
using UnpakSipaksi.Modules.Roadmap.Presentation;
using UnpakSipaksi.Modules.Roadmap.Application.GetAllRoadmap;

namespace UnpakSipaksi.Modules.Roadmap.Presentation.Roadmap
{
    internal class GetAllRoadmap
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("Roadmap", async (ISender sender) =>
            {
                Result<List<RoadmapResponse>> result = await sender.Send(new GetAllRoadmapQuery());

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.Roadmap);
        }
    }
}
