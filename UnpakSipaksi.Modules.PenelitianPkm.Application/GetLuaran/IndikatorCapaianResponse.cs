using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.PenelitianPkm.Application.GetLuaran
{
    public sealed record IndikatorCapaianResponse
    {
        public string? Id { get; set; }
        public string? Uuid { get; set; }
        public string? UuidPenelitianPkm { get; set; }
        public string Nama { get; set; }
        public string? Status { get; set; }
    }
}
