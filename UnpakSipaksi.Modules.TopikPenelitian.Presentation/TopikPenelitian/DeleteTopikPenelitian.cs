using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.TopikPenelitian.Application.DeleteTopikPenelitian;
using UnpakSipaksi.Modules.TopikPenelitian.Presentation;

namespace UnpakSipaksi.Modules.TopikPenelitian.Presentation.TopikPenelitian
{
    internal class DeleteTopikPenelitian
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapDelete("TopikPenelitian/{id}", async (string id, ISender sender) =>
            {
                Result result = await sender.Send(
                    new DeleteTopikPenelitianCommand(id)
                );

                return result.Match(() => Results.Ok(), ApiResults.Problem);
            }).WithTags(Tags.TopikPenelitian);
        }
    }
}
