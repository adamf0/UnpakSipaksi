using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Text.Encodings.Web;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.LuaranArtikel.Application.UpdateLuaranArtikel;
using UnpakSipaksi.Modules.LuaranArtikel.Presentation;

namespace UnpakSipaksi.Modules.LuaranArtikel.Presentation.LuaranArtikel
{
    internal static class UpdateLuaranArtikel
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPut("LuaranArtikel", async (UpdateLuaranArtikelRequest request, ISender sender) =>
            {
                Result result = await sender.Send(new UpdateLuaranArtikelCommand(
                    request.Id,
                    HtmlEncoder.Default.Encode(request.Nama),
                    int.Parse(HtmlEncoder.Default.Encode(request.Nilai))
                    )
                );

                return result.Match(() => Results.Ok(), ApiResults.Problem);
            }).WithTags(Tags.LuaranArtikel);
        }

        internal sealed class UpdateLuaranArtikelRequest
        {
            public Guid Id { get; set; }
            public string Nama { get; set; }
            public string Nilai { get; set; }
        }
    }
}
