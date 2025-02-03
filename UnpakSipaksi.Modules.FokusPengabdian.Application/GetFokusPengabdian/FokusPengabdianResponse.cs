using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.FokusPengabdian.Application.GetFokusPengabdian
{
    public sealed record FokusPengabdianResponse
    {
        public string Uuid { get; set; }
        public string Nama { get; set; } = default!;
    }
}
