using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.KesesuaianJadwal.Application.DeleteKesesuaianJadwal
{
    public sealed record DeleteKesesuaianJadwalCommand(
        Guid uuid
    ) : ICommand;
}
