using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.ArtikelMediaMassa.PublicApi
{
    public sealed record ArtikelMediaMassaResponse(string Id, string Uuid, string Nama, int Nilai);
}
