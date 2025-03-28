using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.PenugasanReviewer.Application.UpdatePenugasanReviewer
{
    public sealed record StatusPenugasanReviewerCommand(
        Guid Uuid,
        string Nidn,
        int Status
    ) : ICommand;
}
