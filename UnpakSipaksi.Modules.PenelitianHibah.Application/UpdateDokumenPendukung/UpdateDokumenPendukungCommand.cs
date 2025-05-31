using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.PenelitianHibah.Application.UpdateDokumenPendukung
{
    public sealed record UpdateDokumenPendukungCommand(
          string Uuid,
          string UuidPenelitianHibah,
          string? File,
          string? Link,
          string Kategori
    ) : ICommand;
}
