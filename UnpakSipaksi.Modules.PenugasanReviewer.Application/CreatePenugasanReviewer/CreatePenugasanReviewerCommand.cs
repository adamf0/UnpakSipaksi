using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.PenugasanReviewer.Application.CreatePenugasanReviewer
{
    public sealed record CreatePenugasanReviewerCommand(
        string Nidn,
        int Status
    ) : ICommand<Guid>;
}
