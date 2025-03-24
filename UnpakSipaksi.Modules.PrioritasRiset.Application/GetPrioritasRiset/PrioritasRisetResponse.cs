using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.PrioritasRiset.Application.GetPrioritasRiset
{
    public sealed record PrioritasRisetResponse
    {
        public string Uuid { get; set; }
        public string Nama { get; set; } = default!;
    }
}
