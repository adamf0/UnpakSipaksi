using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Text.Encodings.Web;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.KesesuaianSolusiMasalahMitra.Application.CreateKesesuaianSolusiMasalahMitra;
using UnpakSipaksi.Modules.KesesuaianSolusiMasalahMitra.Presentation;

namespace UnpakSipaksi.Modules.KesesuaianSolusiMasalahMitra.Presentation.KesesuaianSolusiMasalahMitra
{
    internal static class CreateKesesuaianSolusiMasalahMitra
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("KesesuaianSolusiMasalahMitra", async (CreateKesesuaianSolusiMasalahMitraRequest request, ISender sender) =>
            {
                Result<Guid> result = await sender.Send(new CreateKesesuaianSolusiMasalahMitraCommand(
                    HtmlEncoder.Default.Encode(request.Nama),
                    int.Parse(HtmlEncoder.Default.Encode(request.Nilai))
                    )
                );

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.KesesuaianSolusiMasalahMitra);
        }

        internal sealed class CreateKesesuaianSolusiMasalahMitraRequest
        {
            public string Nama { get; set; }
            public string Nilai { get; set; }
        }
    }
}
