using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.InovasiPemecahanMasalah.Application.GetInovasiPemecahanMasalah
{
    public sealed record GetInovasiPemecahanMasalahQuery(Guid InovasiPemecahanMasalahUuid) : IQuery<InovasiPemecahanMasalahResponse>;
}
