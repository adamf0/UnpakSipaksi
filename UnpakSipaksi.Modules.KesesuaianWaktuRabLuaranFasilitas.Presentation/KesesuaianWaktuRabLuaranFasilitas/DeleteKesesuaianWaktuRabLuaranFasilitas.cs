using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.KesesuaianWaktuRabLuaranFasilitas.Application.DeleteKesesuaianWaktuRabLuaranFasilitas;
using UnpakSipaksi.Modules.KesesuaianWaktuRabLuaranFasilitas.Presentation;

namespace UnpakSipaksi.Modules.KesesuaianWaktuRabLuaranFasilitas.Presentation.KesesuaianWaktuRabLuaranFasilitas
{
    internal class DeleteKesesuaianWaktuRabLuaranFasilitas
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapDelete("KesesuaianWaktuRabLuaranFasilitas/{Uuid}", async (string Uuid, ISender sender) =>
            {
                Result result = await sender.Send(
                    new DeleteKesesuaianWaktuRabLuaranFasilitasCommand(Uuid)
                );

                return result.Match(() => Results.Ok(), ApiResults.Problem);
            }).WithTags(Tags.KesesuaianWaktuRabLuaranFasilitas);
        }
    }
}
