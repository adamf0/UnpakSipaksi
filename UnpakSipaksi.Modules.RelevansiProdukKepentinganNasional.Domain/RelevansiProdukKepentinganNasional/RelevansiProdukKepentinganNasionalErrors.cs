using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.RelevansiProdukKepentinganNasional.Domain.RelevansiProdukKepentinganNasional
{
    public static class RelevansiProdukKepentinganNasionalErrors
    {
        public static Error EmptyData() =>
            Error.NotFound("RelevansiProdukKepentinganNasional.EmptyData", "data is not found");
        public static Error NotSameValue() =>
           Error.NotFound("RelevansiProdukKepentinganNasional.NotSameValue", "not the same value in data 'skor'");
        public static Error UnknownKategoriSkema() =>
            Error.NotFound("RelevansiProdukKepentinganNasional.UnknownKategoriSkema", "Unknown schema category");
        public static Error InvalidValueSkor() =>
            Error.NotFound("RelevansiProdukKepentinganNasional.InvalidValueSkor", "Invalid value 'skor'");
        public static Error NotFound(Guid Id) =>
            Error.NotFound("RelevansiProdukKepentinganNasional.NotFound", $"Relevansi produk kepentingan nasional with the identifier {Id} was not found");

    }
}
