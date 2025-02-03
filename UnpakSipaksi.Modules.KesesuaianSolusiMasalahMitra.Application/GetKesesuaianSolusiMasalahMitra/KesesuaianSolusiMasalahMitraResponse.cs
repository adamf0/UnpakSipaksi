using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.KesesuaianSolusiMasalahMitra.Application.GetKesesuaianSolusiMasalahMitra
{
    public sealed record KesesuaianSolusiMasalahMitraResponse
    {
        public string Uuid { get; set; }
        public string Nama { get; set; } = default!;
        public string Nilai { get; set; } = default!;
    }
}
