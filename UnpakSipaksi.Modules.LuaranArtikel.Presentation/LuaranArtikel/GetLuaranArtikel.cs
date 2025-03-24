using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.LuaranArtikel.Application.GetLuaranArtikel;
using UnpakSipaksi.Modules.LuaranArtikel.Application.GetLuaranArtikel;
using UnpakSipaksi.Modules.LuaranArtikel.Presentation;

namespace UnpakSipaksi.Modules.LuaranArtikel.Presentation.LuaranArtikel
{
    internal static class GetLuaranArtikel
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("LuaranArtikel/{id}", async (Guid id, ISender sender) =>
            {
                Result<LuaranArtikelResponse> result = await sender.Send(new GetLuaranArtikelQuery(id));

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.LuaranArtikel);
        }
    }
}
