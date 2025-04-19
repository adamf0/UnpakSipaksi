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
                    request.Nama,
                    int.Parse(request.BobotSkor)
                    )
                );

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.KredibilitasMitraDukungan);
        }

        internal sealed class CreateKredibilitasMitraDukunganRequest
        {
            public string Nama { get; set; }
            public string BobotSkor { get; set; }
        }
    }
}
