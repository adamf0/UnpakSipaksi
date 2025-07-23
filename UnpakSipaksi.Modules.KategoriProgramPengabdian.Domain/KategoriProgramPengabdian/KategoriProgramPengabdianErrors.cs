using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.KategoriProgramPengabdian.Domain.KategoriProgramPengabdian
{
    public static class KategoriProgramPengabdianErrors
    {
        public static Error EmptyData() =>
            Error.NotFound("KategoriProgramPengabdian.EmptyData", "data is not found");

        public static Error InvalidFormatRule() =>
            Error.NotFound("KategoriProgramPengabdian.InvalidFormatRule", "rule is invalid format");

        public static Error NotFound(Guid Id) =>
            Error.NotFound("KategoriProgramPengabdian.NotFound", $"Kategori program pengabdian with identifier {Id} not found");

    }
}
