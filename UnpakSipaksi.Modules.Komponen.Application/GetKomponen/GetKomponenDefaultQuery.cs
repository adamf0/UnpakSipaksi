

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.Komponen.Application.GetKomponen
{
    public sealed record GetKomponenDefaultQuery(Guid KomponenUuid) : IQuery<KomponenDefaultResponse>;
}
