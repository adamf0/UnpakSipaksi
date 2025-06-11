using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.PenelitianHibah.Application.GetLuaran
{
    public sealed record KategoriResponse
    {
        public string? Uuid { get; set; }
        public string? Kategori { get; set; }
    }
}
