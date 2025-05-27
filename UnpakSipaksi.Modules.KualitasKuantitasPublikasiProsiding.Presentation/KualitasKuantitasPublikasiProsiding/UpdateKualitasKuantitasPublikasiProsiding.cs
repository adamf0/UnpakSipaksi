using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Text.Encodings.Web;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.KualitasKuantitasPublikasiProsiding.Application.UpdateKualitasKuantitasPublikasiProsiding;

namespace UnpakSipaksi.Modules.KualitasKuantitasPublikasiProsiding.Presentation.KualitasKuantitasPublikasiProsiding
{
    internal static class UpdateKualitasKuantitasPublikasiProsiding
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPut("KualitasKuantitasPublikasiProsiding", async (UpdateKualitasKuantitasPublikasiProsidingRequest request, ISender sender) =>
            {
                Result result = await sender.Send(new UpdateKualitasKuantitasPublikasiProsidingCommand(
                    request.Id,
                    request.Nama,
                    request.Nilai
                    )
                );

                return result.Match(() => Results.Ok(), ApiResults.Problem);
            }).WithTags(Tags.KualitasKuantitasPublikasiProsiding);
        }

        internal sealed class UpdateKualitasKuantitasPublikasiProsidingRequest
        {
            public string Id { get; set; }
            public string Nama { get; set; }
            public int Nilai { get; set; }
        }
    }
}
