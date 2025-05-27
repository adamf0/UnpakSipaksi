using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.KategoriLuaran.PublicApi
{
    public sealed record KategoriLuaranResponse(string Id, string Uuid, string uuidKategori, string KategoriId, string Nama, string Status);
}
