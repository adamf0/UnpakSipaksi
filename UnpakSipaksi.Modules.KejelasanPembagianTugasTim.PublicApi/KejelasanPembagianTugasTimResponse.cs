using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.KejelasanPembagianTugasTim.PublicApi
{
    public sealed record KejelasanPembagianTugasTimResponse(string Id, string Uuid, string Nama, int BobotPDP, int BobotTerapan, int BobotKerjasama, int BobotPenelitianDasar, int Skor);
}
