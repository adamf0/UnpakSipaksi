using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.IndikatorCapaian.PublicApi
{
    public sealed record IndikatorCapaianResponse(string Id, string? Uuid, string? JenisLuaranId, string? UuidJenisLuaran, string Nama, string? Status);
}
