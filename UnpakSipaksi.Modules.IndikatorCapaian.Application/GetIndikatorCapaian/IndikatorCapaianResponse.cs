using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.IndikatorCapaian.Application.GetIndikatorCapaian
{
    public sealed record IndikatorCapaianResponse
    {
        public string Uuid { get; set; }
        public string? UuidJenisLuaran { get; set; } = default;
        public string Nama { get; set; } = default!;
        public string? Status { get; set; } = default;
    }
}
