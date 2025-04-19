using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.Metode.Application.GetMetode;
using UnpakSipaksi.Modules.Metode.Presentation;
using UnpakSipaksi.Modules.Metode.Application.GetAllMetode;

namespace UnpakSipaksi.Modules.Metode.Presentation.Metode
{
    internal class GetAllMetode
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("Metode", async (ISender sender) =>
            {
                Result<List<MetodeResponse>> result = await sender.Send(new GetAllMetodeQuery());

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.Metode);
        }
    }
}
