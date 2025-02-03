using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.KategoriSumberDana.Application.GetKategoriSumberDana
{
    public sealed record KategoriSumberDanaResponse
    {
        public string Uuid { get; set; }
        public string Nama { get; set; } = default!;
    }
}
