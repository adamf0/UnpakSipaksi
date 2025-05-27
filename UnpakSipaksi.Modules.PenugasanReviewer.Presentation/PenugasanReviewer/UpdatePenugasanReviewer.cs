using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Text.Encodings.Web;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.PenugasanReviewer.Application.UpdatePenugasanReviewer;

namespace UnpakSipaksi.Modules.PenugasanReviewer.Presentation.PenugasanReviewer
{
    internal static class UpdatePenugasanReviewer
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPut("PenugasanReviewer", async (UpdatePenugasanReviewerRequest request, ISender sender) =>
            {
                Result result = await sender.Send(new UpdatePenugasanReviewerCommand(
                    request.Id,
                    request.Nidn,
                    request.Status
                    )
                );

                return result.Match(() => Results.Ok(), ApiResults.Problem);
            }).WithTags(Tags.PenugasanReviewer);
        }

        internal sealed class UpdatePenugasanReviewerRequest
        {
            public string Id { get; set; }
            public string Nidn { get; set; }
            public int Status { get; set; }
        }
    }
}
