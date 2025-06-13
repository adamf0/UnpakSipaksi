using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.ModelFeasibilityStudys.Application.DeleteModelFeasibilityStudys;

namespace UnpakSipaksi.Modules.ModelFeasibilityStudys.Presentation.ModelFeasibilityStudys
{
    internal class DeleteModelFeasibilityStudys
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapDelete("ModelFeasibilityStudys/{Uuid}", async (string Uuid, ISender sender) =>
            {
                Result result = await sender.Send(
                    new DeleteModelFeasibilityStudysCommand(Uuid)
                );

                return result.Match(() => Results.Ok(), ApiResults.Problem);
            }).WithTags(Tags.ModelFeasibilityStudys);
        }
    }
}
