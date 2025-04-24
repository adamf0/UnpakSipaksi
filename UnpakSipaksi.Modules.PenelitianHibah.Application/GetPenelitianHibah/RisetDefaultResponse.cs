
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.PenelitianHibah.Application.GetPenelitianHibah
{
    public sealed record RisetDefaultResponse
    {
        public string IdPrioritasRiset { get; set; }
        public string UuidPrioritasRiset { get; set; }
        public string NamaPrioritasRiset { get; set; }
        public string IdFokusPenelitian { get; set; }
        public string UuidFokusPenelitian { get; set; }
        public string NamaFokusPenelitian { get; set; }
        public string IdTemaPenelitian { get; set; }
        public string UuidTemaPenelitian { get; set; }
        public string NamaTemaPenelitian { get; set; }
        public string IdTopikPenelitian { get; set; }
        public string UuidTopikPenelitian { get; set; }
        public string NamaTopikPenelitian { get; set; }
    }
}
