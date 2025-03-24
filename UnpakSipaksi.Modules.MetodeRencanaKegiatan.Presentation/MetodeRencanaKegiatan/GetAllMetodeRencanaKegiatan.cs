using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.MetodeRencanaKegiatan.Application.GetAllMetodeRencanaKegiatan;
using UnpakSipaksi.Modules.MetodeRencanaKegiatan.Application.GetMetodeRencanaKegiatan;
using UnpakSipaksi.Modules.MetodeRencanaKegiatan.Presentation;

namespace UnpakSipaksi.Modules.MetodeRencanaKegiatan.Presentation.MetodeRencanaKegiatan
{
    internal class GetAllMetodeRencanaKegiatan
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("MetodeRencanaKegiatan", async (ISender sender) =>
            {
                Result<List<MetodeRencanaKegiatanResponse>> result = await sender.Send(new GetAllMetodeRencanaKegiatanQuery());

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.MetodeRencanaKegiatan);
        }
    }
}
