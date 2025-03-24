using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.ModelFeasibilityStudys.Domain.ModelFeasibilityStudys
{
    public static class ModelFeasibilityStudysErrors
    {
        public static Error EmptyData() =>
            Error.NotFound("ModelFeasibilityStudys.EmptyData", $"data is not found");

        public static Error NotFound(Guid Id) =>
            Error.NotFound("ModelFeasibilityStudys.NotFound", $"The event with the identifier {Id} was not found");

    }
}
