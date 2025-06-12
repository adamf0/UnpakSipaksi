using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.PenelitianPkm.Application.UpdateProgramPengabdian
{
    public sealed record UpdateProgramPengabdianCommand(
          string Uuid,
          string UuidPenelitianPkm,
          string? UuidFokusPengabdian,
          string? UuidRirn
    ) : ICommand;
}
