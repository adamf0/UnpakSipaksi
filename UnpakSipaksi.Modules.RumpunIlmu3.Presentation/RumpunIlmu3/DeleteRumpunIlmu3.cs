using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.RumpunIlmu3.Application.DeleteRumpunIlmu3;
using UnpakSipaksi.Modules.RumpunIlmu3.Presentation;

namespace UnpakSipaksi.Modules.RumpunIlmu3.Presentation.RumpunIlmu3
{
    internal class DeleteRumpunIlmu3
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapDelete("RumpunIlmu3/{id}", async (string id, ISender sender) =>
            {
                Result result = await sender.Send(
                    new DeleteRumpunIlmu3Command(id)
                );

                return result.Match(() => Results.Ok(), ApiResults.Problem);
            }).WithTags(Tags.RumpunIlmu3);
        }
    }
}
