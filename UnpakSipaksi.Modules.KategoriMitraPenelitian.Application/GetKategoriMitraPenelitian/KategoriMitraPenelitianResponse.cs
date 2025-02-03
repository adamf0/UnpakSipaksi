using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.KategoriMitraPenelitian.Application.GetKategoriMitraPenelitian
{
    public sealed record KategoriMitraPenelitianResponse
    {
        public string Uuid { get; set; }
        public string Nama { get; set; } = default!;
    }
}
