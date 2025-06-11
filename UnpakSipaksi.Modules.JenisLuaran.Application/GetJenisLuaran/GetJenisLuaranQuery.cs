using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.JenisLuaran.Application.GetJenisLuaran
{
    public sealed record GetJenisLuaranQuery(Guid JenisLuaranUuid) : IQuery<JenisLuaranResponse>;
}
