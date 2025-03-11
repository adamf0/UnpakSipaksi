using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.KualitasKuantitasPublikasiJurnalIlmiah.Application.GetKualitasKuantitasPublikasiJurnalIlmiah;

namespace UnpakSipaksi.Modules.KualitasKuantitasPublikasiJurnalIlmiah.Presentation.KualitasKuantitasPublikasiJurnalIlmiah
{
    internal static class GetKualitasKuantitasPublikasiJurnalIlmiah
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("KualitasKuantitasPublikasiJurnalIlmiah/{id}", async (Guid id, ISender sender) =>
            {
                Result<KualitasKuantitasPublikasiJurnalIlmiahResponse> result = await sender.Send(new GetKualitasKuantitasPublikasiJurnalIlmiahQuery(id));

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.KualitasKuantitasPublikasiJurnalIlmiah);
        }
    }
}
