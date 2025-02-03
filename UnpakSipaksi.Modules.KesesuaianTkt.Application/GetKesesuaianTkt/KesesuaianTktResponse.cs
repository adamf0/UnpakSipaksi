using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.KesesuaianTkt.Application.GetKesesuaianTkt
{
    public sealed record KesesuaianTktResponse
    {
        public string Uuid { get; set; }
        public string Nama { get; set; } = default!;
        public string Skor { get; set; } = default!;
    }
}
