using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.ModelFeasibilityStudys.Application.GetModelFeasibilityStudys;
using UnpakSipaksi.Modules.ModelFeasibilityStudys.Application.GetModelFeasibilityStudys;

namespace UnpakSipaksi.Modules.ModelFeasibilityStudys.Presentation.ModelFeasibilityStudys
{
    internal static class GetModelFeasibilityStudys
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("ModelFeasibilityStudys/{id}", async (Guid id, ISender sender) =>
            {
                Result<ModelFeasibilityStudysResponse> result = await sender.Send(new GetModelFeasibilityStudysQuery(id));

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.ModelFeasibilityStudys);
        }
    }
}
