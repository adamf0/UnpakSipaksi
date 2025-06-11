using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.JenisLuaran.Application.GetJenisLuaran
{
    public sealed record JenisLuaranResponse
    {
        public string Uuid { get; set; }
        public string Nama { get; set; } = default!;
    }
}
