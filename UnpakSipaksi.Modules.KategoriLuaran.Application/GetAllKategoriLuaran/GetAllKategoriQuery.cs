using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Modules.KategoriLuaran.Application.GetKategoriLuaran;

namespace UnpakSipaksi.Modules.KategoriLuaran.Application.GetAllKategoriLuaran
{
    public sealed record GetAllKategoriLuaranQuery() : IQuery<List<KategoriLuaranResponse>>;
}
