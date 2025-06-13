using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.RumpunIlmu1.Application.DeleteRumpunIlmu1;
using UnpakSipaksi.Modules.RumpunIlmu1.Presentation;

namespace UnpakSipaksi.Modules.RumpunIlmu1.Presentation.RumpunIlmu1
{
    internal class DeleteRumpunIlmu1
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapDelete("RumpunIlmu1/{Uuid}", async (string Uuid, ISender sender) =>
            {
                Result result = await sender.Send(
                    new DeleteRumpunIlmu1Command(Uuid)
                );

                return result.Match(() => Results.Ok(), ApiResults.Problem);
            }).WithTags(Tags.RumpunIlmu1);
        }
    }
}
