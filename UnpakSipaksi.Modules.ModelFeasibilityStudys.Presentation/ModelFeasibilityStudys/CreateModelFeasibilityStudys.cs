using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Text.Encodings.Web;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.ModelFeasibilityStudys.Application.CreateModelFeasibilityStudys;

namespace UnpakSipaksi.Modules.ModelFeasibilityStudys.Presentation.ModelFeasibilityStudys
{
    internal static class CreateModelFeasibilityStudys
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("ModelFeasibilityStudys", async (CreateModelFeasibilityStudysRequest request, ISender sender) =>
            {
                Result<Guid> result = await sender.Send(new CreateModelFeasibilityStudysCommand(
                    request.Nama,
                    int.Parse(request.BobotSkor)
                    )
                );

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.ModelFeasibilityStudys);
        }

        internal sealed class CreateModelFeasibilityStudysRequest
        {
            public string Nama { get; set; }
            public string BobotSkor { get; set; }
        }
    }
}
