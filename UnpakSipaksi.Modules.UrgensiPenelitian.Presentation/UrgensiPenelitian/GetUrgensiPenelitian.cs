using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.UrgensiPenelitian.Application.GetUrgensiPenelitian;
using UnpakSipaksi.Modules.UrgensiPenelitian.Presentation;

namespace UnpakSipaksi.Modules.UrgensiPenelitian.Presentation.UrgensiPenelitian
{
    internal static class GetUrgensiPenelitian
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("UrgensiPenelitian/{id}", async (Guid id, ISender sender) =>
            {
                Result<UrgensiPenelitianResponse> result = await sender.Send(new GetUrgensiPenelitianQuery(id));

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.UrgensiPenelitian);
        }
    }
}
