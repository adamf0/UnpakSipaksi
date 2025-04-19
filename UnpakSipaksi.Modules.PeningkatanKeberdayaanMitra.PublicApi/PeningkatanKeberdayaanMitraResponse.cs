using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.PeningkatanKeberdayaanMitra.PublicApi
{
    public sealed record PeningkatanKeberdayaanMitraResponse(string Id, string Uuid, string Nama, int Nilai);
}
