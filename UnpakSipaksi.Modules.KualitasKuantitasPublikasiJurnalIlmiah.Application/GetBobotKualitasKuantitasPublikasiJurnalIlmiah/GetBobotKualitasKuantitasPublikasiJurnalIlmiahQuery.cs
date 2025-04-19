using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.KualitasKuantitasPublikasiJurnalIlmiah.Application.GetBobotKualitasKuantitasPublikasiJurnalIlmiah
{
    public sealed record GetBobotKualitasKuantitasPublikasiJurnalIlmiahQuery() : IQuery<int?>;
}
