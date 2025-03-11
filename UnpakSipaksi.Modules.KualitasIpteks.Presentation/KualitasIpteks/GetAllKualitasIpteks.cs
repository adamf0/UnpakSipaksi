using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.KualitasIpteks.Application.GetAllKualitasIpteks;
using UnpakSipaksi.Modules.KualitasIpteks.Application.GetKualitasIpteks;

namespace UnpakSipaksi.Modules.KualitasIpteks.Presentation.KualitasIpteks
{
    internal class GetAllKualitasIpteks
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("KualitasIpteks", async (ISender sender) =>
            {
                Result<List<KualitasIpteksResponse>> result = await sender.Send(new GetAllKualitasIpteksQuery());

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.KualitasIpteks);
        }
    }
}
