using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Modules.KesesuaianJadwal.Application.GetkesesuaianJadwalRepository;

namespace UnpakSipaksi.Modules.KesesuaianJadwal.Application.GetKesesuaianJadwal
{
    public sealed record GetKesesuaianJadwalQuery(Guid KesesuaianJadwalUuid) : IQuery<KesesuaianJadwalResponse>;
}
