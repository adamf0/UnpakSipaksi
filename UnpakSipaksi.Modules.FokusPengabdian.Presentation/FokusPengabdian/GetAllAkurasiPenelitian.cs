using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.FokusPengabdian.Application.GetFokusPengabdian;
using UnpakSipaksi.Modules.FokusPengabdian.Application.GetAllFokusPengabdian;
using UnpakSipaksi.Modules.FokusPengabdian.Presentation;

namespace UnpakSipaksi.Modules.FokusPengabdian.Presentation.FokusPengabdian
{
    internal class GetAllFokusPengabdian
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("FokusPengabdian", async (ISender sender) =>
            {
                Result<List<FokusPengabdianResponse>> result = await sender.Send(new GetAllFokusPengabdianQuery());

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.FokusPengabdian);
        }
    }
}
