using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.PenelitianHibah.Application.CreateDokumenKontrak
{
    public sealed record CreateDokumenKontrakCommand(
          string UuidPenelitianHibah,
          string? File
    ) : ICommand<Guid>;
}
