using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.Komponen.PublicApi
{
    public sealed record KomponenResponse(string Id, string Uuid, string Nama, int? MaxBiaya);
}
