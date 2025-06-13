using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.TemaPenelitian.Application.DeleteTemaPenelitian;
using UnpakSipaksi.Modules.TemaPenelitian.Presentation;

namespace UnpakSipaksi.Modules.TemaPenelitian.Presentation.TemaPenelitian
{
    internal class DeleteTemaPenelitian
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapDelete("TemaPenelitian/{Uuid}", async (string Uuid, ISender sender) =>
            {
                Result result = await sender.Send(
                    new DeleteTemaPenelitianCommand(Uuid)
                );

                return result.Match(() => Results.Ok(), ApiResults.Problem);
            }).WithTags(Tags.TemaPenelitian);
        }
    }
}
