using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Text.Encodings.Web;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.KetajamanPerumusanMasalah.Application.CreateKetajamanPerumusanMasalah;
using UnpakSipaksi.Modules.KetajamanPerumusanMasalah.Presentation;

namespace UnpakSipaksi.Modules.KetajamanPerumusanMasalah.Presentation.KetajamanPerumusanMasalah
{
    internal static class CreateKetajamanPerumusanMasalah
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("KetajamanPerumusanMasalah", async (CreateKetajamanPerumusanMasalahRequest request, ISender sender) =>
            {
                Result<Guid> result = await sender.Send(new CreateKetajamanPerumusanMasalahCommand(
                    HtmlEncoder.Default.Encode(request.Nama),
                    int.Parse(HtmlEncoder.Default.Encode(request.BobotPDP)),
                    int.Parse(HtmlEncoder.Default.Encode(request.BobotTerapan)),
                    int.Parse(HtmlEncoder.Default.Encode(request.BobotKerjasama)),
                    int.Parse(HtmlEncoder.Default.Encode(request.BobotPenelitianDasar)),
                    int.Parse(HtmlEncoder.Default.Encode(request.BobotSkor))
                    )
                );

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.KetajamanPerumusanMasalah);
        }

        internal sealed class CreateKetajamanPerumusanMasalahRequest
        {
            public string Nama { get; set; }

            public string BobotPDP { get; set; }
            public string BobotTerapan { get; set; }

            public string BobotKerjasama { get; set; }
            public string BobotPenelitianDasar { get; set; }
            public string BobotSkor { get; set; }
        }
    }
}
