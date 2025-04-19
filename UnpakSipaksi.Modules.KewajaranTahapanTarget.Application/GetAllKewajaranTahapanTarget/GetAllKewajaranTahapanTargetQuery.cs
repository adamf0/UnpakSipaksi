using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Modules.KewajaranTahapanTarget.Application.GetKewajaranTahapanTarget;

namespace UnpakSipaksi.Modules.KewajaranTahapanTarget.Application.GetAllKewajaranTahapanTarget
{
    public sealed record GetAllKewajaranTahapanTargetQuery() : IQuery<List<KewajaranTahapanTargetResponse>>;
}
