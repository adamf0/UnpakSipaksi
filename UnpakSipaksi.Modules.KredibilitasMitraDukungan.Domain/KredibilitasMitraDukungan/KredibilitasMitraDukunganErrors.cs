using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.KredibilitasMitraDukungan.Domain.KredibilitasMitraDukungan
{
    public static class KredibilitasMitraDukunganErrors
    {
        public static Error EmptyData() =>
            Error.NotFound("KredibilitasMitraDukungan.EmptyData", "data is not found");
        public static Error NotSameValue() =>
            Error.NotFound("KredibilitasMitraDukungan.NotSameValue", "not the same value in data 'skor'");
        public static Error UnknownKategoriSkema() =>
            Error.NotFound("KredibilitasMitraDukungan.UnknownKategoriSkema", "Unknown schema category");
        public static Error InvalidValueSkor() =>
            Error.NotFound("KredibilitasMitraDukungan.InvalidValueSkor", "Invalid value 'skor'");
        public static Error NotFound(Guid Id) =>
            Error.NotFound("KredibilitasMitraDukungan.NotFound", $"Kredibilitas mitra dukungan with the identifier {Id} was not found");

    }
}
