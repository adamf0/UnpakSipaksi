using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.KewajaranTahapanTarget.PublicApi
{
    public sealed record KewajaranTahapanTargetResponse(string Id, string Uuid, string Nama, int Nilai);
}
