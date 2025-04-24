using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.PenelitianHibah.Application.GetPenelitianHibah
{
    public sealed record PenelitianHibahDefaultResponse
    {
        public string Id { get; set; }
        public string Uuid { get; set; }
        public string NIDN { get; set; }
        public string Judul { get; set; }
        public string TahunPengajuan { get; set; }
        public SkemaDefaultResponse? Skema { get; set; }
        public RisetDefaultResponse? Riset { get; set; }
        public RumpunIlmuDefaultResponse? RumpunIlmu { get; set; }
        public int? LamaKegiatan { get; set; }
        public string Status { get; set; }
        public string? Type { get; set; }
    }
}
