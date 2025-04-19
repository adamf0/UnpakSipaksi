using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.Metode.Application.GetMetode;
using UnpakSipaksi.Modules.Metode.Presentation;

namespace UnpakSipaksi.Modules.Metode.Presentation.Metode
{
    internal static class GetMetode
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("Metode/{id}", async (Guid id, ISender sender) =>
            {
                Result<MetodeResponse> result = await sender.Send(new GetMetodeQuery(id));

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.Metode);
        }
    }
}
