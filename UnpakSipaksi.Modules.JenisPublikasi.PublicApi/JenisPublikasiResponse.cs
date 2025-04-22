using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.JenisPublikasi.PublicApi
{
    public sealed record JenisPublikasiResponse(string Id, string Uuid, string Nama, int Sbu);
}
