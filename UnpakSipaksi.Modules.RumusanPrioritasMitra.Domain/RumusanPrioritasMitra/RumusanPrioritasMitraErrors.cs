using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.RumusanPrioritasMitra.Domain.RumusanPrioritasMitra
{
    public static class RumusanPrioritasMitraErrors
    {
        public static Error EmptyData() =>
            Error.NotFound("RumusanPrioritasMitra.EmptyData", "data is not found");
        public static Error NotSameValue() =>
            Error.NotFound("RumusanPrioritasMitra.NotSameValue", "not the same value in data 'nilai'");
        public static Error UnknownKategoriSkema() =>
            Error.NotFound("RumusanPrioritasMitra.UnknownKategoriSkema", "Unknown schema category");
        public static Error InvalidValueNilai() =>
            Error.NotFound("RumusanPrioritasMitra.InvalidValueNilai", "Invalid value 'nilai'");
        public static Error NotFound(Guid Id) =>
            Error.NotFound("RumusanPrioritasMitra.NotFound", $"Rumusan prioritas mitra with the identifier {Id} was not found");

    }
}
