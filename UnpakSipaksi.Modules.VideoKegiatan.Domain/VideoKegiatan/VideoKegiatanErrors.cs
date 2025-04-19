using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.VideoKegiatan.Domain.VideoKegiatan
{
    public static class VideoKegiatanErrors
    {
        public static Error EmptyData() =>
            Error.NotFound("VideoKegiatan.EmptyData", "data is not found");
        public static Error NotSameValue() =>
            Error.NotFound("VideoKegiatan.NotSameValue", "not the same value in data 'nilai'");
        public static Error UnknownKategoriSkema() =>
            Error.NotFound("VideoKegiatan.UnknownKategoriSkema", "Unknown schema category");
        public static Error NotFound(Guid Id) =>
            Error.NotFound("VideoKegiatan.NotFound", $"Video kegiatan with the identifier {Id} was not found");

    }
}
