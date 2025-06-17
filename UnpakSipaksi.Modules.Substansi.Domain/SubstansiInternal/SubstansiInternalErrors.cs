using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.Substansi.Domain.SubstansiInternal
{
    public static class SubstansiInternalErrors
    {
        public static Error EmptyData() =>
            Error.NotFound("Substansi.EmptyData", "data is not found");
        public static Error InvalidPenelitianHibah() =>
            Error.NotFound("Substansi.InvalidPenelitianHibah", "PenelitianHibah is invalid data");
        public static Error InvalidArgument() =>
            Error.NotFound("Substansi.InvalidArgument", "data is invalid argument");
        public static Error InvalidReviewer() =>
            Error.NotFound("Substansi.InvalidReviewer", "Reviewer is invalid argument");
        public static Error InvalidDateRangeReview() =>
            Error.NotFound("Substansi.InvalidDateRangeReview", "Invalid date range review");
        public static Error NotFound(Guid Id) =>
            Error.NotFound("Substansi.NotFound", $"Substansi with the identifier {Id} was not found");

    }
}
