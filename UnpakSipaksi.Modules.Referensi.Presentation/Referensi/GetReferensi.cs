using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.Referensi.Application.GetReferensi;
using UnpakSipaksi.Modules.Referensi.Presentation;

namespace UnpakSipaksi.Modules.Referensi.Presentation.Referensi
{
    internal static class GetReferensi
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("Referensi/{id}", async (Guid id, ISender sender) =>
            {
                Result<ReferensiResponse> result = await sender.Send(new GetReferensiQuery(id));

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.Referensi);
        }
    }
}
