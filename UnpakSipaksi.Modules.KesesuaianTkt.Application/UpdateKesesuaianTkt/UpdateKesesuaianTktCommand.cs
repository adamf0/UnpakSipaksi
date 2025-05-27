using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.KesesuaianTkt.Application.UpdateKesesuaianTkt
{
    public sealed record UpdateKesesuaianTktCommand(
        string Uuid,
        string Nama,
        int Skor
    ) : ICommand;
}
