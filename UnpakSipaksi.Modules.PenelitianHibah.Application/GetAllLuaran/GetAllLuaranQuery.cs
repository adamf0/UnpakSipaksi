using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Modules.PenelitianHibah.Application.GetAllLuaran;
using UnpakSipaksi.Modules.PenelitianHibah.Application.GetLuaran;

namespace UnpakSipaksi.Modules.PenelitianHibah.Application.GetAllLuaran
{
    public sealed record GetAllLuaranQuery(string UuidPenelitianHibah) : IQuery<List<LuaranResponse>>;
}
