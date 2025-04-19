using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.KesesuaianPenugasan.Application.GetKesesuaianPenugasan
{
    public sealed record GetKesesuaianPenugasanDefaultQuery(Guid KesesuaianPenugasanUuid) : IQuery<KesesuaianPenugasanDefaultResponse>;
}
