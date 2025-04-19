using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.PenugasanReviewer.Domain.PenugasanReviewer
{
    public static class PenugasanReviewerErrors
    {
        public static Error EmptyData() =>
            Error.NotFound("PenugasanReviewer.EmptyData", "data is not found");

        public static Error NotFound(Guid Id) =>
            Error.NotFound("PenugasanReviewer.NotFound", $"Penugasan reviewer with the identifier {Id} was not found");

    }
}
