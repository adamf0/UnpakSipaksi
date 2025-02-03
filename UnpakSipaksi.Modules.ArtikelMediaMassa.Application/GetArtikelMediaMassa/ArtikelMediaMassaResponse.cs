using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.ArtikelMediaMassa.Application.GetArtikelMediaMassa
{
    public sealed record ArtikelMediaMassaResponse
    {
        public string Uuid { get; set; }
        public string Nama { get; set; } = default!;
        public int Nilai { get; set; }
    }
}
