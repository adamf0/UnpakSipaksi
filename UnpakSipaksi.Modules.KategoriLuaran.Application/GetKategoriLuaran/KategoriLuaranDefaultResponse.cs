using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.KategoriLuaran.Application.GetKategoriLuaran
{
    public sealed record KategoriLuaranDefaultResponse
    {
        public string Id { get; set; }
        public string Uuid { get; set; }
        public string uuidKategori { get; set; } = default!;
        public string KategoriId { get; set; } = default!;
        public string Nama { get; set; } = default!;
        public string Status { get; set; } = default!;
    }
}
