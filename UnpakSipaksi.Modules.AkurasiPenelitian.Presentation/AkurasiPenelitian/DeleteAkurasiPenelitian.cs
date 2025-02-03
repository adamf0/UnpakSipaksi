using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.AkurasiPenelitian.Application.DeleteAkurasiPenelitian;

namespace UnpakSipaksi.Modules.AkurasiPenelitian.Presentation.AkurasiPenelitian
{
    internal class DeleteAkurasiPenelitian
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapDelete("AkurasiPenelitian/{id}", async (Guid id, ISender sender) =>
            {
                Result result = await sender.Send(
                    new DeleteAkurasiPenelitianCommand(id)
                );

                return result.Match(() => Results.Ok(), ApiResults.Problem);
            }).WithTags(Tags.AkurasiPenelitian);
        }
    }
}
