using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Text.Encodings.Web;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.RelevansiKualitasReferensi.Application.UpdateRelevansiKualitasReferensi;
using UnpakSipaksi.Modules.RelevansiKualitasReferensi.Presentation;

namespace UnpakSipaksi.Modules.RelevansiKualitasReferensi.Presentation.RelevansiKualitasReferensi
{
    internal static class UpdateRelevansiKualitasReferensi
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPut("RelevansiKualitasReferensi", async (UpdateRelevansiKualitasReferensiRequest request, ISender sender) =>
            {
                Result result = await sender.Send(new UpdateRelevansiKualitasReferensiCommand(
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
            }).WithTags(Tags.RelevansiKualitasReferensi);
        }

        internal sealed class UpdateRelevansiKualitasReferensiRequest
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
