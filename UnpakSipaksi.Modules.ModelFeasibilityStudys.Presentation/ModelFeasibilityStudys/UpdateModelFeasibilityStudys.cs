using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Text.Encodings.Web;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.ModelFeasibilityStudys.Application.UpdateModelFeasibilityStudys;

namespace UnpakSipaksi.Modules.ModelFeasibilityStudys.Presentation.ModelFeasibilityStudys
{
    internal static class UpdateModelFeasibilityStudys
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPut("ModelFeasibilityStudys", async (UpdateModelFeasibilityStudysRequest request, ISender sender) =>
            {
                Result result = await sender.Send(new UpdateModelFeasibilityStudysCommand(
                    request.Id,
                    request.Nama,
                    int.Parse(request.BobotSkor)
                    )
                );

                return result.Match(() => Results.Ok(), ApiResults.Problem);
            }).WithTags(Tags.ModelFeasibilityStudys);
        }

        internal sealed class UpdateModelFeasibilityStudysRequest
        {
            public string Id { get; set; }
            public string Nama { get; set; }
            public string BobotSkor { get; set; }
        }
    }
}
