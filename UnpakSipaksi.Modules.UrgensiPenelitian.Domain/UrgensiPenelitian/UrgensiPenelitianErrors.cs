using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.UrgensiPenelitian.Domain.UrgensiPenelitian
{
    public static class UrgensiPenelitianErrors
    {
        public static Error EmptyData() =>
            Error.NotFound("UrgensiPenelitian.EmptyData", "data is not found");

        public static Error NotFound(Guid Id) =>
            Error.NotFound("UrgensiPenelitian.NotFound", $"Urgensi penelitian with the identifier {Id} was not found");

    }
}
