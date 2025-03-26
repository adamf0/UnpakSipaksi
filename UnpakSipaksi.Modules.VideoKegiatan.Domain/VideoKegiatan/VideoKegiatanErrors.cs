using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.VideoKegiatan.Domain.VideoKegiatan
{
    public static class VideoKegiatanErrors
    {
        public static Error EmptyData() =>
            Error.NotFound("VideoKegiatan.EmptyData", $"data is not found");

        public static Error NotFound(Guid Id) =>
            Error.NotFound("VideoKegiatan.NotFound", $"The event with the identifier {Id} was not found");

    }
}
