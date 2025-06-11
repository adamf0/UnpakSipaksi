using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.JenisLuaran.Application.UpdateJenisLuaran
{
    public sealed record UpdateJenisLuaranCommand(
        string Uuid,
        string Nama
    ) : ICommand;
}
