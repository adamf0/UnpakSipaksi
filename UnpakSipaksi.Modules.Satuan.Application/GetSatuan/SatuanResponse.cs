using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.Satuan.Application.GetSatuan
{
    public sealed record SatuanResponse
    {
        public string Uuid { get; set; }
        public string Nama { get; set; } = default!;
    }
}
