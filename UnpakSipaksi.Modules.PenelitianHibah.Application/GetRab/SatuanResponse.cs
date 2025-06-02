using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.PenelitianHibah.Application.GetRab
{
    public sealed record SatuanResponse
    {
        public string Uuid{ get; set; }
        public string Satuan { get; set; }
    }
}
