using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.PeningkatanKeberdayaanMitra.Domain.PeningkatanKeberdayaanMitra
{
    public static class PeningkatanKeberdayaanMitraErrors
    {
        public static Error EmptyData() =>
            Error.NotFound("PeningkatanKeberdayaanMitra.EmptyData", $"data is not found");

        public static Error NotFound(Guid Id) =>
            Error.NotFound("PeningkatanKeberdayaanMitra.NotFound", $"The event with the identifier {Id} was not found");

    }
}
