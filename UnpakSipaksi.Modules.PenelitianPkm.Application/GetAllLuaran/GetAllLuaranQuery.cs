using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Modules.PenelitianPkm.Application.GetLuaran;

namespace UnpakSipaksi.Modules.PenelitianPkm.Application.GetAllLuaran
{
    public sealed record GetAllLuaranQuery(string UuidPenelitianPkm) : IQuery<List<LuaranResponse>>;
}
