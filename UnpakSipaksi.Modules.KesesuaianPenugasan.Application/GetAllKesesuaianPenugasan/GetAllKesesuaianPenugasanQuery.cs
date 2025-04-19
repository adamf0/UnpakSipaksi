using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Modules.KesesuaianPenugasan.Application.GetKesesuaianPenugasan;
using UnpakSipaksi.Modules.KesesuaianPenugasan.Application.GetKesesuaianPenugasan;

namespace UnpakSipaksi.Modules.KesesuaianPenugasan.Application.GetAllKesesuaianPenugasan
{
    public sealed record GetAllKesesuaianPenugasanQuery() : IQuery<List<KesesuaianPenugasanResponse>>;
}
