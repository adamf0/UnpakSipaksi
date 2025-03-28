using Microsoft.AspNetCore.Routing;

namespace UnpakSipaksi.Modules.PenugasanReviewer.Presentation.PenugasanReviewer
{
    public static class PenugasanReviewerEndpoints
    {
        public static void MapEndpoints(IEndpointRouteBuilder app)
        {
            CreatePenugasanReviewer.MapEndpoint(app);
            UpdatePenugasanReviewer.MapEndpoint(app);
            DeletePenugasanReviewer.MapEndpoint(app);
            StatusPenugasanReviewer.MapEndpoint(app);
            GetPenugasanReviewer.MapEndpoint(app);
            GetAllPenugasanReviewer.MapEndpoint(app);
        }
    }
}
