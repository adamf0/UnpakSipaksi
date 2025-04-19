using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.UrgensiPenelitian.Presentation;
using UnpakSipaksi.Modules.UrgensiPenelitian.Application.DeleteUrgensiPenelitian;

namespace UnpakSipaksi.Modules.UrgensiPenelitian.Presentation.UrgensiPenelitian
{
    internal class DeleteUrgensiPenelitian
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapDelete("UrgensiPenelitian/{id}", async (Guid id, ISender sender) =>
            {
                Result result = await sender.Send(
                    new DeleteUrgensiPenelitianCommand(id)
                );

                return result.Match(() => Results.Ok(), ApiResults.Problem);
            }).WithTags(Tags.UrgensiPenelitian);
        }
    }
}
