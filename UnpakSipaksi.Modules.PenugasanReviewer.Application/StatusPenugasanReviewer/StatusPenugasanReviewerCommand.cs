using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.PenugasanReviewer.Application.StatusPenugasanReviewer
{
    public sealed record StatusPenugasanReviewerCommand(
        Guid Uuid,
        String Status
    ) : ICommand;
}
