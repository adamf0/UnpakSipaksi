using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.KetajamanAnalisis.PublicApi
{
    public sealed record KetajamanAnalisisResponse(string Id, string Uuid, string Nama, int Nilai);
}
