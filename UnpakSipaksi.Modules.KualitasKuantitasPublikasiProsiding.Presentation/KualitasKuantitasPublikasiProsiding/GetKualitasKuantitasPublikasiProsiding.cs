using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.KualitasKuantitasPublikasiProsiding.Application.GetKualitasKuantitasPublikasiProsiding;

namespace UnpakSipaksi.Modules.KualitasKuantitasPublikasiProsiding.Presentation.KualitasKuantitasPublikasiProsiding
{
    internal static class GetKualitasKuantitasPublikasiProsiding
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("KualitasKuantitasPublikasiProsiding/{id}", async (Guid id, ISender sender) =>
            {
                Result<KualitasKuantitasPublikasiProsidingResponse> result = await sender.Send(new GetKualitasKuantitasPublikasiProsidingQuery(id));

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.KualitasKuantitasPublikasiProsiding);
        }
    }
}
