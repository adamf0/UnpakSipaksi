using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.RelevansiKualitasReferensi.Application.GetBobotRelevansiKualitasReferensi
{
    public sealed record GetBobotRelevansiKualitasReferensiQuery(string KategoriSkema) : IQuery<int?>;
}
