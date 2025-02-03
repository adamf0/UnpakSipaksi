using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Modules.KesesuaianWaktuRabLuaranFasilitas.Application.GetKesesuaianWaktuRabLuaranFasilitas;

namespace UnpakSipaksi.Modules.KesesuaianWaktuRabLuaranFasilitas.Application.GetAllKesesuaianWaktuRabLuaranFasilitas
{
    public sealed record GetAllKesesuaianWaktuRabLuaranFasilitasQuery() : IQuery<List<KesesuaianWaktuRabLuaranFasilitasResponse>>;
}
