using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.KesesuaianPenugasan.PublicApi
{
    public sealed record KesesuaianPenugasanResponse(string Id, string Uuid, string Nama, int Nilai);
}
