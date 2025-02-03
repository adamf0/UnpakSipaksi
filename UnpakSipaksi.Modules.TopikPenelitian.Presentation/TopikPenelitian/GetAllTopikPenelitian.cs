using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.TopikPenelitian.Application.GetTopikPenelitian;
using UnpakSipaksi.Modules.TopikPenelitian.Application.GetAllTopikPenelitian;
using UnpakSipaksi.Modules.TopikPenelitian.Presentation;

namespace UnpakSipaksi.Modules.TopikPenelitian.Presentation.TopikPenelitian
{
    internal class GetAllTopikPenelitian
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("TopikPenelitian", async (ISender sender) =>
            {
                Result<List<TopikPenelitianResponse>> result = await sender.Send(new GetAllTopikPenelitianQuery());

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.TopikPenelitian);
        }
    }
}
