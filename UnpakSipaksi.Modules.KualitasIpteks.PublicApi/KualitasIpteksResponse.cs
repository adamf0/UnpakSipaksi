using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.KualitasIpteks.PublicApi
{
    public sealed record KualitasIpteksResponse(string Id, string Uuid, string Nama, int Skor);
}
