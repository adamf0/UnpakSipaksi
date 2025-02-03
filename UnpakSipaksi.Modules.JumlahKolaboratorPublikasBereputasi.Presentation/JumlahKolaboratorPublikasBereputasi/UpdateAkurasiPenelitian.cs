using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Text.Encodings.Web;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.JumlahKolaboratorPublikasBereputasi.Application.UpdateJumlahKolaboratorPublikasBereputasi;
using UnpakSipaksi.Modules.JumlahKolaboratorPublikasBereputasi.Presentation;

namespace UnpakSipaksi.Modules.JumlahKolaboratorPublikasBereputasi.Presentation.JumlahKolaboratorPublikasBereputasi
{
    internal static class UpdateJumlahKolaboratorPublikasBereputasi
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPut("JumlahKolaboratorPublikasBereputasi", async (UpdateJumlahKolaboratorPublikasBereputasiRequest request, ISender sender) =>
            {
                Result result = await sender.Send(new UpdateJumlahKolaboratorPublikasBereputasiCommand(
                    request.Id,
                    HtmlEncoder.Default.Encode(request.Nama),
                    int.Parse(HtmlEncoder.Default.Encode(request.BobotPDP)),
                    int.Parse(HtmlEncoder.Default.Encode(request.BobotTerapan)),
                    int.Parse(HtmlEncoder.Default.Encode(request.BobotKerjasama)),
                    int.Parse(HtmlEncoder.Default.Encode(request.BobotPenelitianDasar)),
                    int.Parse(HtmlEncoder.Default.Encode(request.BobotSkor))
                    )
                );

                return result.Match(() => Results.Ok(), ApiResults.Problem);
            }).WithTags(Tags.JumlahKolaboratorPublikasBereputasi);
        }

        internal sealed class UpdateJumlahKolaboratorPublikasBereputasiRequest
        {
            public Guid Id { get; set; }
            public string Nama { get; set; }

            public string BobotPDP { get; set; }
            public string BobotTerapan { get; set; }

            public string BobotKerjasama { get; set; }
            public string BobotPenelitianDasar { get; set; }
            public string BobotSkor { get; set; }
        }
    }
}
