using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.PenelitianPkm.Application.GetRab
{
    public sealed record KomponenResponse
    {
        public string Uuid { get; set; }
        public string Komponen { get; set; }
    }
}
