using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Modules.KesesuaianJadwal.Application.GetKesesuaianJadwal;
using UnpakSipaksi.Modules.KesesuaianJadwal.Application.GetkesesuaianJadwalRepository;

namespace UnpakSipaksi.Modules.KesesuaianJadwal.Application.GetAllKesesuaianJadwal
{
    public sealed record GetAllKesesuaianJadwalQuery() : IQuery<List<KesesuaianJadwalResponse>>;
}
