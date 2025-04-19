using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.KesesuaianJadwal.PublicApi
{
    public sealed record KesesuaianJadwalResponse(string Id, string Uuid, string Nama, int Nilai);
}
