using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.KuantitasStatusKi.PublicApi
{
    public sealed record KuantitasStatusKiResponse(string Id, string Uuid, string Nama, int Nilai);
}
