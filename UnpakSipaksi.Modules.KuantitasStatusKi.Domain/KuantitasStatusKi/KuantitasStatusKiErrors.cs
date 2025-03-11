using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.KuantitasStatusKi.Domain.KuantitasStatusKi
{
    public static class KuantitasStatusKiErrors
    {
        public static Error EmptyData() =>
            Error.NotFound("KuantitasStatusKi.EmptyData", $"data is not found");

        public static Error NotFound(Guid Id) =>
            Error.NotFound("KuantitasStatusKi.NotFound", $"The event with the identifier {Id} was not found");

    }
}
