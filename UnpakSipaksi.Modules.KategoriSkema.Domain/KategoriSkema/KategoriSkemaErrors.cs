using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.KategoriSkema.Domain.KategoriSkema
{
    public static class KategoriSkemaErrors
    {
        public static Error EmptyData() =>
            Error.NotFound("KategoriSkema.EmptyData", "data is not found");
        public static Error InvalidFormatRule() =>
            Error.NotFound("KategoriSkema.InvalidFormatRule", "rule is invalid format");
        public static Error NotFound(Guid Id) =>
            Error.NotFound("KategoriSkema.NotFound", $"Kategori skema with identifier {Id} not found");

    }
}
