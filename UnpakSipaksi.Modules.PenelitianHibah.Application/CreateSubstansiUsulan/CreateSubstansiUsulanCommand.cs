using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.PenelitianHibah.Application.CreateSubstansiUsulan
{
    public sealed record CreateSubstansiUsulanCommand(
          string UuidPenelitianHibah,
          string? File
    ) : ICommand<Guid>;
}
