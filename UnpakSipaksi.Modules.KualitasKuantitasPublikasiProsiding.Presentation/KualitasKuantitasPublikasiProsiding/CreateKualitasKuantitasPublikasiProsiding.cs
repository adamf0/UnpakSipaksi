using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Text.Encodings.Web;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.KualitasKuantitasPublikasiProsiding.Application.CreateKualitasKuantitasPublikasiProsiding;

namespace UnpakSipaksi.Modules.KualitasKuantitasPublikasiProsiding.Presentation.KualitasKuantitasPublikasiProsiding
{
    internal static class CreateKualitasKuantitasPublikasiProsiding
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("KualitasKuantitasPublikasiProsiding", async (CreateKualitasKuantitasPublikasiProsidingRequest request, ISender sender) =>
            {
                Result<Guid> result = await sender.Send(new CreateKualitasKuantitasPublikasiProsidingCommand(
                    HtmlEncoder.Default.Encode(request.Nama),
                    int.Parse(HtmlEncoder.Default.Encode(request.Nilai))
                    )
                );

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.KualitasKuantitasPublikasiProsiding);
        }

        internal sealed class CreateKualitasKuantitasPublikasiProsidingRequest
        {
            public string Nama { get; set; }
            public string Nilai { get; set; }
        }
    }
}
