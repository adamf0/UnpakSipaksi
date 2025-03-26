using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Text.Encodings.Web;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.SotaKebaharuan.Application.CreateSotaKebaharuan;
using UnpakSipaksi.Modules.SotaKebaharuan.Presentation;

namespace UnpakSipaksi.Modules.SotaKebaharuan.Presentation.SotaKebaharuan
{
    internal static class CreateSotaKebaharuan
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("SotaKebaharuan", async (CreateSotaKebaharuanRequest request, ISender sender) =>
            {
                Result<Guid> result = await sender.Send(new CreateSotaKebaharuanCommand(
                    HtmlEncoder.Default.Encode(request.Nama),
                    int.Parse(HtmlEncoder.Default.Encode(request.BobotPDP)),
                    int.Parse(HtmlEncoder.Default.Encode(request.BobotTerapan)),
                    int.Parse(HtmlEncoder.Default.Encode(request.BobotKerjasama)),
                    int.Parse(HtmlEncoder.Default.Encode(request.BobotPenelitianDasar)),
                    int.Parse(HtmlEncoder.Default.Encode(request.BobotSkor))
                    )
                );

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.SotaKebaharuan);
        }

        internal sealed class CreateSotaKebaharuanRequest
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
