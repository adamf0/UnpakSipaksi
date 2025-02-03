using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.TopikPenelitian.PublicApi
{
    public sealed record TopikPenelitianResponse(string Id, string Uuid, string TemaPenelitianUuid, string Nama);
}
