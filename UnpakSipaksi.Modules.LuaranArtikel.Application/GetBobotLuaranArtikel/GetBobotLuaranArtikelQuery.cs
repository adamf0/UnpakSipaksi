using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.LuaranArtikel.Application.GetBobotLuaranArtikel
{
    public sealed record GetBobotLuaranArtikelQuery() : IQuery<int?>;
}
