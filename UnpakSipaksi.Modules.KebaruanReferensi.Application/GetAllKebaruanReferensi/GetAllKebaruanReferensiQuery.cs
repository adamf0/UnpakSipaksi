using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Modules.KebaruanReferensi.Application.GetKebaruanReferensi;

namespace UnpakSipaksi.Modules.KebaruanReferensi.Application.GetAllKebaruanReferensi
{
    public sealed record GetAllKebaruanReferensiQuery() : IQuery<List<KebaruanReferensiResponse>>;
}
