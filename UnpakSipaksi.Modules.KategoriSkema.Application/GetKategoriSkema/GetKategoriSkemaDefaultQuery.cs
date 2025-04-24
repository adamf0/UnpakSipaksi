using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.KategoriSkema.Application.GetKategoriSkema
{
    public sealed record GetKategoriSkemaDefaultQuery(Guid KategoriSkemaUuid) : IQuery<KategoriSkemaDefaultResponse>;
}
