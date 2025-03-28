using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.PenugasanReviewer.Application.GetPenugasanReviewer;
using UnpakSipaksi.Modules.PenugasanReviewer.Presentation;

namespace UnpakSipaksi.Modules.PenugasanReviewer.Presentation.PenugasanReviewer
{
    internal static class GetPenugasanReviewer
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("PenugasanReviewer/{id}", async (Guid id, ISender sender) =>
            {
                Result<PenugasanReviewerResponse> result = await sender.Send(new GetPenugasanReviewerQuery(id));

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.PenugasanReviewer);
        }
    }
}
