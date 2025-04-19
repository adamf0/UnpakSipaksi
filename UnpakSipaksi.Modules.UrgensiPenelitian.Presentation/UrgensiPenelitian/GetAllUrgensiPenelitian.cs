using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.UrgensiPenelitian.Application.GetUrgensiPenelitian;
using UnpakSipaksi.Modules.UrgensiPenelitian.Presentation;
using UnpakSipaksi.Modules.UrgensiPenelitian.Application.GetAllUrgensiPenelitian;

namespace UnpakSipaksi.Modules.UrgensiPenelitian.Presentation.UrgensiPenelitian
{
    internal class GetAllUrgensiPenelitian
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("UrgensiPenelitian", async (ISender sender) =>
            {
                Result<List<UrgensiPenelitianResponse>> result = await sender.Send(new GetAllUrgensiPenelitianQuery());

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.UrgensiPenelitian);
        }
    }
}
