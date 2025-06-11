using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.JenisLuaran.PublicApi
{
    public sealed record JenisLuaranResponse(string Id, string Uuid, string Nama);
}
