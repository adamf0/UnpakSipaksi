using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.Insentif.Application.GetInsentif
{
    public sealed record SbuInsentifResponse
    {
        public decimal SBU { get; set; }
        public decimal IFA { get; set; }
        public decimal ICA { get; set; }
        public decimal TotalInsentif { get; set; }
        public int PorsiSBU { get; set; }
    }
}
