using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Text.Encodings.Web;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.KetajamanAnalisis.Application.CreateKetajamanAnalisis;

namespace UnpakSipaksi.Modules.KetajamanAnalisis.Presentation.KetajamanAnalisis
{
    internal static class CreateKetajamanAnalisis
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("KetajamanAnalisis", async (CreateKetajamanAnalisisRequest request, ISender sender) =>
            {
                Result<Guid> result = await sender.Send(new CreateKetajamanAnalisisCommand(
                    HtmlEncoder.Default.Encode(request.Nama),
                    int.Parse(HtmlEncoder.Default.Encode(request.Nilai))
                    )
                );

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.KetajamanAnalisis);
        }

        internal sealed class CreateKetajamanAnalisisRequest
        {
            public string Nama { get; set; }

            public string Nilai { get; set; }
        }
    }
}
