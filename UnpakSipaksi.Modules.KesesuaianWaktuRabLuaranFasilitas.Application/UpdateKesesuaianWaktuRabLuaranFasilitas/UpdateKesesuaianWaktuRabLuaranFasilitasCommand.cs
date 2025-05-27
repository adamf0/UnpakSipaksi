using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.KesesuaianWaktuRabLuaranFasilitas.Application.UpdateKesesuaianWaktuRabLuaranFasilitas
{
    public sealed record UpdateKesesuaianWaktuRabLuaranFasilitasCommand(
        string Uuid,
        string Nama,
        int Skor
    ) : ICommand;
}
