using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.KategoriTkt.Application.GetKategoriTkt
{
    public sealed record KategoriTktDefaultResponse
    {
        public string Id { get; set; }
        public string Uuid { get; set; }
        public string Nama { get; set; } = default!;
    }
}
