using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Modules.KategoriTkt.Application.GetKategoriTkt;

namespace UnpakSipaksi.Modules.KategoriTkt.Application.GetAllKategoriTkt
{
    public sealed record GetAllKategoriTktQuery() : IQuery<List<KategoriTktResponse>>;
}
