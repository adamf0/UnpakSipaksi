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
                    request.Nama,
                    int.Parse(request.BobotSkor)
                    )
                );

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.KetajamanPerumusanMasalah);
        }

        internal sealed class CreateKetajamanPerumusanMasalahRequest
        {
            public string Nama { get; set; }
            public string BobotSkor { get; set; }
        }
    }
}
