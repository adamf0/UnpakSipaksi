using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.PenugasanReviewer.Application.GetPenugasanReviewer
{
    public sealed record GetPenugasanReviewerQuery(Guid PenugasanReviewerUuid) : IQuery<PenugasanReviewerResponse>;
}
