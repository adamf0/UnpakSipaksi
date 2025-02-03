using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.InovasiPemecahanMasalah.Application.GetAllInovasiPemecahanMasalah;
using UnpakSipaksi.Modules.InovasiPemecahanMasalah.Application.GetInovasiPemecahanMasalah;

namespace UnpakSipaksi.Modules.InovasiPemecahanMasalah.Presentation.InovasiPemecahanMasalah
{
    internal class GetAllInovasiPemecahanMasalah
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("InovasiPemecahanMasalah", async (ISender sender) =>
            {
                Result<List<InovasiPemecahanMasalahResponse>> result = await sender.Send(new GetAllInovasiPemecahanMasalahQuery());

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.InovasiPemecahanMasalah);
        }
    }
}
