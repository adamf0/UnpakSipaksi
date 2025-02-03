using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.KelompokMitra.Application.GetKelompokMitra
{
    public sealed record KelompokMitraResponse
    {
        public string Uuid { get; set; }
        public string Nama { get; set; } = default!;
    }
}
