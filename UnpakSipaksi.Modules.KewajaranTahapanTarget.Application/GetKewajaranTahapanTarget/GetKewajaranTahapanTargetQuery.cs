using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.KewajaranTahapanTarget.Application.GetKewajaranTahapanTarget
{
    public sealed record GetKewajaranTahapanTargetQuery(Guid KewajaranTahapanTargetUuid) : IQuery<KewajaranTahapanTargetResponse>;
}
