using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.PenelitianHibah.Application.UpdateLuaran
{
    public sealed record UpdateLuaranCommand(
          string Uuid,
          string UuidPenelitianHibah,
          string? UuidKategori,
          string? UuidKategoriLuaran,
          string? Keterangan,
          string? Link,
          string Jenis
    ) : ICommand<Guid>;
}
