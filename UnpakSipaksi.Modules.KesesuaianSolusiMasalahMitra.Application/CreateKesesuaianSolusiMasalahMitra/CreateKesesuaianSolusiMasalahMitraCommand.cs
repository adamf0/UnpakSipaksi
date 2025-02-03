using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.KesesuaianSolusiMasalahMitra.Application.CreateKesesuaianSolusiMasalahMitra
{
    public sealed record CreateKesesuaianSolusiMasalahMitraCommand(
        string Nama,
        int Nilai
    ) : ICommand<Guid>;
}
