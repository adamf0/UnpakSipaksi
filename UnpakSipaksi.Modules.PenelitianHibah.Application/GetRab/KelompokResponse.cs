using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.PenelitianHibah.Application.GetRab
{
    public sealed record KelompokResponse
    {
        public string Uuid { get; set; }
        public string Kelompok { get; set; }
    }
}
