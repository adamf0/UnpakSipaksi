using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Modules.KesesuaianTkt.Application.GetKesesuaianTkt;

namespace UnpakSipaksi.Modules.KesesuaianTkt.Application.GetAllKesesuaianTkt
{
    public sealed record GetAllKesesuaianTktQuery() : IQuery<List<KesesuaianTktResponse>>;
}
