using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.MetodeRencanaKegiatan.PublicApi
{
    public sealed record MetodeRencanaKegiatanResponse(string Id, string Uuid, string Nama, int Nilai);
}
