using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.Rirn.Presentation;
using UnpakSipaksi.Modules.Rirn.Application.DeleteRirn;

namespace UnpakSipaksi.Modules.Rirn.Presentation.Rirn
{
    internal class DeleteRirn
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapDelete("Rirn/{id}", async (string id, ISender sender) =>
            {
                Result result = await sender.Send(
                    new DeleteRirnCommand(id)
                );

                return result.Match(() => Results.Ok(), ApiResults.Problem);
            }).WithTags(Tags.Rirn);
        }
    }
}
