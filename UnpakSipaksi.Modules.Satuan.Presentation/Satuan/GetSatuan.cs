using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.Satuan.Application.GetSatuan;

namespace UnpakSipaksi.Modules.Satuan.Presentation.Satuan
{
    internal static class GetSatuan
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("Satuan/{id}", async (Guid id, ISender sender) =>
            {
                Result<SatuanResponse> result = await sender.Send(new GetSatuanQuery(id));

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.Satuan);
        }
    }
}
