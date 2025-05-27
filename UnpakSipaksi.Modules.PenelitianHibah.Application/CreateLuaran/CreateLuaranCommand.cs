using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.PenelitianHibah.Application.CreateLuaran
{
    public sealed record CreateLuaranCommand(
          string UuidPenelitianHibah,
          string? UuidKategori,
          string? UuidKategoriLuaran,
          string? Keterangan,
          string? Link,
          string Jenis
    ) : ICommand<Guid>;
}
