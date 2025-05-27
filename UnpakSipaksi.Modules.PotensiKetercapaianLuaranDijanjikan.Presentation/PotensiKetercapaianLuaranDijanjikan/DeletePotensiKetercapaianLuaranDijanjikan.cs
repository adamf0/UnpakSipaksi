using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.PotensiKetercapaianLuaranDijanjikan.Application.DeletePotensiKetercapaianLuaranDijanjikan;
using UnpakSipaksi.Modules.PotensiKetercapaianLuaranDijanjikan.Presentation;

namespace UnpakSipaksi.Modules.PotensiKetercapaianLuaranDijanjikan.Presentation.PotensiKetercapaianLuaranDijanjikan
{
    internal class DeletePotensiKetercapaianLuaranDijanjikan
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapDelete("PotensiKetercapaianLuaranDijanjikan/{id}", async (string id, ISender sender) =>
            {
                Result result = await sender.Send(
                    new DeletePotensiKetercapaianLuaranDijanjikanCommand(id)
                );

                return result.Match(() => Results.Ok(), ApiResults.Problem);
            }).WithTags(Tags.PotensiKetercapaianLuaranDijanjikan);
        }
    }
}
