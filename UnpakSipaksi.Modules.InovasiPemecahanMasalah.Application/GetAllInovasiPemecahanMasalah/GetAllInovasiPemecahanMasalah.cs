using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Modules.InovasiPemecahanMasalah.Application.GetInovasiPemecahanMasalah;

namespace UnpakSipaksi.Modules.InovasiPemecahanMasalah.Application.GetAllInovasiPemecahanMasalah
{
    public sealed record GetAllInovasiPemecahanMasalahQuery() : IQuery<List<InovasiPemecahanMasalahResponse>>;
}
