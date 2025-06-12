using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.PenelitianPkm.Application.GetDokumenLainnya
{
    public sealed record DokumenLainnyaResponse
    {
        public string Uuid { get; set; }
        public string UuidPenelitianPkm { get; set; }
        public string File { get; set; }
        public string? Keterangam { get; set; }
    }
}
