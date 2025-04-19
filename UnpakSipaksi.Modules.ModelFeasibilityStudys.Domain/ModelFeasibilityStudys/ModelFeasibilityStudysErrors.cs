using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.ModelFeasibilityStudys.Domain.ModelFeasibilityStudys
{
    public static class ModelFeasibilityStudysErrors
    {
        public static Error EmptyData() =>
            Error.NotFound("ModelFeasibilityStudys.EmptyData", "data is not found");
        public static Error NotSameValue() =>
            Error.NotFound("ModelFeasibilityStudys.NotSameValue", "not the same value in data 'nilai'");
        public static Error UnknownKategoriSkema() =>
            Error.NotFound("ModelFeasibilityStudys.UnknownKategoriSkema", "Unknown schema category");
        public static Error NotFound(Guid Id) =>
            Error.NotFound("ModelFeasibilityStudys.NotFound", $"Model feasibility study with the identifier {Id} was not found");

    }
}
