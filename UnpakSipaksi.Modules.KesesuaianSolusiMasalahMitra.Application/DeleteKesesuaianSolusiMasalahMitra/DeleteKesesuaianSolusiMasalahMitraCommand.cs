using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.KesesuaianSolusiMasalahMitra.Application.DeleteKesesuaianSolusiMasalahMitra
{
    public sealed record DeleteKesesuaianSolusiMasalahMitraCommand(
        string uuid
    ) : ICommand;
}
