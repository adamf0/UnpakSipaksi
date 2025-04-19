using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.KualitasKuantitasPublikasiJurnalIlmiah.PubliApi
{
    public sealed record KualitasKuantitasPublikasiJurnalIlmiahResponse(string Id, string Uuid, string Nama, int Nilai);
}
