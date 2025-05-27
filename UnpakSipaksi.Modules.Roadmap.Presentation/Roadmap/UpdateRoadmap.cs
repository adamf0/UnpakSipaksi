using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.Roadmap.Application.UpdateRoadmap;

namespace UnpakSipaksi.Modules.Roadmap.Presentation.Roadmap
{
    internal static class UpdateRoadmap
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPut("Roadmap", async (UpdateRoadmapRequest request, ISender sender) =>
            {
                Result result = await sender.Send(new UpdateRoadmapCommand(
                    request.Id,
                    request.Nidn,
                    request.Link
                    )
                );

                return result.Match(() => Results.Ok(), ApiResults.Problem);
            }).WithTags(Tags.Roadmap);
        }

        internal sealed class UpdateRoadmapRequest
        {
            public string Id { get; set; }
            public string Nidn { get; set; }
            public string Link { get; set; }
        }
    }
}
