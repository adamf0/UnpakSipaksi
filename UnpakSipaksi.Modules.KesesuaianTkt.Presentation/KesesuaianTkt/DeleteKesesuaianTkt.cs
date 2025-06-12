using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.KesesuaianTkt.Application.DeleteKesesuaianTkt;
using UnpakSipaksi.Modules.KesesuaianTkt.Presentation;

namespace UnpakSipaksi.Modules.KesesuaianTkt.Presentation.KesesuaianTkt
{
    internal class DeleteKesesuaianTkt
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapDelete("KesesuaianTkt/{Uuid}", async (string Uuid, ISender sender) =>
            {
                Result result = await sender.Send(
                    new DeleteKesesuaianTktCommand(Uuid)
                );

                return result.Match(() => Results.Ok(), ApiResults.Problem);
            }).WithTags(Tags.KesesuaianTkt);
        }
    }
}
