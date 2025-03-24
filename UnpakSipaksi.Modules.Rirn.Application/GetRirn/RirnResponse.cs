using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.Rirn.Application.GetRirn
{
    public sealed record RirnResponse
    {
        public string Uuid { get; set; }
        public string Nama { get; set; } = default!;
    }
}
