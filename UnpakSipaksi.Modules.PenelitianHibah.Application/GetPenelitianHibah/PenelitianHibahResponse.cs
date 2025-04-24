using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.PenelitianHibah.Application.GetPenelitianHibah
{
    public sealed record PenelitianHibahResponse
    {
        public string Uuid { get; set; }
        public string NIDN { get; set; }
        public string Judul { get; set; }
        public string TahunPengajuan { get; set; }
        public SkemaResponse? Skema { get; set; }
        public RisetResponse? Riset { get; set; }
        public RumpunIlmuResponse? RumpunIlmu { get; set; }
        public int? LamaKegiatan { get; set; }
        public string Status { get; set; }
        public string? Type { get; set; }
    }
}
