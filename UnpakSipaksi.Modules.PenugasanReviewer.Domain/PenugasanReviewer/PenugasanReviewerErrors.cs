using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.PenugasanReviewer.Domain.PenugasanReviewer
{
    public static class PenugasanReviewerErrors
    {
        public static Error EmptyData() =>
            Error.NotFound("PenugasanReviewer.EmptyData", "data is not found");
        public static Error InvalidValueStatus() =>
            Error.NotFound("PenugasanReviewer.InvalidValueStatus", "Invalid value 'status'");
        public static Error NotFound(Guid Id) =>
            Error.NotFound("PenugasanReviewer.NotFound", $"Penugasan reviewer with the identifier {Id} was not found");
        public static Error InvalidNidn() =>
            Error.NotFound("PenugasanReviewer.InvalidNidn", "Nidn is invalid format");
    }
}
