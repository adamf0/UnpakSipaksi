using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.KategoriTkt.Application.GetBobotKategoriTkt
{
    public sealed record GetBobotKategoriTktQuery(string KategoriSkema) : IQuery<int?>;
}
