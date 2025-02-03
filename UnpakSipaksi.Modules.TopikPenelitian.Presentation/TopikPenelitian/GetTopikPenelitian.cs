using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.TopikPenelitian.Application.GetTopikPenelitian;
using UnpakSipaksi.Modules.TopikPenelitian.Presentation;

namespace UnpakSipaksi.Modules.TopikPenelitian.Presentation.TopikPenelitian
{
    internal static class GetTopikPenelitian
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("TopikPenelitian/{id}", async (Guid id, ISender sender) =>
            {
                Result<TopikPenelitianResponse> result = await sender.Send(new GetTopikPenelitianQuery(id));

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.TopikPenelitian);
        }
    }
}
