using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.Roadmap.Application.CreateRoadmap;

namespace UnpakSipaksi.Modules.Roadmap.Presentation.Roadmap
{
    internal static class CreateRoadmap
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("Roadmap", async (CreateRoadmapRequest request, ISender sender) =>
            {
                Result<Guid> result = await sender.Send(new CreateRoadmapCommand(
                    request.Nidn,
                    request.Link
                    )
                );

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.Roadmap);
        }

        internal sealed class CreateRoadmapRequest
        {
            public string Nidn { get; set; }
            public string Link { get; set; }
        }
    }
}
