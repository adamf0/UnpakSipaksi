using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.PenugasanReviewer.Application.UpdatePenugasanReviewer
{
    public sealed record UpdatePenugasanReviewerCommand(
        string Uuid,
        string Nidn,
        int Status
    ) : ICommand;
}
