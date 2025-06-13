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
            app.MapDelete("PotensiKetercapaianLuaranDijanjikan/{Uuid}", async (string Uuid, ISender sender) =>
            {
                Result result = await sender.Send(
                    new DeletePotensiKetercapaianLuaranDijanjikanCommand(Uuid)
                );

                return result.Match(() => Results.Ok(), ApiResults.Problem);
            }).WithTags(Tags.PotensiKetercapaianLuaranDijanjikan);
        }
    }
}
