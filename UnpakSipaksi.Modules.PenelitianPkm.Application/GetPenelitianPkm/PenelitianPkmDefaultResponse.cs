using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.PenelitianPkm.Application.GetPenelitianPkm
{
    public sealed record PenelitianPkmDefaultResponse
    {
        public string Id { get; set; }
        public string? Uuid { get; set; }
        public string NIDN { get; set; }
        public string Judul { get; set; }
        public string TahunPengajuan { get; set; }
        public KategoriProgramPengabdianDefaultResponse? KategoriProgramPengabdian { get; set; }
        public ProgramPengabdianDefaultResponse? ProgramPengabdian { get; set; }
        public RumpunIlmuDefaultResponse? RumpunIlmu { get; set; }
        public string Status { get; set; }
        public string? Type { get; set; }
    }
}
