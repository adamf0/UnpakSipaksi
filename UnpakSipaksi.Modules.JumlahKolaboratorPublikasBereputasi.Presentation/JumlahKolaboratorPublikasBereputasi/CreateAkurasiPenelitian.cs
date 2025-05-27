using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Text.Encodings.Web;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.JumlahKolaboratorPublikasBereputasi.Application.CreateJumlahKolaboratorPublikasBereputasi;

namespace UnpakSipaksi.Modules.JumlahKolaboratorPublikasBereputasi.Presentation.JumlahKolaboratorPublikasBereputasi
{
    internal static class CreateJumlahKolaboratorPublikasBereputasi
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("JumlahKolaboratorPublikasBereputasi", async (CreateJumlahKolaboratorPublikasBereputasiRequest request, ISender sender) =>
            {
                Result<Guid> result = await sender.Send(new CreateJumlahKolaboratorPublikasBereputasiCommand(
                    request.Nama,
                    request.BobotSkor
                    )
                );

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.JumlahKolaboratorPublikasBereputasi);
        }

        internal sealed class CreateJumlahKolaboratorPublikasBereputasiRequest
        {
            public string Nama { get; set; }
            public int BobotSkor { get; set; }
        }
    }
}
