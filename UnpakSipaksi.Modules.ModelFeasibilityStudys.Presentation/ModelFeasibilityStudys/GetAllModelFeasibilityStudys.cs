using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.ModelFeasibilityStudys.Application.GetAllModelFeasibilityStudys;
using UnpakSipaksi.Modules.ModelFeasibilityStudys.Application.GetModelFeasibilityStudys;

namespace UnpakSipaksi.Modules.ModelFeasibilityStudys.Presentation.ModelFeasibilityStudys
{
    internal class GetAllModelFeasibilityStudys
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("ModelFeasibilityStudys", async (ISender sender) =>
            {
                Result<List<ModelFeasibilityStudysResponse>> result = await sender.Send(new GetAllModelFeasibilityStudysQuery());

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.ModelFeasibilityStudys);
        }
    }
}
