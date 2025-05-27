using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.KesesuaianSolusiMasalahMitra.Application.UpdateKesesuaianSolusiMasalahMitra
{
    public sealed record UpdateKesesuaianSolusiMasalahMitraCommand(
        string Uuid,
        string Nama,
        int Nilai
    ) : ICommand;
}
