using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.IndikatorCapaian.Application.GetIndikatorCapaian;
using UnpakSipaksi.Modules.IndikatorCapaian.Presentation;
using UnpakSipaksi.Modules.IndikatorCapaian.Application.GetAllIndikatorCapaian;

namespace UnpakSipaksi.Modules.IndikatorCapaian.Presentation.IndikatorCapaian
{
    internal class GetAllIndikatorCapaian
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("IndikatorCapaian", async (ISender sender) =>
            {
                Result<List<IndikatorCapaianResponse>> result = await sender.Send(new GetAllIndikatorCapaianQuery());

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.IndikatorCapaian);
        }
    }
}
