using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.SotaKebaharuan.Application.GetAllSotaKebaharuan;
using UnpakSipaksi.Modules.SotaKebaharuan.Application.GetSotaKebaharuan;
using UnpakSipaksi.Modules.SotaKebaharuan.Presentation;

namespace UnpakSipaksi.Modules.SotaKebaharuan.Presentation.SotaKebaharuan
{
    internal class GetAllSotaKebaharuan
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("SotaKebaharuan", async (ISender sender) =>
            {
                Result<List<SotaKebaharuanResponse>> result = await sender.Send(new GetAllSotaKebaharuanQuery());

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.SotaKebaharuan);
        }
    }
}
