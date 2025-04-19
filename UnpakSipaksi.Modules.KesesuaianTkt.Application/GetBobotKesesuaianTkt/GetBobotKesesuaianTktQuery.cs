using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.KesesuaianTkt.Application.GetBobotKesesuaianTkt
{
    public sealed record GetBobotKesesuaianTktQuery(string KategoriSkema) : IQuery<int?>;
}
