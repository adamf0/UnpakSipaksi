using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.Roadmap.Presentation;
using UnpakSipaksi.Modules.Roadmap.Application.DeleteRoadmap;

namespace UnpakSipaksi.Modules.Roadmap.Presentation.Roadmap
{
    internal class DeleteRoadmap
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapDelete("Roadmap/{Uuid}", async (string Uuid, ISender sender) =>
            {
                Result result = await sender.Send(
                    new DeleteRoadmapCommand(Uuid)
                );

                return result.Match(() => Results.Ok(), ApiResults.Problem);
            }).WithTags(Tags.Roadmap);
        }
    }
}
