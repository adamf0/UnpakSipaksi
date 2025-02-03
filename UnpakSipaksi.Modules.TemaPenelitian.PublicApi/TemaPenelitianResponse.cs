using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.TemaPenelitian.PublicApi
{
    public sealed record TemaPenelitianResponse(string Id, string Uuid, string FokusPenelitianUuid, string Nama);
}
