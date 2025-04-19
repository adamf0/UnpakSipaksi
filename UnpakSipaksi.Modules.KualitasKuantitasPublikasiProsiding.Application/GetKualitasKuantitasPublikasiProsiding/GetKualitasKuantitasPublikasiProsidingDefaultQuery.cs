using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.KualitasKuantitasPublikasiProsiding.Application.GetKualitasKuantitasPublikasiProsiding
{
    public sealed record GetKualitasKuantitasPublikasiProsidingDefaultQuery(Guid KualitasKuantitasPublikasiProsidingUuid) : IQuery<KualitasKuantitasPublikasiProsidingDefaultResponse>;
}
