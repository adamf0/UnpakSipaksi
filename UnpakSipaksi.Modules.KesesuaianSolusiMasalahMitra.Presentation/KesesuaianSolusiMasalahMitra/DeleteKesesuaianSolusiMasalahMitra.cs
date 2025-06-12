using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.KesesuaianSolusiMasalahMitra.Application.DeleteKesesuaianSolusiMasalahMitra;
using UnpakSipaksi.Modules.KesesuaianSolusiMasalahMitra.Presentation;

namespace UnpakSipaksi.Modules.KesesuaianSolusiMasalahMitra.Presentation.KesesuaianSolusiMasalahMitra
{
    internal class DeleteKesesuaianSolusiMasalahMitra
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapDelete("KesesuaianSolusiMasalahMitra/{Uuid}", async (string Uuid, ISender sender) =>
            {
                Result result = await sender.Send(
                    new DeleteKesesuaianSolusiMasalahMitraCommand(Uuid)
                );

                return result.Match(() => Results.Ok(), ApiResults.Problem);
            }).WithTags(Tags.KesesuaianSolusiMasalahMitra);
        }
    }
}
