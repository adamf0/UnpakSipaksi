using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Text.Encodings.Web;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.RoadmapPenelitian.Application.CreateRoadmapPenelitian;
using UnpakSipaksi.Modules.RoadmapPenelitian.Presentation;

namespace UnpakSipaksi.Modules.RoadmapPenelitian.Presentation.RoadmapPenelitian
{
    internal static class CreateRoadmapPenelitian
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("RoadmapPenelitian", async (CreateRoadmapPenelitianRequest request, ISender sender) =>
            {
                Result<Guid> result = await sender.Send(new CreateRoadmapPenelitianCommand(
                    request.Nama,
                    int.Parse(request.BobotSkor)
                    )
                );

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.RoadmapPenelitian);
        }

        internal sealed class CreateRoadmapPenelitianRequest
        {
            public string Nama { get; set; }
            public string BobotSkor { get; set; }
        }
    }
}
