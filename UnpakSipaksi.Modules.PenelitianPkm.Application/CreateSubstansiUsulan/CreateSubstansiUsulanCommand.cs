using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.PenelitianPkm.Application.CreateSubstansiUsulan
{
    public sealed record CreateSubstansiUsulanCommand(
          string UuidPenelitianPkm,
          string? File
    ) : ICommand<Guid>;
}
