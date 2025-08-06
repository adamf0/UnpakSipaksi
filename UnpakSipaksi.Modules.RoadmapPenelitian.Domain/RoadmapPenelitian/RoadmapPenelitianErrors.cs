using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.RoadmapPenelitian.Domain.RoadmapPenelitian
{
    public static class RoadmapPenelitianErrors
    {
        public static Error EmptyData() =>
            Error.NotFound("RoadmapPenelitian.EmptyData", "data is not found");
        public static Error NotSameValue() =>
            Error.NotFound("RoadmapPenelitian.NotSameValue", "not the same value in data 'skor'");
        public static Error UnknownKategoriSkema() =>
            Error.NotFound("RoadmapPenelitian.UnknownKategoriSkema", "Unknown schema category");
        public static Error InvalidValueSkor() =>
            Error.NotFound("RoadmapPenelitian.InvalidValueSkor", "Invalid value 'skor'");
        public static Error NotFound(Guid Id) =>
            Error.NotFound("RoadmapPenelitian.NotFound", $"Roadmap Penelitian with the identifier {Id} was not found");

    }
}
