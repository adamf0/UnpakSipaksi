using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.Insentif.Domain.CheckInsentif
{
    public class InsentifResult
    {
        public decimal IFA { get; set; }
        public decimal ICA { get; set; }
        public decimal TotalInsentif { get; set; }
        public int PorsiSBU { get; set; }
    }

}
