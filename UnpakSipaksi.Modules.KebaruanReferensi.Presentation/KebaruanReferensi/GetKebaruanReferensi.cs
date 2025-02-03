using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.KebaruanReferensi.Application.GetKebaruanReferensi;
using UnpakSipaksi.Modules.KebaruanReferensi.Presentation;

namespace UnpakSipaksi.Modules.KebaruanReferensi.Presentation.KebaruanReferensi
{
    internal static class GetKebaruanReferensi
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("KebaruanReferensi/{id}", async (Guid id, ISender sender) =>
            {
                Result<KebaruanReferensiResponse> result = await sender.Send(new GetKebaruanReferensiQuery(id));

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.KebaruanReferensi);
        }
    }
}
