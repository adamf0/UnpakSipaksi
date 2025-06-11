using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.PenelitianPkm.Application.UpdateSubstansiUsulan
{
    public sealed record UpdateSubstansiUsulanCommand(
          string Uuid,
          string UuidPenelitianPkm,
          string? File
    ) : ICommand;
}
