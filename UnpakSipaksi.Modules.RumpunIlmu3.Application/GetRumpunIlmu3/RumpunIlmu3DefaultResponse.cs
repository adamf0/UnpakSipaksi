using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.RumpunIlmu3.Application.GetRumpunIlmu3
{
    public sealed record RumpunIlmu3DefaultResponse
    {
        public string Id { get; set; }
        public string Uuid { get; set; }
        public string Nama { get; set; }
    }
}
