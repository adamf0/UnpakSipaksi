using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.PenelitianPkm.Application.GetLuaran
{
    public sealed record LuaranResponse
    {
        public string? Uuid { get; set; }
        public string? UuidPenelitianPkm { get; set; }
        public JenisLuaranResponse? JenisLuaran { get; set; }
        public IndikatorCapaianResponse? IndikatorCapaian { get; set; }
    }
}
