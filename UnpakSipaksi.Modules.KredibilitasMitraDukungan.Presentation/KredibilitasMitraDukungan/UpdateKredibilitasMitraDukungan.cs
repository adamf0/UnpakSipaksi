using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Text.Encodings.Web;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.KredibilitasMitraDukungan.Application.UpdateKredibilitasMitraDukungan;
using UnpakSipaksi.Modules.KredibilitasMitraDukungan.Presentation;

namespace UnpakSipaksi.Modules.KredibilitasMitraDukungan.Presentation.KredibilitasMitraDukungan
{
    internal static class UpdateKredibilitasMitraDukungan
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPut("KredibilitasMitraDukungan", async (UpdateKredibilitasMitraDukunganRequest request, ISender sender) =>
            {
                Result result = await sender.Send(new UpdateKredibilitasMitraDukunganCommand(
                    request.Id,
                    request.Nama,
                    int.Parse(request.BobotSkor)
                    )
                );

                return result.Match(() => Results.Ok(), ApiResults.Problem);
            }).WithTags(Tags.KredibilitasMitraDukungan);
        }

        internal sealed class UpdateKredibilitasMitraDukunganRequest
        {
            public string Id { get; set; }
            public string Nama { get; set; }
            public string BobotSkor { get; set; }
        }
    }
}
