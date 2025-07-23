using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.Komponen.Domain.Komponen
{
    public static class KomponenErrors
    {
        public static Error EmptyData() =>
            Error.NotFound("Komponen.EmptyData", "data is not found");

        public static Error NotFound(Guid Id) =>
            Error.NotFound("Komponen.NotFound", $"Komponen with the identifier {Id} was not found");

        public static Error NamaEmpty() =>
            Error.NotFound("Komponen.NamaEmpty", "Nama can't be empty");
        public static Error InvalidMaxBiaya() =>
            Error.NotFound("Komponen.InvalidMaxBiaya", "MaxBiaya is invalid format");

    }
}
