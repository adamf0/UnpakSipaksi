using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.KebaruanReferensi.PublicApi
{
    public sealed record KebaruanReferensiResponse(string Id, string Uuid, string Nama, int BobotPDP, int BobotTerapan, int BobotKerjasama, int BobotPenelitianDasar, int Skor);
}
