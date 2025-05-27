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
                    request.Nama,
                    request.BobotSkor
                    )
                );

                return result.Match(() => Results.Ok(), ApiResults.Problem);
            }).WithTags(Tags.JumlahKolaboratorPublikasBereputasi);
        }

        internal sealed class UpdateJumlahKolaboratorPublikasBereputasiRequest
        {
            public string Id { get; set; }
            public string Nama { get; set; }
            public int BobotSkor { get; set; }
        }
    }
}
