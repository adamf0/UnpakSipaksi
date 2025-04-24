
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.KategoriSkema.PublicApi
{
    public sealed record KategoriSkemaResponse(string Id, string Uuid, string Nama, object? Rule);
}
