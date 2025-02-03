using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Modules.KesesuaianSolusiMasalahMitra.Application.GetKesesuaianSolusiMasalahMitra;

namespace UnpakSipaksi.Modules.KesesuaianSolusiMasalahMitra.Application.GetAllKesesuaianSolusiMasalahMitra
{
    public sealed record GetAllKesesuaianSolusiMasalahMitraQuery() : IQuery<List<KesesuaianSolusiMasalahMitraResponse>>;
}
