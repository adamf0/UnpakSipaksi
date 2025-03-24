using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.MetodeRencanaKegiatan.Application.GetMetodeRencanaKegiatan;
using UnpakSipaksi.Modules.MetodeRencanaKegiatan.Application.GetMetodeRencanaKegiatan;

namespace UnpakSipaksi.Modules.MetodeRencanaKegiatan.Presentation.MetodeRencanaKegiatan
{
    internal static class GetMetodeRencanaKegiatan
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("MetodeRencanaKegiatan/{id}", async (Guid id, ISender sender) =>
            {
                Result<MetodeRencanaKegiatanResponse> result = await sender.Send(new GetMetodeRencanaKegiatanQuery(id));

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.MetodeRencanaKegiatan);
        }
    }
}
