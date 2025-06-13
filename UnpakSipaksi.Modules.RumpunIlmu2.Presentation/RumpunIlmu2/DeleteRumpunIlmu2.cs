using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.RumpunIlmu2.Application.DeleteRumpunIlmu2;
using UnpakSipaksi.Modules.RumpunIlmu2.Presentation;

namespace UnpakSipaksi.Modules.RumpunIlmu2.Presentation.RumpunIlmu2
{
    internal class DeleteRumpunIlmu2
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapDelete("RumpunIlmu2/{Uuid}", async (string Uuid, ISender sender) =>
            {
                Result result = await sender.Send(
                    new DeleteRumpunIlmu2Command(Uuid)
                );

                return result.Match(() => Results.Ok(), ApiResults.Problem);
            }).WithTags(Tags.RumpunIlmu2);
        }
    }
}
