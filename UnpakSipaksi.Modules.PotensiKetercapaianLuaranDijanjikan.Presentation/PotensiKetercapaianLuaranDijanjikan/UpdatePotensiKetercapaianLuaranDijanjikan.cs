using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Text.Encodings.Web;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.PotensiKetercapaianLuaranDijanjikan.Application.UpdatePotensiKetercapaianLuaranDijanjikan;
using UnpakSipaksi.Modules.PotensiKetercapaianLuaranDijanjikan.Presentation;

namespace UnpakSipaksi.Modules.PotensiKetercapaianLuaranDijanjikan.Presentation.PotensiKetercapaianLuaranDijanjikan
{
    internal static class UpdatePotensiKetercapaianLuaranDijanjikan
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPut("PotensiKetercapaianLuaranDijanjikan", async (UpdatePotensiKetercapaianLuaranDijanjikanRequest request, ISender sender) =>
            {
                Result result = await sender.Send(new UpdatePotensiKetercapaianLuaranDijanjikanCommand(
                    request.Id,
                    request.Nama,
                    int.Parse(request.BobotSkor)
                    )
                );

                return result.Match(() => Results.Ok(), ApiResults.Problem);
            }).WithTags(Tags.PotensiKetercapaianLuaranDijanjikan);
        }

        internal sealed class UpdatePotensiKetercapaianLuaranDijanjikanRequest
        {
            public Guid Id { get; set; }
            public string Nama { get; set; }

            public string BobotSkor { get; set; }
        }
    }
}
