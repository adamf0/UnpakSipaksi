using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.KualitasKuantitasPublikasiProsiding.PubliApi
{
    public sealed record KualitasKuantitasPublikasiProsidingResponse(string Id, string Uuid, string Nama, int Nilai);
}
