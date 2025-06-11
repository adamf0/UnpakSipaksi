using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Modules.JenisLuaran.Application.GetJenisLuaran;

namespace UnpakSipaksi.Modules.JenisLuaran.Application.GetAllJenisLuaran
{
    public sealed record GetAllJenisLuaranQuery() : IQuery<List<JenisLuaranResponse>>;
}
