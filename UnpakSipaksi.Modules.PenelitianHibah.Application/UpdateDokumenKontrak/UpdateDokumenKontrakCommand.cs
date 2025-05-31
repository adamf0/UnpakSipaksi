using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.PenelitianHibah.Application.UpdateDokumenKontrak
{
    public sealed record UpdateDokumenKontrakCommand(
          string Uuid,
          string UuidPenelitianHibah,
          string? File
    ) : ICommand;
}
