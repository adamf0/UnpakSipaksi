using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.MetodeRencanaKegiatan.Application.GetMetodeRencanaKegiatan
{
    public sealed record GetMetodeRencanaKegiatanDefaultQuery(Guid MetodeRencanaKegiatanUuid) : IQuery<MetodeRencanaKegiatanDefaultResponse>;
}
