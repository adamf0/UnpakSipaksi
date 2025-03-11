using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.KetajamanPerumusanMasalah.Application.GetKetajamanPerumusanMasalah;

namespace UnpakSipaksi.Modules.KetajamanPerumusanMasalah.Presentation.KetajamanPerumusanMasalah
{
    internal static class GetKetajamanPerumusanMasalah
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("KetajamanPerumusanMasalah/{id}", async (Guid id, ISender sender) =>
            {
                Result<KetajamanPerumusanMasalahResponse> result = await sender.Send(new GetKetajamanPerumusanMasalahQuery(id));

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.KetajamanPerumusanMasalah);
        }
    }
}
