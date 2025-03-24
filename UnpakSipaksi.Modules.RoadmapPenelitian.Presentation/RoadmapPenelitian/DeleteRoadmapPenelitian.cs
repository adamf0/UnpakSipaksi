using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.RoadmapPenelitian.Application.DeleteRoadmapPenelitian;
using UnpakSipaksi.Modules.RoadmapPenelitian.Presentation;

namespace UnpakSipaksi.Modules.RoadmapPenelitian.Presentation.RoadmapPenelitian
{
    internal class DeleteRoadmapPenelitian
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapDelete("RoadmapPenelitian/{id}", async (Guid id, ISender sender) =>
            {
                Result result = await sender.Send(
                    new DeleteRoadmapPenelitianCommand(id)
                );

                return result.Match(() => Results.Ok(), ApiResults.Problem);
            }).WithTags(Tags.RoadmapPenelitian);
        }
    }
}
