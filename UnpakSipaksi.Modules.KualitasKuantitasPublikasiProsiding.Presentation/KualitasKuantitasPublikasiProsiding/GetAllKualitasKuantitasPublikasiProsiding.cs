using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.KualitasKuantitasPublikasiProsiding.Application.GetAllKualitasKuantitasPublikasiProsiding;
using UnpakSipaksi.Modules.KualitasKuantitasPublikasiProsiding.Application.GetKualitasKuantitasPublikasiProsiding;

namespace UnpakSipaksi.Modules.KualitasKuantitasPublikasiProsiding.Presentation.KualitasKuantitasPublikasiProsiding
{
    internal class GetAllKualitasKuantitasPublikasiProsiding
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("KualitasKuantitasPublikasiProsiding", async (ISender sender) =>
            {
                Result<List<KualitasKuantitasPublikasiProsidingResponse>> result = await sender.Send(new GetAllKualitasKuantitasPublikasiProsidingQuery());

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.KualitasKuantitasPublikasiProsiding);
        }
    }
}
