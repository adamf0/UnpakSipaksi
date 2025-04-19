using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.KebaruanReferensi.Application.GetBobotKebaruanReferensi
{
    public sealed record GetBobotKebaruanReferensiQuery(string KategoriSkema) : IQuery<int?>;
}
