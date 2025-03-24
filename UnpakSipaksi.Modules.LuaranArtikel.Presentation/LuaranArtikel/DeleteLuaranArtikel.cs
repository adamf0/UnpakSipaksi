using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.LuaranArtikel.Application.DeleteLuaranArtikel;
using UnpakSipaksi.Modules.LuaranArtikel.Presentation;

namespace UnpakSipaksi.Modules.LuaranArtikel.Presentation.LuaranArtikel
{
    internal class DeleteLuaranArtikel
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapDelete("LuaranArtikel/{id}", async (Guid id, ISender sender) =>
            {
                Result result = await sender.Send(
                    new DeleteLuaranArtikelCommand(id)
                );

                return result.Match(() => Results.Ok(), ApiResults.Problem);
            }).WithTags(Tags.LuaranArtikel);
        }
    }
}
