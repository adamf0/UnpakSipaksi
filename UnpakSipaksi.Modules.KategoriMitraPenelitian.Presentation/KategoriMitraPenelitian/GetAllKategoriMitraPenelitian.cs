using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.KategoriMitraPenelitian.Application.GetKategoriMitraPenelitian;
using UnpakSipaksi.Modules.KategoriMitraPenelitian.Application.GetAllKategoriMitraPenelitian;
using UnpakSipaksi.Modules.KategoriMitraPenelitian.Presentation;

namespace UnpakSipaksi.Modules.KategoriMitraPenelitian.Presentation.KategoriMitraPenelitian
{
    internal class GetAllKategoriMitraPenelitian
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("KategoriMitraPenelitian", async (ISender sender) =>
            {
                Result<List<KategoriMitraPenelitianResponse>> result = await sender.Send(new GetAllKategoriMitraPenelitianQuery());

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.KategoriMitraPenelitian);
        }
    }
}
