using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.FokusPenelitian.Application.GetFokusPenelitian;
using UnpakSipaksi.Modules.FokusPenelitian.Application.GetAllFokusPenelitian;

namespace UnpakSipaksi.Modules.FokusPenelitian.Presentation.FokusPenelitian
{
    internal class GetAllFokusPenelitian
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("FokusPenelitian", async (ISender sender) =>
            {
                Result<List<FokusPenelitianResponse>> result = await sender.Send(new GetAllFokusPenelitianQuery());

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.FokusPenelitian);
        }
    }
}
