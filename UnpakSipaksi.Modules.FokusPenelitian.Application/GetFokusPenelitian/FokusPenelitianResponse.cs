using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.FokusPenelitian.Application.GetFokusPenelitian
{
    public sealed record FokusPenelitianResponse
    {
        public string Uuid { get; set; }
        public string Nama { get; set; } = default!;
    }
}
