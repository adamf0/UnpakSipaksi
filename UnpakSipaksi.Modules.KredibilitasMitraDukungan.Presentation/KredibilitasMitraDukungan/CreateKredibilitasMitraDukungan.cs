using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Text.Encodings.Web;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.KredibilitasMitraDukungan.Application.CreateKredibilitasMitraDukungan;
using UnpakSipaksi.Modules.KredibilitasMitraDukungan.Presentation;

namespace UnpakSipaksi.Modules.KredibilitasMitraDukungan.Presentation.KredibilitasMitraDukungan
{
    internal static class CreateKredibilitasMitraDukungan
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("KredibilitasMitraDukungan", async (CreateKredibilitasMitraDukunganRequest request, ISender sender) =>
            {
                Result<Guid> result = await sender.Send(new CreateKredibilitasMitraDukunganCommand(
                    HtmlEncoder.Default.Encode(request.Nama),
                    int.Parse(HtmlEncoder.Default.Encode(request.BobotPDP)),
                    int.Parse(HtmlEncoder.Default.Encode(request.BobotTerapan)),
                    int.Parse(HtmlEncoder.Default.Encode(request.BobotKerjasama)),
                    int.Parse(HtmlEncoder.Default.Encode(request.BobotPenelitianDasar)),
                    int.Parse(HtmlEncoder.Default.Encode(request.BobotSkor))
                    )
                );

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.KredibilitasMitraDukungan);
        }

        internal sealed class CreateKredibilitasMitraDukunganRequest
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
