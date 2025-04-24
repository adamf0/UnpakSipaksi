

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.PenelitianHibah.Application.GetPenelitianHibah
{
    public sealed record RumpunIlmuDefaultResponse
    {
        public string IdRumpunIlmu1 { get; set; }
        public string UuidRumpunIlmu1 { get; set; }
        public string NamaRumpunIlmu1 { get; set; }
        public string IdRumpunIlmu2 { get; set; }
        public string UuidRumpunIlmu2 { get; set; }
        public string NamaRumpunIlmu2 { get; set; }
        public string IdRumpunIlmu3 { get; set; }
        public string UuidRumpunIlmu3 { get; set; }
        public string NamaRumpunIlmu3 { get; set; }
    }
}
