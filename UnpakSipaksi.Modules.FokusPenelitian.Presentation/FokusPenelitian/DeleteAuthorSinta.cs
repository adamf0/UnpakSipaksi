using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.FokusPenelitian.Application.DeleteFokusPenelitian;

namespace UnpakSipaksi.Modules.FokusPenelitian.Presentation.FokusPenelitian
{
    internal class DeleteFokusPenelitian
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapDelete("FokusPenelitian/{id}", async (string id, ISender sender) =>
            {
                Result result = await sender.Send(
                    new DeleteFokusPenelitianCommand(id)
                );

                return result.Match(() => Results.Ok(), ApiResults.Problem);
            }).WithTags(Tags.FokusPenelitian);
        }
    }
}
