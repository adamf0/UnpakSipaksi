using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.KesesuaianWaktuRabLuaranFasilitas.Application.GetKesesuaianWaktuRabLuaranFasilitas
{
    public sealed record GetKesesuaianWaktuRabLuaranFasilitasQuery(Guid KesesuaianWaktuRabLuaranFasilitasUuid) : IQuery<KesesuaianWaktuRabLuaranFasilitasResponse>;
}
