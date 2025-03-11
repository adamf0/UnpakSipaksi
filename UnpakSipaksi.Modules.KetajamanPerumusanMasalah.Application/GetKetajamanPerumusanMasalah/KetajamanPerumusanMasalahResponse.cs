using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.KetajamanPerumusanMasalah.Application.GetKetajamanPerumusanMasalah
{
    public sealed record KetajamanPerumusanMasalahResponse
    {
        public string Uuid { get; set; }
        public string Nama { get; set; } = default!;
        public int BobotPDP { get; set; }
        public int BobotTerapan { get; set; }
        public int BobotKerjasama { get; set; }
        public int BobotPenelitianDasar { get; set; }
        public int Skor { get; set; }
    }
}
