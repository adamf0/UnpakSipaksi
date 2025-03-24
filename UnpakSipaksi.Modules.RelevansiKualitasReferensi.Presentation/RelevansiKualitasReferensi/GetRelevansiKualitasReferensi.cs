using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.RelevansiKualitasReferensi.Application.GetRelevansiKualitasReferensi;
using UnpakSipaksi.Modules.RelevansiKualitasReferensi.Application.GetRelevansiKualitasReferensi;
using UnpakSipaksi.Modules.RelevansiKualitasReferensi.Presentation;

namespace UnpakSipaksi.Modules.RelevansiKualitasReferensi.Presentation.RelevansiKualitasReferensi
{
    internal static class GetRelevansiKualitasReferensi
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("RelevansiKualitasReferensi/{id}", async (Guid id, ISender sender) =>
            {
                Result<RelevansiKualitasReferensiResponse> result = await sender.Send(new GetRelevansiKualitasReferensiQuery(id));

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.RelevansiKualitasReferensi);
        }
    }
}
