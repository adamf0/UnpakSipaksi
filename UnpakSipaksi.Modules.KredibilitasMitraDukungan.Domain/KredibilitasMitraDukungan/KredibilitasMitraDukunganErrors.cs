using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.KredibilitasMitraDukungan.Domain.KredibilitasMitraDukungan
{
    public static class KredibilitasMitraDukunganErrors
    {
        public static Error EmptyData() =>
            Error.NotFound("KredibilitasMitraDukungan.EmptyData", $"data is not found");

        public static Error NotFound(Guid Id) =>
            Error.NotFound("KredibilitasMitraDukungan.NotFound", $"The event with the identifier {Id} was not found");

    }
}
