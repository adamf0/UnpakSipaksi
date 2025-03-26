using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.RumusanPrioritasMitra.Domain.RumusanPrioritasMitra
{
    public static class RumusanPrioritasMitraErrors
    {
        public static Error EmptyData() =>
            Error.NotFound("RumusanPrioritasMitra.EmptyData", $"data is not found");

        public static Error NotFound(Guid Id) =>
            Error.NotFound("RumusanPrioritasMitra.NotFound", $"The event with the identifier {Id} was not found");

    }
}
