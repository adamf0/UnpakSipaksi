using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Modules.KategoriSumberDana.Application.GetKategoriSumberDana;

namespace UnpakSipaksi.Modules.KategoriSumberDana.Application.GetAllKategoriSumberDana
{
    public sealed record GetAllKategoriSumberDanaQuery() : IQuery<List<KategoriSumberDanaResponse>>;
}
