using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.Satuan.Application.GetAllSatuan;
using UnpakSipaksi.Modules.Satuan.Application.GetSatuan;
using UnpakSipaksi.Modules.Satuan.Presentation;

namespace UnpakSipaksi.Modules.Satuan.Presentation.Satuan
{
    internal class GetAllSatuan
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("Satuan", async (ISender sender) =>
            {
                Result<List<SatuanResponse>> result = await sender.Send(new GetAllSatuanQuery());

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.Satuan);
        }
    }
}
