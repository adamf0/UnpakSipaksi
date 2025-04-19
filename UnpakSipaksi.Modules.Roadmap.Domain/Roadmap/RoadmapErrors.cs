using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.Roadmap.Domain.Roadmap
{
    public static class RoadmapErrors
    {
        public static Error EmptyData() =>
            Error.NotFound("Roadmap.EmptyData", "data is not found");

        public static Error NotFound(Guid Id) =>
            Error.NotFound("Roadmap.NotFound", $"Roadmap with the identifier {Id} was not found");

    }
}
