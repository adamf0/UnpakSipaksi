using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.SotaKebaharuan.Application.GetBobotSotaKebaharuan
{
    public sealed record GetBobotSotaKebaharuanQuery(string KategoriSkema) : IQuery<int?>;
}
