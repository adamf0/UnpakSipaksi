using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.KetajamanPerumusanMasalah.Application.GetAllKetajamanPerumusanMasalah;
using UnpakSipaksi.Modules.KetajamanPerumusanMasalah.Application.GetKetajamanPerumusanMasalah;

namespace UnpakSipaksi.Modules.KetajamanPerumusanMasalah.Presentation.KetajamanPerumusanMasalah
{
    internal class GetAllKetajamanPerumusanMasalah
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("KetajamanPerumusanMasalah", async (ISender sender) =>
            {
                Result<List<KetajamanPerumusanMasalahResponse>> result = await sender.Send(new GetAllKetajamanPerumusanMasalahQuery());

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.KetajamanPerumusanMasalah);
        }
    }
}
