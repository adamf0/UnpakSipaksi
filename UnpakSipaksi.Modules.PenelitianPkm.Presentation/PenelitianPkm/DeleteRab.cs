using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.PenelitianPkm.Application.DeleteRAB;
using UnpakSipaksi.Modules.PenelitianPkm.Presentation;

namespace UnpakSipaksi.Modules.PenelitianPkm.Presentation.PenelitianPkm
{
    internal class DeleteRAB
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapDelete("PenelitianPkm/RAB/{uuid}/{UuidPenelitianPkm}", async (string uuid, string UuidPenelitianPkm, ISender sender) =>
            {
                Result result = await sender.Send(
                    new DeleteRABCommand(uuid, UuidPenelitianPkm)
                );

                return result.Match(() => Results.Ok(), ApiResults.Problem);
            }).WithTags(Tags.PenelitianPkm);
        }
    }
}
