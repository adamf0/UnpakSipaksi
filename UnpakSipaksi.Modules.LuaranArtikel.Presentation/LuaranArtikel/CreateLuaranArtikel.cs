using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Text.Encodings.Web;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.LuaranArtikel.Application.CreateLuaranArtikel;
using UnpakSipaksi.Modules.LuaranArtikel.Presentation;

namespace UnpakSipaksi.Modules.LuaranArtikel.Presentation.LuaranArtikel
{
    internal static class CreateLuaranArtikel
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("LuaranArtikel", async (CreateLuaranArtikelRequest request, ISender sender) =>
            {
                Result<Guid> result = await sender.Send(new CreateLuaranArtikelCommand(
                    request.Nama,
                    request.Nilai
                    )
                );

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.LuaranArtikel);
        }

        internal sealed class CreateLuaranArtikelRequest
        {
            public string Nama { get; set; }
            public int Nilai { get; set; }
        }
    }
}
