using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.RelevansiProdukKepentinganNasional.Domain.RelevansiProdukKepentinganNasional
{
    public static class RelevansiProdukKepentinganNasionalErrors
    {
        public static Error EmptyData() =>
            Error.NotFound("RelevansiProdukKepentinganNasional.EmptyData", $"data is not found");

        public static Error NotFound(Guid Id) =>
            Error.NotFound("RelevansiProdukKepentinganNasional.NotFound", $"The event with the identifier {Id} was not found");

    }
}
