using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.KualitasKuantitasPublikasiJurnalIlmiah.Application.GetAllKualitasKuantitasPublikasiJurnalIlmiah;
using UnpakSipaksi.Modules.KualitasKuantitasPublikasiJurnalIlmiah.Application.GetKualitasKuantitasPublikasiJurnalIlmiah;

namespace UnpakSipaksi.Modules.KualitasKuantitasPublikasiJurnalIlmiah.Presentation.KualitasKuantitasPublikasiJurnalIlmiah
{
    internal class GetAllKualitasKuantitasPublikasiJurnalIlmiah
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("KualitasKuantitasPublikasiJurnalIlmiah", async (ISender sender) =>
            {
                Result<List<KualitasKuantitasPublikasiJurnalIlmiahResponse>> result = await sender.Send(new GetAllKualitasKuantitasPublikasiJurnalIlmiahQuery());

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.KualitasKuantitasPublikasiJurnalIlmiah);
        }
    }
}
