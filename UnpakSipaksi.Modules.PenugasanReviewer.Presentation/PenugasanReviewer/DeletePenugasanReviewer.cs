using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.PenugasanReviewer.Presentation;
using UnpakSipaksi.Modules.PenugasanReviewer.Application.DeletePenugasanReviewer;

namespace UnpakSipaksi.Modules.PenugasanReviewer.Presentation.PenugasanReviewer
{
    internal class DeletePenugasanReviewer
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapDelete("PenugasanReviewer/{id}", async (string id, ISender sender) =>
            {
                Result result = await sender.Send(
                    new DeletePenugasanReviewerCommand(id)
                );

                return result.Match(() => Results.Ok(), ApiResults.Problem);
            }).WithTags(Tags.PenugasanReviewer);
        }
    }
}
