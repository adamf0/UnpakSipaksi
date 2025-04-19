using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Modules.Referensi.Application.GetReferensi;

namespace UnpakSipaksi.Modules.Referensi.Application.GetAllReferensi
{
    public sealed record GetAllReferensiQuery() : IQuery<List<ReferensiResponse>>;
}
