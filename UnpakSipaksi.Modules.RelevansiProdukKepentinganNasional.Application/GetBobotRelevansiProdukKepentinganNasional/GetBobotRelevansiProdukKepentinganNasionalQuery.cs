using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.RelevansiProdukKepentinganNasional.Application.GetBobotRelevansiProdukKepentinganNasional
{
    public sealed record GetBobotRelevansiProdukKepentinganNasionalQuery(string KategoriSkema) : IQuery<int?>;
}
