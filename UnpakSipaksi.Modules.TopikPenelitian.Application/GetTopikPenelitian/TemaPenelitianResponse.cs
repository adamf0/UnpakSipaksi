using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.TopikPenelitian.Application.GetTopikPenelitian
{
    public sealed record TopikPenelitianResponse
    {
        public string Uuid { get; set; }
        public string Nama { get; set; } = default!;
        public string TemaPenelitianUuid { get; set; }
    }
}
