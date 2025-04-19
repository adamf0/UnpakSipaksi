using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.KesesuaianWaktuRabLuaranFasilitas.Application.GetKesesuaianWaktuRabLuaranFasilitas
{
    public sealed record KesesuaianWaktuRabLuaranFasilitasDefaultResponse
    {
        public string Id { get; set; }
        public string Uuid { get; set; }
        public string Nama { get; set; } = default!;
        public int BobotPDP { get; set; }
        public int BobotTerapan { get; set; }
        public int BobotKerjasama { get; set; }
        public int BobotPenelitianDasar { get; set; }
        public int Skor { get; set; } = default!;
    }
}
