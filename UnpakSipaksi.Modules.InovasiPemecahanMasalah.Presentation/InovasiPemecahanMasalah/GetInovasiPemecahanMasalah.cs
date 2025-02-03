using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.InovasiPemecahanMasalah.Application.GetInovasiPemecahanMasalah;

namespace UnpakSipaksi.Modules.InovasiPemecahanMasalah.Presentation.InovasiPemecahanMasalah
{
    internal static class GetInovasiPemecahanMasalah
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("InovasiPemecahanMasalah/{id}", async (Guid id, ISender sender) =>
            {
                Result<InovasiPemecahanMasalahResponse> result = await sender.Send(new GetInovasiPemecahanMasalahQuery(id));

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.InovasiPemecahanMasalah);
        }
    }
}
