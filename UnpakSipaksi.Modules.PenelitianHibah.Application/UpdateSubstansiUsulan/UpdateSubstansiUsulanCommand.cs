using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.PenelitianHibah.Application.UpdateSubstansiUsulan
{
    public sealed record UpdateSubstansiUsulanCommand(
          string Uuid,
          string UuidPenelitianHibah,
          string? File
    ) : ICommand;
}
