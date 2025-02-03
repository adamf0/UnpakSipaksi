using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.KesesuaianJadwal.Application.CreateKesesuaianJadwal
{
    public sealed record CreateKesesuaianJadwalCommand(
        string Nama,
        int Nilai
    ) : ICommand<Guid>;
}
