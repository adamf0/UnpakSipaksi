using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.PenelitianPkm.Application.GetDokumenMitra
{
    public sealed record DokumenMitraResponse
    {
        public string Uuid { get; set; }
        public string UuidPenelitianPkm { get; set; }
        public string Mitra { get; set; }
        public string Provinsi { get; set; }
        public string Kota { get; set; }
        public string UuidKelompokMitra { get; set; }
        public string PemimpinMitra { get; set; }
        public string KontakMitra { get; set; }
        public string File { get; set; }
    }
}
