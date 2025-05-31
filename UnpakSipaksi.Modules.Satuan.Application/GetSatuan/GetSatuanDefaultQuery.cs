

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.Satuan.Application.GetSatuan
{
    public sealed record GetSatuanDefaultQuery(Guid SatuanUuid) : IQuery<SatuanDefaultResponse>;
}
