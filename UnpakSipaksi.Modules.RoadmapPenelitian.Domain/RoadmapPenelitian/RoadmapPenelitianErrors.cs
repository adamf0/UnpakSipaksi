using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.RoadmapPenelitian.Domain.RoadmapPenelitian
{
    public static class RoadmapPenelitianErrors
    {
        public static Error EmptyData() =>
            Error.NotFound("RoadmapPenelitian.EmptyData", $"data is not found");

        public static Error NotFound(Guid Id) =>
            Error.NotFound("RoadmapPenelitian.NotFound", $"The event with the identifier {Id} was not found");

    }
}
