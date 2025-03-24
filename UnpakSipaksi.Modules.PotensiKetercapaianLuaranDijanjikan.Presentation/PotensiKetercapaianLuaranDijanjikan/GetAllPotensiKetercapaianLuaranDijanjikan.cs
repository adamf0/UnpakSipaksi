using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.PotensiKetercapaianLuaranDijanjikan.Application.GetAllPotensiKetercapaianLuaranDijanjikan;
using UnpakSipaksi.Modules.PotensiKetercapaianLuaranDijanjikan.Application.GetPotensiKetercapaianLuaranDijanjikan;
using UnpakSipaksi.Modules.PotensiKetercapaianLuaranDijanjikan.Presentation;

namespace UnpakSipaksi.Modules.PotensiKetercapaianLuaranDijanjikan.Presentation.PotensiKetercapaianLuaranDijanjikan
{
    internal class GetAllPotensiKetercapaianLuaranDijanjikan
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("PotensiKetercapaianLuaranDijanjikan", async (ISender sender) =>
            {
                Result<List<PotensiKetercapaianLuaranDijanjikanResponse>> result = await sender.Send(new GetAllPotensiKetercapaianLuaranDijanjikanQuery());

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.PotensiKetercapaianLuaranDijanjikan);
        }
    }
}
