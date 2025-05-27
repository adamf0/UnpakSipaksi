using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Text.Encodings.Web;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.KesesuaianWaktuRabLuaranFasilitas.Application.UpdateKesesuaianWaktuRabLuaranFasilitas;
using UnpakSipaksi.Modules.KesesuaianWaktuRabLuaranFasilitas.Presentation;

namespace UnpakSipaksi.Modules.KesesuaianWaktuRabLuaranFasilitas.Presentation.KesesuaianWaktuRabLuaranFasilitas
{
    internal static class UpdateKesesuaianWaktuRabLuaranFasilitas
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPut("KesesuaianWaktuRabLuaranFasilitas", async (UpdateKesesuaianWaktuRabLuaranFasilitasRequest request, ISender sender) =>
            {
                Result result = await sender.Send(new UpdateKesesuaianWaktuRabLuaranFasilitasCommand(
                    request.Id,
                    request.Nama,
                    request.Skor
                    )
                );

                return result.Match(() => Results.Ok(), ApiResults.Problem);
            }).WithTags(Tags.KesesuaianWaktuRabLuaranFasilitas);
        }

        internal sealed class UpdateKesesuaianWaktuRabLuaranFasilitasRequest
        {
            public string Id { get; set; }
            public string Nama { get; set; }
            public int Skor { get; set; }
        }
    }
}
