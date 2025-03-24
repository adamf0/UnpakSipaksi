using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.RelevansiKualitasReferensi.Application.GetAllRelevansiKualitasReferensi;
using UnpakSipaksi.Modules.RelevansiKualitasReferensi.Application.GetRelevansiKualitasReferensi;
using UnpakSipaksi.Modules.RelevansiKualitasReferensi.Presentation;

namespace UnpakSipaksi.Modules.RelevansiKualitasReferensi.Presentation.RelevansiKualitasReferensi
{
    internal class GetAllRelevansiKualitasReferensi
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("RelevansiKualitasReferensi", async (ISender sender) =>
            {
                Result<List<RelevansiKualitasReferensiResponse>> result = await sender.Send(new GetAllRelevansiKualitasReferensiQuery());

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.RelevansiKualitasReferensi);
        }
    }
}
