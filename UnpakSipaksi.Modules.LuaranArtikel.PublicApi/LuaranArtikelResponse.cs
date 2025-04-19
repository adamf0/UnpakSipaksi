using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.LuaranArtikel.PublicApi
{
    public sealed record LuaranArtikelResponse(string Id, string Uuid, string Nama, int Nilai);
}
