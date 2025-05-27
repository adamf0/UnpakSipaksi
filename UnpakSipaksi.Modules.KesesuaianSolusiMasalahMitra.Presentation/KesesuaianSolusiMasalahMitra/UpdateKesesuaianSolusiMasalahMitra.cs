using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Text.Encodings.Web;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.KesesuaianSolusiMasalahMitra.Application.UpdateKesesuaianSolusiMasalahMitra;
using UnpakSipaksi.Modules.KesesuaianSolusiMasalahMitra.Presentation;

namespace UnpakSipaksi.Modules.KesesuaianSolusiMasalahMitra.Presentation.KesesuaianSolusiMasalahMitra
{
    internal static class UpdateKesesuaianSolusiMasalahMitra
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPut("KesesuaianSolusiMasalahMitra", async (UpdateKesesuaianSolusiMasalahMitraRequest request, ISender sender) =>
            {
                Result result = await sender.Send(new UpdateKesesuaianSolusiMasalahMitraCommand(
                    request.Id,
                    request.Nama,
                    request.Nilai
                    )
                );

                return result.Match(() => Results.Ok(), ApiResults.Problem);
            }).WithTags(Tags.KesesuaianSolusiMasalahMitra);
        }

        internal sealed class UpdateKesesuaianSolusiMasalahMitraRequest
        {
            public string Id { get; set; }
            public string Nama { get; set; }
            public int Nilai { get; set; }
        }
    }
}
