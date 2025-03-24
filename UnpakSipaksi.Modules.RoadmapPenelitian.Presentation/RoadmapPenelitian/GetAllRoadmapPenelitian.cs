using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.RoadmapPenelitian.Application.GetAllRoadmapPenelitian;
using UnpakSipaksi.Modules.RoadmapPenelitian.Application.GetRoadmapPenelitian;
using UnpakSipaksi.Modules.RoadmapPenelitian.Presentation;

namespace UnpakSipaksi.Modules.RoadmapPenelitian.Presentation.RoadmapPenelitian
{
    internal class GetAllRoadmapPenelitian
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("RoadmapPenelitian", async (ISender sender) =>
            {
                Result<List<RoadmapPenelitianResponse>> result = await sender.Send(new GetAllRoadmapPenelitianQuery());

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.RoadmapPenelitian);
        }
    }
}
