using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.KesesuaianSolusiMasalahMitra.PublicApi
{
    public sealed record KesesuaianSolusiMasalahMitraResponse(string Id, string Uuid, string Nama, int Nilai);
}
