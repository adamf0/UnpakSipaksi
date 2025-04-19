using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.KualitasKuantitasPublikasiJurnalIlmiah.Application.GetKualitasKuantitasPublikasiJurnalIlmiah
{
    public sealed record GetKualitasKuantitasPublikasiJurnalIlmiahDefaultQuery(Guid KualitasKuantitasPublikasiJurnalIlmiahUuid) : IQuery<KualitasKuantitasPublikasiJurnalIlmiahDefaultResponse>;
}
