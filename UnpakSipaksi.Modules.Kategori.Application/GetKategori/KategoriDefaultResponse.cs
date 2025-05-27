using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.Kategori.Application.GetKategori
{
    public sealed record KategoriDefaultResponse
    {
        public string Id { get; set; }
        public string Uuid { get; set; }
        public string Nama { get; set; } = default!;
    }
}
