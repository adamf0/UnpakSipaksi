using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.PotensiKetercapaianLuaranDijanjikan.Application.GetPotensiKetercapaianLuaranDijanjikan;
using UnpakSipaksi.Modules.PotensiKetercapaianLuaranDijanjikan.Presentation;

namespace UnpakSipaksi.Modules.PotensiKetercapaianLuaranDijanjikan.Presentation.PotensiKetercapaianLuaranDijanjikan
{
    internal static class GetPotensiKetercapaianLuaranDijanjikan
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("PotensiKetercapaianLuaranDijanjikan/{id}", async (Guid id, ISender sender) =>
            {
                Result<PotensiKetercapaianLuaranDijanjikanResponse> result = await sender.Send(new GetPotensiKetercapaianLuaranDijanjikanQuery(id));

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.PotensiKetercapaianLuaranDijanjikan);
        }
    }
}
