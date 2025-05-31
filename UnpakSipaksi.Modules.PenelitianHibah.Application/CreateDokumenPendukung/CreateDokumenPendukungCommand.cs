using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.PenelitianHibah.Application.CreateDokumenPendukung
{
    public sealed record CreateDokumenPendukungCommand(
          string UuidPenelitianHibah,
          string? File,
          string? Link,
          string Kategori
    ) : ICommand<Guid>;
}
