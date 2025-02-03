using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Modules.KategoriMitraPenelitian.Application.GetKategoriMitraPenelitian;

namespace UnpakSipaksi.Modules.KategoriMitraPenelitian.Application.GetAllKategoriMitraPenelitian
{
    public sealed record GetAllKategoriMitraPenelitianQuery() : IQuery<List<KategoriMitraPenelitianResponse>>;
}
