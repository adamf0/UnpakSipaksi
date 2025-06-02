using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.PenelitianHibah.Application.GetSubstansiUsulan
{
    public sealed record SubstansiUsulanResponse
    {
        public string Uuid { get; set; }
        public string UuidPenelitianHibah { get; set; }
        public string File { get; set; }
    }
}
