using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.KategoriLuaran.Application.GetKategoriLuaran
{
    public sealed record GetKategoriLuaranQuery(string Uuid) : IQuery<KategoriLuaranResponse>;
}
