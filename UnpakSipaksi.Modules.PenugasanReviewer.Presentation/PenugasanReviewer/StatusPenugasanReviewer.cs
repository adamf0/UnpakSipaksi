using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Text.Encodings.Web;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.PenugasanReviewer.Presentation;
using UnpakSipaksi.Modules.PenugasanReviewer.Application.StatusPenugasanReviewer;

namespace UnpakSipaksi.Modules.PenugasanReviewer.Presentation.PenugasanReviewer
{
    internal static class StatusPenugasanReviewer
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPut("PenugasanReviewer/Status/{Id}/{Status}", async (string Id, string Status, ISender sender) =>
            {
                Result result = await sender.Send(new StatusPenugasanReviewerCommand(
                    Guid.Parse(Id),
                    Status
                    )
                );

                return result.Match(() => Results.Ok(), ApiResults.Problem);
            }).WithTags(Tags.PenugasanReviewer);
        }

    }
}
