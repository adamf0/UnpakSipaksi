using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.PenelitianHibah.Application.GetLuaran
{
    public sealed record LuaranResponse
    {
        public string? Uuid { get; set; }
        public string? UuidPenelitianHibah { get; set; }
        public KategoriResponse? Kategori { get; set; }
        public KategoriLuaranResponse? KategoriLuaran { get; set; }
    }
}
