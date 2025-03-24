using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.RumpunIlmu2.Application.GetRumpunIlmu2
{
    public sealed record RumpunIlmu2Response
    {
        public string Uuid { get; set; }
        public string Nama { get; set; } = default!;
        public string UuidRumpunIlmu1 { get; set; } = default!;
    }
}
