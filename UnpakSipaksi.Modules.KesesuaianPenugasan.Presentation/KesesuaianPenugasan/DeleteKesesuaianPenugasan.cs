using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.KesesuaianPenugasan.Application.DeleteKesesuaianPenugasan;
using UnpakSipaksi.Modules.KesesuaianPenugasan.Presentation;

namespace UnpakSipaksi.Modules.KesesuaianPenugasan.Presentation.KesesuaianPenugasan
{
    internal class DeleteKesesuaianPenugasan
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapDelete("KesesuaianPenugasan/{id}", async (string id, ISender sender) =>
            {
                Result result = await sender.Send(
                    new DeleteKesesuaianPenugasanCommand(id)
                );

                return result.Match(() => Results.Ok(), ApiResults.Problem);
            }).WithTags(Tags.KesesuaianPenugasan);
        }
    }
}
