using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Text.Encodings.Web;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.PenugasanReviewer.Presentation;
using UnpakSipaksi.Modules.PenugasanReviewer.Application.CreatePenugasanReviewer;

namespace UnpakSipaksi.Modules.PenugasanReviewer.Presentation.PenugasanReviewer
{
    internal static class CreatePenugasanReviewer
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("PenugasanReviewer", async (CreatePenugasanReviewerRequest request, ISender sender) =>
            {
                Result<Guid> result = await sender.Send(new CreatePenugasanReviewerCommand(
                    HtmlEncoder.Default.Encode(request.Nidn),
                    request.Status
                    )
                );

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.PenugasanReviewer);
        }

        internal sealed class CreatePenugasanReviewerRequest
        {
            public string Nidn { get; set; }
            public int Status { get; set; }
        }
    }
}
