

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.PenelitianPkm.Application.GetPenelitianPkm
{
    public sealed record ProgramPengabdianResponse
    {
        public string? UuidFokusPengabdian { get; set; }
        public string NamaFokusPengabdian { get; set; }
        public string? UuidRirn { get; set; }
        public string NamaRirn { get; set; }
    }
}
