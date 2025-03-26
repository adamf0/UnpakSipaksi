using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Modules.Satuan.Application.GetSatuan;

namespace UnpakSipaksi.Modules.Satuan.Application.GetAllSatuan
{
    public sealed record GetAllSatuanQuery() : IQuery<List<SatuanResponse>>;
}
