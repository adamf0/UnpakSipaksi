using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Text.Encodings.Web;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.RelevansiProdukKepentinganNasional.Application.UpdateRelevansiProdukKepentinganNasional;
using UnpakSipaksi.Modules.RelevansiProdukKepentinganNasional.Presentation;

namespace UnpakSipaksi.Modules.RelevansiProdukKepentinganNasional.Presentation.RelevansiProdukKepentinganNasional
{
    internal static class UpdateRelevansiProdukKepentinganNasional
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPut("RelevansiProdukKepentinganNasional", async (UpdateRelevansiProdukKepentinganNasionalRequest request, ISender sender) =>
            {
                Result result = await sender.Send(new UpdateRelevansiProdukKepentinganNasionalCommand(
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
            }).WithTags(Tags.RelevansiProdukKepentinganNasional);
        }

        internal sealed class UpdateRelevansiProdukKepentinganNasionalRequest
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
