using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.PenugasanReviewer.Application.GetPenugasanReviewer;
using UnpakSipaksi.Modules.PenugasanReviewer.Presentation;
using UnpakSipaksi.Modules.PenugasanReviewer.Application.GetAllPenugasanReviewer;

namespace UnpakSipaksi.Modules.PenugasanReviewer.Presentation.PenugasanReviewer
{
    internal class GetAllPenugasanReviewer
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("PenugasanReviewer", async (ISender sender) =>
            {
                Result<List<PenugasanReviewerResponse>> result = await sender.Send(new GetAllPenugasanReviewerQuery());

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.PenugasanReviewer);
        }
    }
}
