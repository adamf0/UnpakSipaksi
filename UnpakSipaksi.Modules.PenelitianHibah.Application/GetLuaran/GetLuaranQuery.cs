using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.PenelitianHibah.Application.GetLuaran
{
    public sealed record GetLuaranQuery(string Uuid, string UuidPenelitianHibah) : IQuery<LuaranResponse>;
}
