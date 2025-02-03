using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.KategoriMitraPenelitian.Application.GetKategoriMitraPenelitian
{
    public sealed record GetKategoriMitraPenelitianQuery(Guid KategoriMitraPenelitianUuid) : IQuery<KategoriMitraPenelitianResponse>;
}
