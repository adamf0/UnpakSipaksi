

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.KewajaranTahapanTarget.Application.GetKewajaranTahapanTarget
{
    public sealed record GetKewajaranTahapanTargetDefaultQuery(Guid KewajaranTahapanTargetUuid) : IQuery<KewajaranTahapanTargetDefaultResponse>;
}
