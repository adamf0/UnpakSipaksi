using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.KategoriSkema.Domain.KategoriSkema
{
    public static class KategoriSkemaErrors
    {
        public static Error EmptyData() =>
            Error.NotFound("KategoriSkema.EmptyData", $"data is not found");

        public static Error NotFound(Guid Id) =>
            Error.NotFound("KategoriSkema.NotFound", $"The event with the identifier {Id} was not found");

    }
}
