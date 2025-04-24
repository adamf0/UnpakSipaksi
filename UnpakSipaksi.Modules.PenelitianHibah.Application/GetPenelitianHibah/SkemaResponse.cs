
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.PenelitianHibah.Application.GetPenelitianHibah
{
    public sealed record SkemaResponse
    {
        public string UuidSkema { get; set; }
        public string NamaSkema { get; set; }
        public string TKT { get; set; }
        public string UuidKategoriTkt { get; set; }
        public string NamaKategoriTkt { get; set; }
    }
}
