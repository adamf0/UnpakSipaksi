using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.PenugasanReviewer.Application.DeletePenugasanReviewer
{
    public sealed record DeletePenugasanReviewerCommand(
        Guid uuid
    ) : ICommand;
}
