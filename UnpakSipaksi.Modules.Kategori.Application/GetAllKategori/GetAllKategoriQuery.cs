using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Modules.Kategori.Application.GetKategori;

namespace UnpakSipaksi.Modules.Kategori.Application.GetAllKategori
{
    public sealed record GetAllKategoriQuery() : IQuery<List<KategoriResponse>>;
}
