using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Modules.PenugasanReviewer.Application.GetPenugasanReviewer;

namespace UnpakSipaksi.Modules.PenugasanReviewer.Application.GetAllPenugasanReviewer
{
    public sealed record GetAllPenugasanReviewerQuery() : IQuery<List<PenugasanReviewerResponse>>;
}
