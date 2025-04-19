using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.VideoKegiatan.PublicApi
{
    public sealed record VideoKegiatanResponse(string Id, string Uuid, string Nama, int Nilai);
}
