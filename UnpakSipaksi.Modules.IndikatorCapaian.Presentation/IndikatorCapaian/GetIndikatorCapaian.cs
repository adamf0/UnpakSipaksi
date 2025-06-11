using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.IndikatorCapaian.Application.GetIndikatorCapaian;

namespace UnpakSipaksi.Modules.IndikatorCapaian.Presentation.IndikatorCapaian
{
    internal static class GetIndikatorCapaian
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("IndikatorCapaian/{id}", async (Guid id, ISender sender) =>
            {
                Result<IndikatorCapaianResponse> result = await sender.Send(new GetIndikatorCapaianQuery(id));

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.IndikatorCapaian);
        }
    }
}
