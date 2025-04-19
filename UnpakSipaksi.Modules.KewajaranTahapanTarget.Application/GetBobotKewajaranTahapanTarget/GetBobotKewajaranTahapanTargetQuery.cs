using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.KewajaranTahapanTarget.Application.GetBobotKewajaranTahapanTarget
{
    public sealed record GetBobotKewajaranTahapanTargetQuery() : IQuery<int?>;
}
