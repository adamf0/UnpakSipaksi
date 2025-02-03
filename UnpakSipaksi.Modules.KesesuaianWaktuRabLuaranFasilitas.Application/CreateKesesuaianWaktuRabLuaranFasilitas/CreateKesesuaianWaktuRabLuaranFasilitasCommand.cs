using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.KesesuaianWaktuRabLuaranFasilitas.Application.CreateKesesuaianWaktuRabLuaranFasilitas
{
    public sealed record CreateKesesuaianWaktuRabLuaranFasilitasCommand(
        string Nama,
        int Skor
    ) : ICommand<Guid>;
}
