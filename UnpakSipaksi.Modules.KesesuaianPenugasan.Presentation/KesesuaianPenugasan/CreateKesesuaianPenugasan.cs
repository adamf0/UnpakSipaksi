using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Text.Encodings.Web;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.KesesuaianPenugasan.Application.CreateKesesuaianPenugasan;
using UnpakSipaksi.Modules.KesesuaianPenugasan.Presentation;

namespace UnpakSipaksi.Modules.KesesuaianPenugasan.Presentation.KesesuaianPenugasan
{
    internal static class CreateKesesuaianPenugasan
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("KesesuaianPenugasan", async (CreateKesesuaianPenugasanRequest request, ISender sender) =>
            {
                Result<Guid> result = await sender.Send(new CreateKesesuaianPenugasanCommand(
                    HtmlEncoder.Default.Encode(request.Nama),
                    int.Parse(HtmlEncoder.Default.Encode(request.Nilai))
                    )
                );

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.KesesuaianPenugasan);
        }

        internal sealed class CreateKesesuaianPenugasanRequest
        {
            public string Nama { get; set; }
            public string Nilai { get; set; }
        }
    }
}
