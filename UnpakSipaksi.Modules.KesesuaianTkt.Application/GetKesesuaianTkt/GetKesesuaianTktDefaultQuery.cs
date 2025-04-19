using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.KesesuaianTkt.Application.GetKesesuaianTkt
{
    public sealed record GetKesesuaianTktDefaultQuery(Guid KesesuaianTktUuid) : IQuery<KesesuaianTktDefaultResponse>;
}
