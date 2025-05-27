using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Text.Encodings.Web;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.KesesuaianWaktuRabLuaranFasilitas.Application.CreateKesesuaianWaktuRabLuaranFasilitas;
using UnpakSipaksi.Modules.KesesuaianWaktuRabLuaranFasilitas.Presentation;

namespace UnpakSipaksi.Modules.KesesuaianWaktuRabLuaranFasilitas.Presentation.KesesuaianWaktuRabLuaranFasilitas
{
    internal static class CreateKesesuaianWaktuRabLuaranFasilitas
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("KesesuaianWaktuRabLuaranFasilitas", async (CreateKesesuaianWaktuRabLuaranFasilitasRequest request, ISender sender) =>
            {
                Result<Guid> result = await sender.Send(new CreateKesesuaianWaktuRabLuaranFasilitasCommand(
                    request.Nama,
                    request.Skor
                    )
                );

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.KesesuaianWaktuRabLuaranFasilitas);
        }

        internal sealed class CreateKesesuaianWaktuRabLuaranFasilitasRequest
        {
            public string Nama { get; set; }
            public int Skor { get; set; }
        }
    }
}
