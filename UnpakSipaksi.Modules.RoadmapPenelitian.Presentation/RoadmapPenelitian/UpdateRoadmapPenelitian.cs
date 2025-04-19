using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Text.Encodings.Web;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.RoadmapPenelitian.Application.UpdateRoadmapPenelitian;
using UnpakSipaksi.Modules.RoadmapPenelitian.Presentation;

namespace UnpakSipaksi.Modules.RoadmapPenelitian.Presentation.RoadmapPenelitian
{
    internal static class UpdateRoadmapPenelitian
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPut("RoadmapPenelitian", async (UpdateRoadmapPenelitianRequest request, ISender sender) =>
            {
                Result result = await sender.Send(new UpdateRoadmapPenelitianCommand(
                    request.Id,
                    request.Nama,
                    int.Parse(request.BobotSkor)
                    )
                );

                return result.Match(() => Results.Ok(), ApiResults.Problem);
            }).WithTags(Tags.RoadmapPenelitian);
        }

        internal sealed class UpdateRoadmapPenelitianRequest
        {
            public Guid Id { get; set; }
            public string Nama { get; set; }
            public string BobotSkor { get; set; }
        }
    }
}
